using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.RetrieveData;
using Newtonsoft.Json;
using StockAnalysis.Model;

namespace StockAnalysis.Analysis
{
    public static class Calculations
    {
        /// <summary>
        /// Calculate the Average open and close price for each symbols
        /// </summary>
        /// <param name="tickerData">Daily Data grouped by its ticker</param>
        /// <returns>Monthly Average Result</returns>
        public static string CalculateMonthlyAverage(IDictionary<string, IList<DailySummary>> tickerData)
        {
            //Create Monthly summary list for each ticker
            var tickerMonthlyAverageData = new Dictionary<string, List<MonthlySummary>>();

            foreach (KeyValuePair<string, IList<DailySummary>> item in tickerData)
            {
                var monthlySummaryData = new Dictionary<string, MonthlySummary>();
                foreach (var data in item.Value)
                {
                    string yearMonth = data.Date.ToString("yyyy-MM");
                    if (monthlySummaryData.ContainsKey(yearMonth))
                    {
                        monthlySummaryData[yearMonth].OpenSum += data.Open;
                        monthlySummaryData[yearMonth].CloseSum += data.Close;
                        monthlySummaryData[yearMonth].Count += 1;
                    }
                    else
                    {
                        monthlySummaryData[yearMonth] = new MonthlySummary
                        {
                            month = yearMonth,
                            OpenSum = data.Open,
                            CloseSum = data.Close,
                            Count = 1
                        };
                    }
                }

                // now display //ticker - year-month - avgOpen -- avgClose
                foreach (var monthData in monthlySummaryData)
                {
                    if (tickerMonthlyAverageData.ContainsKey(item.Key))
                    {
                        tickerMonthlyAverageData[item.Key].Add(monthData.Value);
                    }
                    else
                    {
                        tickerMonthlyAverageData[item.Key] = new List<MonthlySummary> { monthData.Value };
                    }
                }
            }
            //Return Readable/formatted json
            return JsonConvert.SerializeObject(tickerMonthlyAverageData,Formatting.Indented);
        }

        /// <summary>
        /// Find max profit date for each ticker when bought at the lowest and sold at the highest price of the day
        /// </summary>
        /// <param name="tickerData">Daily Data grouped by its ticker</param>
        /// <returns>Max profit date data for each ticker</returns>
        public static string CalculateHighestProfitDay(IDictionary<string, IList<DailySummary>> tickerData)
        {
            var maxProfitData = new Dictionary<string, MaxProfit>();

            foreach (KeyValuePair<string, IList<DailySummary>> dailyData in tickerData)
            {
                foreach (var data in dailyData.Value)
                {
                    string ticker = data.Ticker;
                    DateTime date = data.Date;
                    decimal dailyHigh = data.High;
                    decimal dailyLow = data.Low;
                    decimal dailyProfit = decimal.Round(dailyHigh - dailyLow, 2);

                    if (maxProfitData.ContainsKey(ticker))
                    {
                        if (dailyProfit > maxProfitData[ticker].max_profit)
                        {
                            maxProfitData[ticker].max_profit = dailyProfit;
                            maxProfitData[ticker].Date = date.ToString("yyyy-MM-dd");
                            maxProfitData[ticker].low = dailyLow;
                            maxProfitData[ticker].high = dailyHigh;
                        }
                    }
                    else
                    {
                        maxProfitData[ticker] = new MaxProfit
                        {
                            Date = data.Date.ToString("yyyy-MM-dd"),
                            low = data.Low,
                            high = data.High,
                            max_profit = dailyProfit
                        };
                    }
                }
            }
            //Return Readable/formatted max profit result
            return JsonConvert.SerializeObject(maxProfitData, Formatting.Indented);
        }

    }
}
