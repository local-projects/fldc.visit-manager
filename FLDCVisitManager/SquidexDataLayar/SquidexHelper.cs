using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.ClientLibrary;
using CMSDataLayer.Models;
using System.Linq;
using CMSDataLayer;

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

        public async Task<CollectionPoint> GetCollectionPointsById(string cpId)
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
            if (cpResult.Data.CollectionAssets != null)
                await GetCollectionAsset(cpResult.Data.CollectionAssets.Iv.FirstOrDefault());
            return data.Items.FirstOrDefault();
        }

        private async Task GetCollectionAsset(string iv)
        {
            var referenceData = await cpImageAsset.GetAsync(new HashSet<Guid> { Guid.Parse(iv) });
            //var asset = clientManager.GetClient<DynamicContent, DynamicData>(referenceData.Items.FirstOrDefault().SchemaName);
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
    public sealed class DynamicData : SquidexEntityBase<DynamicContent>
    {

    }

    public sealed class DynamicContent : Content<DynamicData>
    {

    }
}
