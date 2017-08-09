using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockAnalysis.Model
{
    public class MonthlySummary
    {
        public string month { get; set; }
        [JsonIgnore]
        public decimal OpenSum { get; set; }
        [JsonIgnore]
        public decimal CloseSum { get; set; }
        [JsonIgnore]
        public int Count { get; set; }

        public decimal average_open
        {
            get
            {
                if (Count == 0)
                {
                    return 0;
                }
                return decimal.Round(OpenSum / Count, 2);
            }
        }
        public decimal average_cLose
        {
            get
            {
                if (Count == 0)
                {
                    return 0;
                }
                return decimal.Round(CloseSum / Count, 2);
            }
        }
    }
}
