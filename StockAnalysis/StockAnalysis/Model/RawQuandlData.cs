using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysis.Model
{
    public class RawQuandlData
    {
        public class Column
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        public class Datatable
        {
            public List<List<object>> data { get; set; }
            public List<Column> columns { get; set; }
        }

        public class Meta
        {
            public object next_cursor_id { get; set; }
        }

        public class StockData
        {
            public Datatable datatable { get; set; }
            public Meta meta { get; set; }
        }
    }
}
