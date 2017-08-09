using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysis.Analysis
{
    public class DataAnalysis : IDataAnalysis
    {
        private readonly IParseRawStockData _parseRawData;

        public DataAnalysis(IParseRawStockData parseRawData)
        {
            _parseRawData = parseRawData;
        }

        /// <summary>
        /// Call functions to get data and calculate Monthly average open and close price
        /// </summary>
        /// <param name="url">Quandl api url</param>
        /// <returns>Monthly Average Result</returns>
        public string AverageMontlyOpenClosePrice(string url)
        {
            var dailyData = _parseRawData.ExtractDailyData(url);
            var tickerData = _parseRawData.ExtractTickerData(dailyData);

            return Calculations.CalculateMonthlyAverage(tickerData);
        }

        /// <summary>
        /// call functions to get data and calculate maxt profit date
        /// </summary>
        /// <param name="url">Quandl api url</param>
        /// <returns>Max profit date data</returns>
        public string MaxDailyProfit(string url)
        {
            var dailyData = _parseRawData.ExtractDailyData(url);
            var tickerData = _parseRawData.ExtractTickerData(dailyData);

            return Calculations.CalculateHighestProfitDay(tickerData);
        }
    }
}
