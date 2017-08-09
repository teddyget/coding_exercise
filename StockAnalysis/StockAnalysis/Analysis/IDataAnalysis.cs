using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysis.Analysis
{
    public interface IDataAnalysis
    {
        string AverageMontlyOpenClosePrice(string url);
        string MaxDailyProfit(string url);
    }
}
