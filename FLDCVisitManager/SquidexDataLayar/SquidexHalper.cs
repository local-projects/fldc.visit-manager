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
        private static SquidexClient<LedColorsSeq, LedColorsSeqData> ledColorsSeq;


        public SquidexHalper()
        {

        }

        public void ConnectToCMS(AppOptions appOptions)
        {
            var clientManager = new SquidexClientManager(appOptions.Url, appOptions.AppName, appOptions.ClientId, appOptions.ClientSecret);

            ledColorsSeq = clientManager.GetClient<LedColorsSeq, LedColorsSeqData>("cp-led-color-sequence");
        }

        public async Task<LedColorsSeq> GetLedColors()
        {
            try
            {
                var colors = await ledColorsSeq.GetAllAsync();
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
