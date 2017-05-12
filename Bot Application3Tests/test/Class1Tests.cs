using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bot_Application3.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Diagnostics;
using Service;
using Bot_Application3.Model;
using Bot_Application3.Common;
using Dao;
using System.Globalization;

namespace Bot_Application3.test.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void test1Test()
        {
            //BaseInfo bi = new BaseInfo();

            //Debug.WriteLine(BaseInfo.wordDic["1"].name + BaseInfo.synonymDic["1"].name + BaseInfo.wordDic[BaseInfo.brandDic["1"].brand].name);

            //Class1 c1 = new Class1();
            //c1.select();

            BaseInfo bi = new BaseInfo();
            bi.init();

            SearchService ss = new SearchService();

            //List<t_activity>  list = ss.GetActivityListByBasciInfoAndFloor("黄金大道", "", DateTime.Now.ToString("yyyy-MM-dd"));

            List<t_brand> list = ss.GetBrandListByProductAndFloor("咖啡", null, false);

            Debug.WriteLine(list.Count);

            foreach(t_brand b in list)
            {
                Debug.WriteLine(b.brand);
            }
            

            //t_brand brand = ss.getBrandByName("星巴克");

            //Debug.WriteLine("code = " + brand.code + " brand = " + brand.brand + " floor = " + brand.floor + " house_number = " + brand.house_number + " vip = " + brand.vip_flag);

            //t_brand_businessformat brand_businessformat = ss.getBusinessformatByBrandAndBf(brand.code, "15")[0];

            //Debug.WriteLine("id = " + brand_businessformat.id);
        }

        [TestMethod()]
        public void getBrandByName()
        {
            BaseInfo bi = new BaseInfo();
            bi.init();

        }

        [TestMethod()]
        public void getDataByCondition()
        {
            String builtinDatetimeDateResolution = DateTime.Now.ToString("yyyy-MM-dd");

            builtinDatetimeDateResolution = "2017-01";

            List<t_activity> activityList = new List<t_activity>();
            //楼层
            String basic_info_code = String.Empty;
            //楼层
            String floor_code = String.Empty;
            ActivityDao activityDao = new ActivityDao();
            activityList = activityDao.getDataByCondition(basic_info_code, floor_code, MSDateUtil.getBegin(builtinDatetimeDateResolution), MSDateUtil.getEnd(builtinDatetimeDateResolution));

            //activityList = activityDao.getDataByCondition(basic_info_code, floor_code, MSDateUtil.getBegin(builtinDatetimeDateResolution), null);

            Debug.WriteLine("count = " + activityList.Count);


        }

        [TestMethod()]
        public void tttt()
        {
            String s = "XXXX-03-03";
            DateTime? dt = MSDateUtil.getBegin(s);
            if (dt.HasValue)
                Debug.WriteLine(dt.ToString());
            else
                Debug.WriteLine("null");

            DateTime? dt1 = MSDateUtil.getEnd(s);
            if (dt1.HasValue)
                Debug.WriteLine(dt1.ToString());
            else
                Debug.WriteLine("null");

            Debug.WriteLine(DateTime.Now.DayOfWeek);

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime dt11 = new DateTime(2016, 1, 1);
            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay ,dfi.FirstDayOfWeek));
            Debug.WriteLine("Week" + cal.GetWeekOfYear(dt11, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
            dt11 = cal.AddWeeks(dt11 , 5);
            Debug.WriteLine("Week" + cal.GetWeekOfYear(dt11, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));

            if (dt11.DayOfWeek == DayOfWeek.Sunday)
            {
                Debug.WriteLine("sunday");
                Debug.WriteLine(dt11.ToString("yyyy/MM/dd"));
                Debug.WriteLine(dt11.AddDays(6).ToString("yyyy/MM/dd"));
            }

            else
            {
                Debug.WriteLine("other");
                Debug.WriteLine(dt11.AddDays(8 - (int)dt11.DayOfWeek).AddDays(-7).ToString("yyyy/MM/dd"));
                Debug.WriteLine(dt11.AddDays(8 - (int)dt11.DayOfWeek).AddDays(-1).ToString("yyyy/MM/dd"));
            }
                


            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday));


            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, dfi.FirstDayOfWeek));
            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday));
            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday));

            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, dfi.FirstDayOfWeek));
            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday));
            //Debug.WriteLine("Week" + cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday));

        }
    }
}