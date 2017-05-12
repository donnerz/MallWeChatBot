using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test
{
    public class Activity
    {
        public string Word { get; set; }


        public string Remark { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        public string BasicInfo { get; set; }

        public string Begin { get; set; }


        public string End { get; set; }

        public override string ToString()
        {
            return "Word = " + Word + " ; Remark = " + Remark + " ; Floor = " + Floor + " ; BasicInfo = " + BasicInfo + " ; Begin = " + Begin + " ; End = " + End;
        }

    }
}