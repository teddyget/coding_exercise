using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalysis.Model;

namespace StockAnalysis.RetrieveData
{
    /// <summary>
    /// Interface for Quandl API Client Handler.
    /// </summary>
    public interface IStockDataRepository
    {
        Task<RawQuandlData.StockData> GetRawData(string url);

    }

}
