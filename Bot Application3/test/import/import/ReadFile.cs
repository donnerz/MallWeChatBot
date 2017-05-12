
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Test
{
    public class ReadFile
    {

        public static Dictionary<string, Brand> brandDic;
        public static String path;

        public ReadFile ()
        {
            path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "File/eqeq.txt");
            brandDic = GetDicBrand(path);
        }

        public Dictionary<string, Brand> GetDicBrand(string sPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPath))
                {
                    return null;
                }

                Dictionary<string, Brand> dicBrand = new Dictionary<string, Brand>();

                string[] arrlines = System.IO.File.ReadAllLines(sPath, Encoding.UTF8);

                if (arrlines == null || arrlines.Length == 0)
                {
                    return null;
                }

                foreach (string str in arrlines)
                {
                    Brand objBrand = GetBrand(str);
                    if (objBrand == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(objBrand.BrandName))
                        continue;
                    if (dicBrand.ContainsKey(objBrand.BrandName))
                    {
                        dicBrand[objBrand.BrandName] = objBrand;
                    }
                    else
                    {
                        dicBrand.Add(objBrand.BrandName, objBrand);
                    }
                }
                return dicBrand;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Brand GetBrand(string sInput)
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
            if (arr.Length < 13)
            {
                return null;
            }

            return new Brand
            {
                Id = arr[0] == null ? string.Empty : arr[0].Trim(),
                UnitNumber = arr[1] == null ? string.Empty : arr[1].Trim(),
                Floor = arr[2] == null ? string.Empty : arr[2].Trim(),
                BrandCode = arr[3] == null ? string.Empty : arr[3].Trim(),
                BrandName = arr[4] == null ? string.Empty : arr[4].Trim(),
                RetailType = arr[5] == null ? string.Empty : arr[5].Trim(),
                FirstBusiness = arr[6] == null ? string.Empty : arr[6].Trim(),
                SecondBusiness = arr[7] == null ? string.Empty : arr[7].Trim(),
                ThirdBusiness = arr[8] == null ? string.Empty : arr[8].Trim(),
                VipFee = arr[9] == null ? string.Empty : arr[9].Trim(),
                Tag1 = arr[10] == null ? string.Empty : arr[10].Trim(),
                Tag2 = arr[11] == null ? string.Empty : arr[11].Trim(),
                Tag3 = arr[12] == null ? string.Empty : arr[12].Trim(),
                Tag4 = arr[13] == null ? string.Empty : arr[13].Trim(),
            };
        }

        public static List<Brand> GetBrandList(String bis, String floor, bool vipFlag)
        {
            List<Brand> brandList = new List<Brand>();
            foreach (Brand brand in brandDic.Values)
            {
                Brand b = brand;

                if (vipFlag)
                {
                    if (!"特约商户".Equals(b.VipFee))
                    {
                        continue;
                    }
                }

                bool bisFlag = true;
                bool floorFlag = true;
                if (!String.IsNullOrWhiteSpace(bis))
                {
                    bisFlag = (b.SecondBusiness + b.ThirdBusiness).Contains(bis);
                }
                if (!String.IsNullOrWhiteSpace(floor))
                {
                    floorFlag = floor.Equals(b.Floor);
                }
                if (bisFlag && floorFlag)
                {
                    brandList.Add(b);
                }
                if (brandList.Count >= 5)
                {
                    break;
                }
            }
            return brandList;
        }

        public static List<Brand> GetAllBrandList(bool vipFlag)
        {
            List<Brand> brandList = new List<Brand>();
            foreach (Brand brand in brandDic.Values)
            {
                Brand b = brand;

                if (vipFlag)
                {
                    if (!"特约商户".Equals(b.VipFee))
                    {
                        continue;
                    }
                }

                brandList.Add(b);
                if (brandList.Count >= 5)
                {
                    break;
                }
            }
            return brandList;
        }

    }
}