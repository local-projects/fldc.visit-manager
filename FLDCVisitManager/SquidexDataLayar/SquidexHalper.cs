using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.ClientLibrary;
using SquidexDataLayer.Models;

namespace SquidexDataLayer
{
    public class SquidexHalper : IApiClient
    {
        private readonly SquidexClient<LedColorsSeq, LedColorsSeqData> ledColorsSeq;

        public SquidexHalper(SquidexAppOptions appOptions)
        {
            var clientManager = new SquidexClientManager(appOptions.Url, appOptions.AppName, appOptions.ClientId, appOptions.ClientSecret);

            //wordsClient = clientManager.GetClient<LedColorsSeq, LedColorsSeqData>("cp-led-color-sequence");
        }
/*        public async Task<LedColorsSeq> GetLedColors()
        {
            var colors = await ledColorsSeq.GetAsync();
            return colors.Items;
        }*/
    }

    public interface IApiClient
    {/*
        Task<List<LedColorsSeq>> GetLedColors();*/
    }
}
