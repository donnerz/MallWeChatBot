using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Test
{
    public class ReadBusinessformat
    {

        public static Dictionary<string, Business> dic;
        public static String path;

        public ReadBusinessformat()
        {
            path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "File/business.txt");
            dic = GetDic(path);
        }

        public Dictionary<string, Business> GetDic(string sPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPath))
                {
                    return null;
                }

                Dictionary<string, Business> tmpDic = new Dictionary<string, Business>();

                string[] arrlines = System.IO.File.ReadAllLines(sPath, Encoding.UTF8);

                if (arrlines == null || arrlines.Length == 0)
                {
                    return null;
                }

                foreach (string str in arrlines)
                {
                    Business obj = GetDomain(str);
                    if (obj == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(obj.Brand))
                        continue;
                    if (tmpDic.ContainsKey(obj.Brand))
                    {
                        tmpDic[obj.Brand] = obj;
                    }
                    else
                    {
                        tmpDic.Add(obj.Brand, obj);
                    }
                }
                return tmpDic;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Business GetDomain(string sInput)
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
            if (arr.Length < 4)
            {
                return null;
            }

            return new Business
            {
                Business1 = arr[0] == null ? string.Empty : arr[0].Trim(),
                Business2 = arr[1] == null ? string.Empty : arr[1].Trim(),
                Business3 = arr[2] == null ? string.Empty : arr[2].Trim(),
                Brand = arr[3] == null ? string.Empty : arr[3].Trim()
                

            };
        }

      

    }
}