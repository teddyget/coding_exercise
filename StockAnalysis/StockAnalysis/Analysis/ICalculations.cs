using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.Model;

namespace StockAnalysis.Analysis
{
    public interface ICalculations
    {
        string CalculateMonthlyAverage(IDictionary<string, IList<DailySummary>> tickerData);
        string CalculateHighestProfitDay(IDictionary<string, IList<DailySummary>> tickerData);
    }
}
