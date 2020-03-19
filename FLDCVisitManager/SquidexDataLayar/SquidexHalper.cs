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

        public SquidexHalper()
        {

        }

        public void ConnectToCMS(AppOptions appOptions)
        {
            var clientManager = new SquidexClientManager(appOptions.Url, appOptions.AppName, appOptions.ClientId, appOptions.ClientSecret);

            ledColorsSeqClient = clientManager.GetClient<LedColorsSeq, LedColorsSeqData>("cp-led-color-sequence");
            collectionPointsData = clientManager.GetClient<CollectionPoint, CollectionPointData>("collection-points");
        }

        public async Task<CollectionPoint> GetCollectionPointsById(int cpId)
        {
            var data = await collectionPointsData.GetAsync();//$"filter = contains(data/pointID/iv eq '{cpId}') eq");
            //var refId = Guid.Parse("xxx...");
            return data.Items.Where(d => d.Data.PointId.Iv == cpId).FirstOrDefault();
        }

        public async Task<LedColorsSeq> GetLedColors()
        {
            try
            {
                var colors = await ledColorsSeqClient.GetAllAsync();
                return colors.Items.FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw;
            }
            return null;
        }
    }
}
