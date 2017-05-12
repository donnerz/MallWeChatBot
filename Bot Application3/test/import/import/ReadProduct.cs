using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Test
{
    public class ReadProduct
    {

        public static List<string> list;
        public static String path;

        public ReadProduct()
        {
            path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "File/product.txt");
            list = GetList(path);
        }

        public List<string> GetList(string sPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPath))
                {
                    return null;
                }

                List<string> list = new List<string>();

                string[] arrlines = System.IO.File.ReadAllLines(sPath, Encoding.UTF8);

                if (arrlines == null || arrlines.Length == 0)
                {
                    return null;
                }

                foreach (string str in arrlines)
                {
                    list.Add(str);
                }
                return list;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        
      

    }
}