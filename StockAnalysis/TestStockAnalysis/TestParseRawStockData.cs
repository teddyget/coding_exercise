using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockAnalysis.RetrieveData;
using StockAnalysis.Analysis;
using StockAnalysis.Model;
using System.Threading.Tasks;
using System.Linq;
using static StockAnalysis.Model.RawQuandlData;
using System.Net.Http;

namespace TestStockAnalysis
{
    /// <summary>
    /// Test the two function which are used to parse raw json stock data into C# object
    /// </summary>
    [TestClass]
    public class TestParseRawStockData
    {       
        [TestMethod]
        public void Test_ParseRawStockData_Returns_Parsed_Data()
        {
            //Arrange

            Mock<IStockDataRepository> mockStockDataRepository = new Mock<IStockDataRepository>();

            IParseRawStockData parseRawData = new ParseRawStockData(mockStockDataRepository.Object);

            var dt = new Datatable
            {
                data = new List<List<object>>(),
                columns = new List<Column>()
            };

            StockData stockData = new StockData()
            {
                datatable = new Datatable
                {
                    data = new List<List<object>>
                    {
                        new List<object> { "MSFT", "2017-01-03", 62.79, 62.84, 62.125, 62.58, 20694101.0, 0.0, 1.0, 62.064301929466, 62.113724052359, 61.406987694984, 61.856729013314, 20694101.0 },
                        new List<object> { "MSFT", "2017-01-04", 62.48, 62.75, 62.12, 62.3, 21339969.0, 0.0, 1.0, 61.757884767527, 62.024764231151, 61.402045482695, 61.579965125111, 21339969.0},
                        new List<object> { "MSFT", "2017-01-05", 62.19, 62.66, 62.03, 62.3, 24875968.0, 0.0, 1.0, 61.471236454746, 61.935804409943, 61.313085661487, 61.579965125111, 24875968.0},
                        new List<object> { "MSFT", "2017-01-06", 62.3, 63.15, 62.04, 62.84, 19922919.0, 0.0, 1.0, 61.579965125111, 62.420141214298, 61.322970086066, 62.113724052359, 19922919.0},
                        new List<object> { "MSFT", "2017-01-09", 62.76, 63.08, 62.54, 62.64, 20382730.0, 0.0, 1.0, 62.03464865573, 62.350950242247, 61.817191314999, 61.916035560786, 20382730.0},
                        new List<object> { "MSFT", "2017-01-10", 62.73, 63.07, 62.28, 62.62, 18593004.0, 0.0, 1.0, 62.004995381994, 62.341065817669, 61.560196275954, 61.896266711629, 18593004.0},
                        new List<object> { "MSFT", "2017-01-11", 62.61, 63.23, 62.43, 63.19, 21517335.0, 0.0, 1.0, 61.88638228705, 62.499216610927, 61.708462644634, 62.459678912613, 21517335.0 },
                    },
                    columns = new List<Column>()
                },
                meta = new Meta()
            };

            var expectedResult = stockData.datatable.data.Select(x => new DailySummary(x)).ToList();

            mockStockDataRepository.Setup(x => x.GetRawData(It.IsAny<string>())).Returns(Task.FromResult(stockData));

            //Act
            var result = parseRawData.ExtractDailyData("http://localhost:8000").ToList();

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(result.Count(), expectedResult.Count());

            for(int i =0; i < expectedResult.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i].Ticker, result[i].Ticker);
                Assert.AreEqual(expectedResult[i].Date, result[i].Date);
                Assert.AreEqual(expectedResult[i].Open, result[i].Open);
                Assert.AreEqual(expectedResult[i].High, result[i].High);
                Assert.AreEqual(expectedResult[i].Low, result[i].Low);
                Assert.AreEqual(expectedResult[i].Close, result[i].Close);
                Assert.AreEqual(expectedResult[i].Volume, result[i].Volume);
                Assert.AreEqual(expectedResult[i].Ex_Dividend, result[i].Ex_Dividend);
                Assert.AreEqual(expectedResult[i].Split_Ratio, result[i].Split_Ratio);
                Assert.AreEqual(expectedResult[i].Adj_Open, result[i].Adj_Open);
                Assert.AreEqual(expectedResult[i].Adj_High, result[i].Adj_High);
                Assert.AreEqual(expectedResult[i].Adj_Low, result[i].Adj_Low);
                Assert.AreEqual(expectedResult[i].Adj_Close, result[i].Adj_Close);
                Assert.AreEqual(expectedResult[i].Adj_Volume, result[i].Adj_Volume);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void Test_ParseRawStockData_ThrowsException()
        {
            //Arrange
            Mock<IStockDataRepository> mockStockDataRepository = new Mock<IStockDataRepository>();

            IParseRawStockData parseRawData = new ParseRawStockData(mockStockDataRepository.Object);

            mockStockDataRepository.Setup(x => x.GetRawData(It.IsAny<string>())).Throws(new HttpRequestException("Oops, error occured."));

            //Act
            var result = parseRawData.ExtractDailyData("http://localhost:8000");

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Test_ExtractTickerData_Returns_Valid_Ticker_Data()
        {
            //Arrange
            Mock<IStockDataRepository> mockStockDataRepository = new Mock<IStockDataRepository>();

            IParseRawStockData parseRawData = new ParseRawStockData(mockStockDataRepository.Object);

            var dt = new Datatable
            {
                data = new List<List<object>>(),
                columns = new List<Column>()
            };

            StockData stockData = new StockData()
            {
                datatable = new Datatable
                {
                    data = new List<List<object>>
                    {
                new List<object> { "COF", "2017-01-03", 88.55, 89.6, 87.79, 88.87, 3441067.0, 0.0, 1.0, 87.30123847595, 88.336431027048, 86.551956248488, 87.616725729618, 3441067.0 },
                new List<object> { "COF", "2017-01-04", 89.13, 90.77, 89.13, 90.3, 2630905.0, 0.0, 1.0, 87.873059123223, 89.489931298271, 87.873059123223, 89.026559394447, 2630905.0 },
                new List<object> { "MSFT", "2017-01-03", 62.79, 62.84, 62.125, 62.58, 20694101.0, 0.0, 1.0, 62.064301929466, 62.113724052359, 61.406987694984, 61.856729013314, 20694101.0 },
                new List<object> { "MSFT", "2017-01-04", 62.48, 62.75, 62.12, 62.3, 21339969.0, 0.0, 1.0, 61.757884767527, 62.024764231151, 61.402045482695, 61.579965125111, 21339969.0}
                    },
                    columns = new List<Column>()
                },
                meta = new Meta()
            };
            var dailyData = stockData.datatable.data.Select(x => new DailySummary(x)).ToList();

            //expected result
            IDictionary<string, IList<DailySummary>> expectedResult = new Dictionary<string, IList<DailySummary>>();
            var msData = new List<DailySummary> {
                new DailySummary(new List<object> {"MSFT", "2017-01-03", 62.79, 62.84, 62.125, 62.58, 20694101.0, 0.0, 1.0, 62.064301929466, 62.113724052359, 61.406987694984, 61.856729013314, 20694101.0  }),
                new DailySummary(new List<object> {"MSFT", "2017-01-04", 62.48, 62.75, 62.12, 62.3, 21339969.0, 0.0, 1.0, 61.757884767527, 62.024764231151, 61.402045482695, 61.579965125111, 21339969.0  }) };
            var cofData = new List<DailySummary> {
                new DailySummary(new List<object> {"COF", "2017-01-03", 88.55, 89.6, 87.79, 88.87, 3441067.0, 0.0, 1.0, 87.30123847595, 88.336431027048, 86.551956248488, 87.616725729618, 3441067.0  }),
                new DailySummary(new List<object> {"COF", "2017-01-04", 89.13, 90.77, 89.13, 90.3, 2630905.0, 0.0, 1.0, 87.873059123223, 89.489931298271, 87.873059123223, 89.026559394447, 2630905.0  }) };
            expectedResult.Add("COF", cofData);
            expectedResult.Add("MSFT", msData);


            //Act
            var actualResult = parseRawData.ExtractTickerData(dailyData);

            //Assert
            Assert.AreEqual(expectedResult.Count(), actualResult.Count());
        }
    }
}
