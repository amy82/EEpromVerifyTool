using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl.Data
{
    public class _SerialPort
    {
        public string Bcr { get; set; }
    }

    public class ConfigData
    {
        public _SerialPort SerialPort { get; set; }
    }
}
