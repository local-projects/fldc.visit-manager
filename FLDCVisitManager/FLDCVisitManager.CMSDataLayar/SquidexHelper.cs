using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.ClientLibrary;
using FLDCVisitManager.CMSDataLayar.DTO;
using System.Linq;
using Newtonsoft.Json;

namespace FLDCVisitManager.CMSDataLayar
{
    public class SquidexHelper : ICMSDataHelper
    {
        private static IContentsClient<LedColorsSeq, LedColorsSeqData> ledColorsSeqClient;
        private static IContentsClient<CollectionPoint, CollectionPointData> collectionPointsData;
        private static IContentsClient<ImageAsset, ImageAssetData> cpImageAsset;
        private static IContentsClient<QuoteAsset, QuoteAssetData> cpQuoteAsset;
        private static SquidexClientManager clientManager;
        public SquidexHelper()
        {

        }

        public void ConnectToCMS(AppOptions appOptions)
        {
            var options = new SquidexOptions()
            {
                AppName = appOptions.AppName,
                ClientId = appOptions.ClientId,
                ClientSecret = appOptions.ClientSecret,
                Url = appOptions.Url
            };
            clientManager = new SquidexClientManager(options);
            ledColorsSeqClient = clientManager.CreateContentsClient<LedColorsSeq, LedColorsSeqData>("cp-led-color-sequence");
            collectionPointsData = clientManager.CreateContentsClient<CollectionPoint, CollectionPointData>("collection-points");
            cpImageAsset = clientManager.CreateContentsClient<ImageAsset, ImageAssetData>("cp-image-asset");
            cpQuoteAsset = clientManager.CreateContentsClient<QuoteAsset, QuoteAssetData>("cp-qoute-asset");
        }

        public async Task<CollectionPoint> GetCollectionPointDataById(string cpId)
        {
            var queryDictionary = new Dictionary<string, string>();
            CreateFieldDictonary("pointID/iv", cpId, queryDictionary);
            var query = GetQuery(queryDictionary); 
            var data = await collectionPointsData.GetAsync(query);
            var cpResult = data.Items.FirstOrDefault();
            if (cpResult.Data.TriggerAnimation != null)
                cpResult.Data.TriggerLedColorsSeq = await GetLedColors(cpResult.Data.TriggerAnimation.Iv.FirstOrDefault());
            if (cpResult.Data.SleepAnimation != null)
                cpResult.Data.SleepLedColorsSeq = await GetLedColors(cpResult.Data.SleepAnimation.Iv.FirstOrDefault());
/*            if (cpResult.Data.CollectionAssets != null)
                await GetCollectionAsset(cpResult.Data.CollectionAssets.Iv, cpResult);*/
            return data.Items.FirstOrDefault();
        }

        private void CreateFieldDictonary(string field, string value, Dictionary<string, string> queryFields)
        {
            queryFields.Add(field, value);
        }

        public async Task<CollectionPointAssets> GetCollectionAssets(List<string> iv)//, CollectionPoint cpData)
        {
            var dynamicClient = clientManager.CreateContentsClient<DynamicContentDetails, DynamicContentData>("collection-points");
            var referenceData = await dynamicClient.GetAsync(new HashSet<Guid>(iv.Select(x => Guid.Parse(x))));
            var assets = new CollectionPointAssets();
            string jsonData;
            foreach (var content in referenceData.Items)
            {
                switch (content.SchemaName)
                {
                    case "cp-image-asset":
                        jsonData = JsonConvert.SerializeObject(content.Data, Formatting.None);
                        var insertedImage = JsonConvert.DeserializeObject<ImageAsset>(jsonData);
                        insertedImage.Id = content.Id;
                        assets.ImageAssets.Add(insertedImage);
                        break;
                    case "cp-qoute-asset":
                        jsonData = JsonConvert.SerializeObject(content.Data, Formatting.None);
                        var insertedQuote = JsonConvert.DeserializeObject<QuoteAsset>(jsonData);
                        insertedQuote.Id = content.Id;
                        assets.QuoteAssets.Add(insertedQuote);
                        break;
                }
            }
            return assets;
        }

        public async Task<LedColorsSeq> GetLedColors(string seqId)
        {
            try
            {
                var refId = Guid.Parse(seqId);
                var colors = await ledColorsSeqClient.GetAsync(refId);
                return colors;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task<CollectionPointAssets> GetAllCollectibleAssets(DateTime? dateLastTaken)
        {
            var collectabileAssets = new CollectionPointAssets();
            collectabileAssets.ImageAssets = await GetImageAssets(dateLastTaken);
            collectabileAssets.QuoteAssets = await GetQuoteAssets(dateLastTaken);
            return collectabileAssets;
        }

        public async Task<List<ImageAsset>> GetImageAssets(DateTime? dateLastTaken, string iv = null)
        {
            if (!string.IsNullOrEmpty(iv) || dateLastTaken != null)
            {
                var queryDictionary = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(iv))
                    CreateFieldDictonary("id", iv, queryDictionary);
                if(dateLastTaken != null)
                    CreateFieldDictonary("lastModified", dateLastTaken.ToString(), queryDictionary);
                var query = GetQuery(queryDictionary);
                var imageAssets = await cpImageAsset.GetAsync(query);
                return imageAssets.Items;
            }
            else
            {
                var imageAssets = await cpImageAsset.GetAsync();
                return imageAssets.Items;
            }
        }

        public async Task<List<QuoteAsset>> GetQuoteAssets(DateTime? dateLastTaken, string iv = null)
        {
            if (!string.IsNullOrEmpty(iv) || dateLastTaken != null)
            {
                var queryDictionary = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(iv))
                    CreateFieldDictonary("id", iv, queryDictionary);
                if (dateLastTaken != null)
                    CreateFieldDictonary("lastModified", dateLastTaken.ToString(), queryDictionary);
                var query = GetQuery(queryDictionary);
                var quoteAssets = await cpQuoteAsset.GetAsync(query);
                return quoteAssets.Items;
            }
            else
            {
                var quoteAssets = await cpQuoteAsset.GetAsync();
                return quoteAssets.Items;
            }
        }

        public ContentQuery GetQuery(Dictionary<string, string> fieldValuePair)
        {
            string filter = string.Empty;
            foreach (var pair in fieldValuePair.Select((Entry, Index) => new { Entry, Index }))
            {
                if (pair.Index == 0)
                    filter = $"data/{pair.Entry.Key} eq {pair.Entry.Value}";
                else
                    filter += $"&& data/{ pair.Entry.Key} eq { pair.Entry.Value}";
            }
            return new ContentQuery
            {
                Filter = filter
            };
        }
    }
}
