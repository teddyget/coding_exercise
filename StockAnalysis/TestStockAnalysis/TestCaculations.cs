using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StockAnalysis.Model;
using StockAnalysis.Analysis;
using Newtonsoft.Json;

namespace TestStockAnalysis
{
    [TestClass]
    public class TestCaculations
    {
        [TestMethod]
        public void Test_CalculateMonthlyAverage_Returns_Correct_Result()
        {
            //Arrange
            //input
            IDictionary<string, IList<DailySummary>> stockData = new Dictionary<string, IList<DailySummary>>();
            var msData = new List<DailySummary> {
                new DailySummary(new List<object> {"MSFT", "2017-01-03", 62.79, 62.84, 62.125, 62.58, 20694101.0, 0.0, 1.0, 62.064301929466, 62.113724052359, 61.406987694984, 61.856729013314, 20694101.0  }),
                new DailySummary(new List<object> {"MSFT", "2017-01-04", 62.48, 62.75, 62.12, 62.3, 21339969.0, 0.0, 1.0, 61.757884767527, 62.024764231151, 61.402045482695, 61.579965125111, 21339969.0  }) };
            stockData.Add("MSFT", msData);

            //output
            var tickerMonthlyAverageData = new Dictionary<string, List<MonthlySummary>>();
            tickerMonthlyAverageData.Add("MSFT", new List<MonthlySummary> { new MonthlySummary {
                CloseSum=124.88m,
                Count=2,
                month="2017-01",
                OpenSum=125.27m
            } });

            var expected = JsonConvert.SerializeObject(tickerMonthlyAverageData, Formatting.Indented);

            //Act
            var result = Calculations.CalculateMonthlyAverage(stockData);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_CalculateHighestProfitDay_Returns_Correct_Result()
        {
            //Arrange
            //input
            IDictionary<string, IList<DailySummary>> stockData = new Dictionary<string, IList<DailySummary>>();
            var cofData = new List<DailySummary> {
                new DailySummary(new List<object> {"COF", "2017-01-03", 88.55, 89.6, 87.79, 88.87, 3441067.0, 0.0, 1.0, 87.30123847595, 88.336431027048, 86.551956248488, 87.616725729618, 3441067.0  }),
                new DailySummary(new List<object> {"COF", "2017-01-04", 89.13, 90.77, 89.13, 90.3, 2630905.0, 0.0, 1.0, 87.873059123223, 89.489931298271, 87.873059123223, 89.026559394447, 2630905.0 }) };
            stockData.Add("COF", cofData);

            //output
            var tickerMaxDailyProfit = new Dictionary<string, MaxProfit>();
            tickerMaxDailyProfit.Add("COF", new MaxProfit {
                Date="2017-01-03",
                low=87.79m,
                high=89.6m,
                max_profit=1.81m
             });

            var expected = JsonConvert.SerializeObject(tickerMaxDailyProfit, Formatting.Indented);

            //Act
            var result = Calculations.CalculateHighestProfitDay(stockData);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
