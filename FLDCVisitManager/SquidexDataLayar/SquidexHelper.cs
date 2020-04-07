using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.ClientLibrary;
using CMSDataLayer.Models;
using System.Linq;
using CMSDataLayer;
using Newtonsoft.Json;

namespace CMSDataLayer
{
    public class SquidexHelper : ICMSDataHelper
    {
        private static SquidexClient<LedColorsSeq, LedColorsSeqData> ledColorsSeqClient;
        private static SquidexClient<CollectionPoint, CollectionPointData> collectionPointsData;
        private static SquidexClient<ImageAsset, ImageAssetData> cpImageAsset;
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
        }

        public async Task<CollectionPoint> GetCollectionPointDataById(string cpId)
        {
            var query = new ODataQuery
            {
                Filter = $"data/pointID/iv eq {cpId}"
            };

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

        public async Task<CollectionPointAssets> GetCollectionAssets(List<string> iv)//, CollectionPoint cpData)
        {
            var dynamicClient = clientManager.CreateContentsClient<DynamicContentDetails, DynamicContentData>("collection-points");
            var referenceData = await dynamicClient.GetAsync(new HashSet<Guid>(iv.Select(x => Guid.Parse(x))));
            var assets = new CollectionPointAssets();
/*            cpData.Data.QuoteAssets = new List<QuoteAsset>();
            cpData.Data.ImageAssets = new List<ImageAssetData>();*/
            string jsonData;
            foreach (var content in referenceData.Items)
            {
                switch (content.SchemaName)
                {
                    case "cp-image-asset":
                        jsonData = JsonConvert.SerializeObject(content.Data, Formatting.None);
                        //return JsonConvert.DeserializeObject<ImageAssetData>(jsonData);
                        var insertedImage = JsonConvert.DeserializeObject<ImageAssetData>(jsonData);
                        insertedImage.Iv = content.Id.ToString();
                        assets.ImageAssets.Add(insertedImage);
                        break;
                    case "cp-qoute-asset":
                        jsonData = JsonConvert.SerializeObject(content.Data, Formatting.None);
                        //return JsonConvert.DeserializeObject<QuoteAsset>(jsonData);
                        var insertedQuote = JsonConvert.DeserializeObject<QuoteAsset>(jsonData);
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
    }
    public sealed class DynamicContent : SquidexEntityBase<DynamicData>
    {

    }

    public sealed class DynamicData : Content<DynamicData>
    {

    }

    public sealed class DynamicContentData : Dictionary<string, object>
    {

    }

    public sealed class DynamicContentDetails : Content<DynamicContentData>
    {

    }
}
