using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.Analysis;
using StockAnalysis.RetrieveData;

namespace StockAnalysis
{
    public class AnalysisApp
    {
        public static void Main(string[] args)
        {
            IStockDataRepository stockDataRepository = new StockDataRepository();
            IParseRawStockData parseRawData = new ParseRawStockData(stockDataRepository);
 
            //Specify stock symbol and time frame
            string[] ticker = new string[] { "COF", "GOOGL", "MSFT" };
            string startDate = "2017-01-01";
            string endDate = "2017-06-30";
            string apiKey = "PUT YOUR OWN API KEY";

            //Quandl api url for the specified time and tickers
            string quandlUrl = "https://www.quandl.com/api/v3/datatables/WIKI/PRICES.json?" +
                               "date.gte=" + startDate + "&" +
                               "date.lte=" + endDate + "&" +
                               "ticker=" + string.Join(",", ticker) + "&" +
                               "api_key=" + apiKey;

            string errorMessage = "Please, select from the available choices.  To continue, press the -Enter- key";
            string continueMessage = "Please press the -Enter- key to continue";
            int selection = 0;
            bool validSelection = false;
            while(selection != 3)
            {
                //Display options for the user
                Console.WriteLine($"Please select from the following options for stock data analysis.\n" +
                                  $" \n" +
                                  $"[1] - Monthly Average Open and Close Prices\n" +
                                  $"[2] - Max profit date if bought at low and sold at highest price of the day\n" +
                                  $"[3] - QUIT");
                //Display stock analysis based on the user's option
                validSelection = int.TryParse(Console.ReadLine(), out selection);
                if (validSelection)
                {
                    DataAnalysis dataAnalysis = new DataAnalysis(parseRawData);
                    switch (selection)
                    {
                        case 1:
                            Console.WriteLine(dataAnalysis.AverageMontlyOpenClosePrice(quandlUrl));
                            Console.WriteLine(continueMessage);
                            break;
                        case 2:
                            Console.WriteLine(dataAnalysis.MaxDailyProfit(quandlUrl));
                            Console.WriteLine(continueMessage);
                            break;
                        case 3:
                            Console.WriteLine("Quiting app . . . Press -Enter- to Quit.");
                            break;
                        default:
                            Console.WriteLine(errorMessage);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
                Console.ReadKey();
            }
           
        }
    }
}
