using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParserToCSV.Models
{
    public class HeaderRecordModel
    {
        public string SourceFileName { get; set; }
        public int CustomerCount { get; set; }
        public string CustomersTotalAmount { get; set; }
        public string TodaysDate { get; set; }
        public string TodaysTimestamp { get; set; }
    }
}
