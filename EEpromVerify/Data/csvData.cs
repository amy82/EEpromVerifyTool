using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ApsMotionControl.Data
{
    public class EEpromCsvData
    {
        public string SHOPID { get; set; }
        public string PRODID { get; set; }
        public string PROCID { get; set; }
        public string EEP_ITEM { get; set; }
        public int ADDRESS { get; set; }
        public int DATA_SIZE { get; set; }
        public string DATA_FORMAT { get; set; }
        public string BYTE_ORDER { get; set; }
        public string FIX_YN { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_VALUE { get; set; }
        public string CRC_START { get; set; }
        public string CRC_END { get; set; }
        public string PAD_VALUE { get; set; }
        public string PAD_POSITION { get; set; }

    }
}
