using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Bot_Application3.Common
{
    public class MSDateUtil
    {


        public static DateTime? getBegin(String date)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            //2017-02-07
            Regex reg = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            if (reg.IsMatch(date))
                return Convert.ToDateTime(date, dtFormat);
            //2017
            reg = new Regex(@"^\d{4}$");
            if (reg.IsMatch(date))
                return Convert.ToDateTime(date + "-01-01", dtFormat);
            //2017-10
            reg = new Regex(@"^\d{4}-\d{2}$");
            if (reg.IsMatch(date))
                return Convert.ToDateTime(date + "-01", dtFormat);
            //XXXX-10
            reg = new Regex(@"^XXXX-(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                //Debug.WriteLine(mat.Groups[1]);
                return Convert.ToDateTime(DateTime.Now.Year + "-" + mat.Groups[1] + "-01", dtFormat);
            }
            //XXXX-10-01
            reg = new Regex(@"^XXXX-(\d{2})-(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                //Debug.WriteLine(mat.Groups[1]);
                return Convert.ToDateTime(DateTime.Now.Year + "-" + mat.Groups[1] + "-" + mat.Groups[2], dtFormat);
            }

            //2017-W06
            //暂定 可能有问题 需要咨询微软
            reg = new Regex(@"^(\d{4})-W(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
                //Debug.WriteLine("Week" + cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
                dt = cal.AddWeeks(dt, int.Parse(mat.Groups[2].ToString()));
                //Debug.WriteLine("Week" + cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Debug.WriteLine("sunday");
                    return dt;
                }
                else
                {
                    //Debug.WriteLine("other");
                    return dt.AddDays(8 - (int)dt.DayOfWeek).AddDays(-7);                  
                }       
            }

            return null;
        }

        public static DateTime? getEnd(String date)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            //2017-02-07
            Regex reg = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            if (reg.IsMatch(date))
                return Convert.ToDateTime(date, dtFormat);
            //2017
            reg = new Regex(@"^\d{4}$");
            if (reg.IsMatch(date))
                return Convert.ToDateTime(date + "-12-31", dtFormat);
            //2017-10
            reg = new Regex(@"^(\d{4})-(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                DateTime d1 = new DateTime(int.Parse(mat.Groups[1].ToString()), int.Parse(mat.Groups[2].ToString()), 1);
                DateTime d2 = d1.AddMonths(1).AddDays(-1);
                return d2;
            }
            //XXXX-10
            reg = new Regex(@"^XXXX-(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                DateTime d1 = new DateTime(DateTime.Now.Year, int.Parse(mat.Groups[1].ToString()), 1);
                DateTime d2 = d1.AddMonths(1).AddDays(-1);

                return d2;
            }
            //XXXX-10-01
            reg = new Regex(@"^XXXX-(\d{2})-(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                //Debug.WriteLine(mat.Groups[1]);
                return Convert.ToDateTime(DateTime.Now.Year + "-" + mat.Groups[1] + "-" + mat.Groups[2], dtFormat);
            }
            //2017-W06
            //暂定 可能有问题 需要咨询微软
            reg = new Regex(@"^(\d{4})-W(\d{2})$");
            if (reg.IsMatch(date))
            {
                var mat = reg.Match(date);
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
                //Debug.WriteLine("Week" + cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
                dt = cal.AddWeeks(dt, int.Parse(mat.Groups[2].ToString()));
                //Debug.WriteLine("Week" + cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Debug.WriteLine("sunday");
                    return dt.AddDays(6);
                }
                else
                {
                    //Debug.WriteLine("other");
                    return dt.AddDays(8 - (int)dt.DayOfWeek).AddDays(-1);
                }
            }

            return null;
        }




    }
}