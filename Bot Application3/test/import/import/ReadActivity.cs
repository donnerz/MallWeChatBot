using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Test
{
    public class ReadActivity
    {

        public static Dictionary<string, Activity> dic;
        public static String path;

        public ReadActivity()
        {
            path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "File/activity.txt");
            dic = GetDic(path);
        }

        public Dictionary<string, Activity> GetDic(string sPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPath))
                {
                    return null;
                }

                Dictionary<string, Activity> tmpDic = new Dictionary<string, Activity>();

                string[] arrlines = System.IO.File.ReadAllLines(sPath, Encoding.UTF8);

                if (arrlines == null || arrlines.Length == 0)
                {
                    return null;
                }

                foreach (string str in arrlines)
                {
                    Activity obj = GetDomain(str);
                    if (obj == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(obj.Word))
                        continue;
                    if (tmpDic.ContainsKey(obj.Word))
                    {
                        tmpDic[obj.Word] = obj;
                    }
                    else
                    {
                        tmpDic.Add(obj.Word, obj);
                    }
                }
                return tmpDic;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Activity GetDomain(string sInput)
        {
            if (string.IsNullOrEmpty(sInput))
            { 
                return null;
            }

            string[] arr = sInput.Split(',');

            if (arr == null || arr.Length == 0)
            {
                return null;
            }

            //列默认10 不满足条件 排除
            if (arr.Length < 6)
            {
                return null;
            }

            return new Activity
            {
                Word = arr[0] == null ? string.Empty : arr[0].Trim(),
                Remark = arr[1] == null ? string.Empty : arr[1].Trim(),
                Floor = arr[2] == null ? string.Empty : arr[2].Trim(),
                BasicInfo = arr[3] == null ? string.Empty : arr[3].Trim(),
                Begin = arr[4] == null ? string.Empty : arr[4].Trim(),
                End = arr[5] == null ? string.Empty : arr[5].Trim(),

            };
        }

      

    }
}