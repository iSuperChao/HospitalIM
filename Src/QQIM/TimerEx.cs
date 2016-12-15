using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientView
{
    class TimerEx : System.Timers.Timer
    {
        #region 构造函数
        
        public TimerEx() : base() { }
        public TimerEx(double interval) : base(interval) { }

        #endregion

        #region 属性

        public object Tag { get; set; }

        #endregion
    }
}
