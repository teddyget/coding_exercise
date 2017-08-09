using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.Model;
using StockAnalysis.RetrieveData;

namespace StockAnalysis.Analysis
{
    public class ParseRawStockData : IParseRawStockData
    {
        private readonly IStockDataRepository _stockDataRepository;
        public ParseRawStockData(IStockDataRepository stockDataReposiotry)
        {
            _stockDataRepository = stockDataReposiotry;
        }

        /// <summary>
        /// Retrieve all data for the specified ticker and timeframe
        /// </summary>
        /// <param name="url">Quandl api url</param>
        /// <returns>Daily data extracted from api response</returns>
        public IEnumerable<DailySummary> ExtractDailyData(string url)
        {
            var rawData = _stockDataRepository.GetRawData(url).Result;

            //capture the actual daily data
            List<DailySummary> dataItems = new List<DailySummary>();

            foreach (var item in rawData.datatable.data)
            {
                dataItems.Add(new DailySummary(item));
            }
            return dataItems;
        }

        /// <summary>
        /// Organize daily data by each individual ticker
        /// </summary>
        /// <param name="DailyData">Daily data extracted from raw api response</param>
        /// <returns>Daily data extracted from api response</returns>
        public IDictionary<string, IList<DailySummary>> ExtractTickerData(IEnumerable<DailySummary> DailyData)
        {
            //group/separate daily data by ticker
            IDictionary<string, IList<DailySummary>> tickerData = new Dictionary<string, IList<DailySummary>>();

            foreach (var dataItem in DailyData)
            {
                if (tickerData.ContainsKey(dataItem.Ticker))
                {
                    tickerData[dataItem.Ticker].Add(dataItem);
                }
                else
                {
                    tickerData[dataItem.Ticker] = new List<DailySummary> { dataItem };
                }
            }
            return tickerData;
        }

    }
}
