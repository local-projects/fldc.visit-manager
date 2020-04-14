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
        private static SquidexClient<LedColorsSeq, LedColorsSeqData> ledColorsSeqClient;
        private static SquidexClient<CollectionPoint, CollectionPointData> collectionPointsData;
        private static SquidexClient<ImageAsset, ImageAssetData> cpImageAsset;
        private static SquidexClient<QuoteAsset, QuoteAssetData> cpQuoteAsset;
        private static SquidexClientManager clientManager;
        public SquidexHelper()
        {

        }

        public void ConnectToCMS(AppOptions appOptions)
        {
            clientManager = new SquidexClientManager(appOptions.Url, appOptions.AppName, appOptions.ClientId, appOptions.ClientSecret);

            ledColorsSeqClient = clientManager.GetClient<LedColorsSeq, LedColorsSeqData>("cp-led-color-sequence");
            collectionPointsData = clientManager.GetClient<CollectionPoint, CollectionPointData>("collection-points");
            cpImageAsset = clientManager.GetClient<ImageAsset, ImageAssetData>("cp-image-asset");
            cpQuoteAsset = clientManager.GetClient<QuoteAsset, QuoteAssetData>("cp-qoute-asset");
        }

        public async Task<CollectionPoint> GetCollectionPointDataById(string cpId)
        {
            var queryDictionary = CreateFieldDictonary("pointID/iv", cpId);
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

        private Dictionary<string, string> CreateFieldDictonary(string field, string value)
        {
            var queryFields = new Dictionary<string, string>();
            queryFields.Add(field, value);
            return queryFields;
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
                        var insertedImage = JsonConvert.DeserializeObject<ImageAssetData>(jsonData);
                        insertedImage.Iv = content.Id.ToString();
                        assets.ImageAssets.Add(insertedImage);
                        break;
                    case "cp-qoute-asset":
                        jsonData = JsonConvert.SerializeObject(content.Data, Formatting.None);
                        var insertedQuote = JsonConvert.DeserializeObject<QuoteAssetData>(jsonData);
                        insertedQuote.Iv = content.Id.ToString();
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
        public async void GetAllCollectabileAssets(DateTime? dateLastTaken)
        {
            var imageAssets = GetImageAssets(dateLastTaken);
            var quoteAssets = GetQuoteAssets(dateLastTaken);
        }

        public async Task<List<ImageAsset>> GetImageAssets(DateTime? dateLastTaken, string iv = null)
        {
            if (string.IsNullOrEmpty(iv))
            {
                var queryDictionary = CreateFieldDictonary("id", iv);
                var query = GetQuery(queryDictionary);
                var imageAssets = await cpImageAsset.GetAsync(query);
                return imageAssets.Items;
            }
            else
            {
                var imageAssets = await cpImageAsset.GetAllAsync();
                return imageAssets.Items;
            }
        }

        public async Task<List<QuoteAsset>> GetQuoteAssets(DateTime? dateLastTaken, string iv = null)
        {
            if (string.IsNullOrEmpty(iv))
            {
                var queryDictionary = CreateFieldDictonary("id", iv);
                var query = GetQuery(queryDictionary);
                var quoteAssets = await cpQuoteAsset.GetAsync(query);
                return quoteAssets.Items;
            }
            else
            {
                var quoteAssets = await cpQuoteAsset.GetAllAsync();
                return quoteAssets.Items;
            }
        }

        public ODataQuery GetQuery(Dictionary<string, string> fieldValuePair)
        {
            string filter = string.Empty;
            foreach (var pair in fieldValuePair.Select((Entry, Index) => new { Entry, Index }))
            {
                if (pair.Index == 0)
                    filter = $"data/{pair.Entry.Key} eq {pair.Entry.Value}";
                else
                    filter += $"&& data/{ pair.Entry.Key} eq { pair.Entry.Value}";
            }
            return new ODataQuery
            {
                Filter = filter
            };
        }
    }
}
