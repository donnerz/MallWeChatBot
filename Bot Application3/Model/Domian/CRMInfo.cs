using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Domian
{
    public class CRMInfo
    {
        //顾客编号
        public string customer { get; set; }
        //返回信息
        public string replyString { get; set; }


        //目的
        public string intent { get; set; }
        //品牌
        public string brandText { get; set; }
        //业态
        public string businessFormatText { get; set; }
        //楼层
        public string floorText { get; set; }
        //单品
        public string productText { get; set; }
        //基础信息
        public string basicInfoText { get; set; }
        //活动
        public string activityText { get; set; }


        //特约商户标志
        private bool vipFlag = false;
        public bool VipFlag
        {
            get
            {
                return this.vipFlag;
            }
            set
            {
                this.vipFlag = value;
            }
        }

        //builtin.datetime.date
        private string builtinDatetimeDate = "今天";
        public string BuiltinDatetimeDate
        {
            get
            {
                return this.builtinDatetimeDate;
            }
            set
            {
                this.builtinDatetimeDate = value;
            }
        }

        private string builtinDatetimeDateResolution = DateTime.Now.ToString("yyyy-MM-dd");
        public string BuiltinDatetimeDateResolution
        {
            get
            {
                return this.builtinDatetimeDateResolution;
            }
            set
            {
                this.builtinDatetimeDateResolution = value;
            }
        }

        //builtin.datetime.time
        public string builtinDatetimeTime { get; set; }
        public string builtinDatetimeTimeResolution { get; set; }

        public override string ToString()
        {
            return "customer = " + customer + " ; replyString = " + replyString + " ; intent = " + intent + " ; brandText = " + brandText + " ; businessFormatText = " + businessFormatText
                + " ; floorText = " + floorText + " ; productText = " + productText + " ; basicInfoText = " + basicInfoText + " ; activityText = " + activityText
                + " ; VipFlag = " + VipFlag + " ; BuiltinDatetimeDate = " + BuiltinDatetimeDate + " ; builtinDatetimeDateResolution = " + builtinDatetimeDateResolution
                + " ; builtinDatetimeTime = " + builtinDatetimeTime + " ; builtinDatetimeTimeResolution = " + builtinDatetimeTimeResolution;
        }


    }
}