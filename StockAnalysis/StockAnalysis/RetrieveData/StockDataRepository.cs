using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using StockAnalysis.Model;
using Newtonsoft.Json;

namespace StockAnalysis.RetrieveData
{
    public class StockDataRepository : IStockDataRepository
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        /// <summary>
        /// Retrieve raw stock data using Quandl api call.
        /// </summary>
        /// <param name="url">Quandl API url</param>
        /// <returns>RawQuandlData object</returns>
        public  async Task<RawQuandlData.StockData> GetRawData(string url)
        {
            try
            {
                string response = await _httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<RawQuandlData.StockData>(response);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

    }
}
