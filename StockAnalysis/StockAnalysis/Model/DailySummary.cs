using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysis.Model
{
    public class DailySummary
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public decimal Ex_Dividend { get; set; }
        public double Split_Ratio { get; set; }
        public decimal Adj_Open { get; set; }
        public decimal Adj_High { get; set; }
        public decimal Adj_Low { get; set; }
        public decimal Adj_Close { get; set; }
        public double Adj_Volume { get; set; }

        public DailySummary(IList<object> data)
        {
            Ticker = data[0].ToString();
            Date = DateTime.Parse(data[1].ToString());
            Open = decimal.Parse(data[2].ToString());
            High = decimal.Parse(data[3].ToString());
            Low = decimal.Parse(data[4].ToString());
            Close = decimal.Parse(data[5].ToString());
            Volume = decimal.Parse(data[6].ToString());
            Ex_Dividend = decimal.Parse(data[7].ToString());
            Split_Ratio = double.Parse(data[8].ToString());
            Adj_Open = decimal.Parse(data[9].ToString());
            Adj_High = decimal.Parse(data[10].ToString());
            Adj_Low = decimal.Parse(data[11].ToString());
            Adj_Close = decimal.Parse(data[12].ToString());
            Adj_Volume = double.Parse(data[13].ToString());
        }
    }
}
