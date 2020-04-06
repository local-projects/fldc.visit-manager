using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.ClientLibrary;
using CMSDataLayer.Models;
using System.Linq;
using CMSDataLayer;

namespace CMSDataLayer
{
    public class SquidexHalper : ICMSDataHelper
    {
        private static SquidexClient<LedColorsSeq, LedColorsSeqData> ledColorsSeqClient;
        private static SquidexClient<CollectionPoint, CollectionPointData> collectionPointsData;
        private static SquidexClient<ImageAsset, ImageAssetData> cpImageAsset;

        public SquidexHalper()
        {

        }

        public void ConnectToCMS(AppOptions appOptions)
        {
            var clientManager = new SquidexClientManager(appOptions.Url, appOptions.AppName, appOptions.ClientId, appOptions.ClientSecret);

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

        private Task GetCollectionAsset(string iv)
        {
            var referenceData = cpImageAsset.GetAsync(new HashSet<Guid> { Guid.Parse(iv) });
            throw new NotImplementedException();
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
}
