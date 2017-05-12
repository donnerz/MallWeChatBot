using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application3.Model.Vo
{
    public class BrandVo
    {
        public string Id { get; set; }

        /// <summary>
        /// 单元号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        public string BrandCode { get; set; }

        public string BrandName { get; set; }

        /// <summary>
        /// 零售/非零售
        /// </summary>
        public string RetailType { get; set; }

        public string FirstBusiness { get; set; }

        public string SecondBusiness { get; set; }

        public string ThirdBusiness { get; set; }

        /// <summary>
        /// 是否有VIP手续费
        /// </summary>
        public string VipFee { get; set; }

        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Tag4 { get; set; }


        public override string ToString()
        {
            return "Id:" + Id + " UnitNumber:" + UnitNumber + " Floor:" + Floor + " BrandCode:" + BrandCode + " BrandName:" + BrandName + " RetailType:" + RetailType +
                " FirstBusiness:" + FirstBusiness + " SecondBusiness:" + SecondBusiness + " ThirdBusiness:" + ThirdBusiness + " VipFee:" + VipFee;
        }

    }
}