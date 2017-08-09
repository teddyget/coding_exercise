using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.Model;

namespace StockAnalysis.Analysis
{
    public interface IParseRawStockData
    {
        IEnumerable<DailySummary> ExtractDailyData(string url);
        IDictionary<string, IList<DailySummary>> ExtractTickerData(IEnumerable<DailySummary> DailyData);
    }
}
