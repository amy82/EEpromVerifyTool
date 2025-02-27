﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl.Data
{
    public class Alarm
    {
        public int No { get; set; }
        public string Time { get; set; }
        public string Details { get; set; }
    }

    public class AlarmData
    {
        public List<Alarm> Alarms { get; set; } = new List<Alarm>();
    }
}
