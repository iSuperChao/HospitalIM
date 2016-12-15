using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientView
{
    public class ChatState
    {
        public DateTime FirstChatTime { get; set; }//首次聊天的时间
        public DateTime LastChatTime { get; set; }//最近聊天的时间
        public int ChatCount { get; set; }//本次总发话数
        public int ChatCountFromLastForbiden { get; set; }//自上次限制说话后到出在共发话数
        public Boolean isForbiden { get; set; }//当前是否正在限制说话
    }
}
