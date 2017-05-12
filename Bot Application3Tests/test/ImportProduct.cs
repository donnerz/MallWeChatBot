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

using Dao;
using System.Data.Entity.Validation;
using Test;
using System.Globalization;

namespace Bot_Application3.test.Tests
{
    [TestClass()]
    public class ImportProduct
    {
        [TestMethod()]
        public void ttt()
        {
            ReadProduct rf = new ReadProduct();
            HashSet<string> set = new HashSet<string>();
            foreach (string s in ReadProduct.list)
            {
                //Debug.WriteLine(s);
                set.Add(s.Split(',')[1]);
            }

            BaseInfo bi = new BaseInfo();
            bi.init();

            SearchService ss = new SearchService();

            SynonymDao synonymDao = new SynonymDao();
            WordDao wordDao = new WordDao();
            WordSynonymDao wsDao = new WordSynonymDao();


            int i = 50000;

            foreach (string s in set)
            {
                //Debug.WriteLine(s);

                t_word word = ss.getWordBySynonym(s);
                if (null == word)
                {
                    Debug.WriteLine(s + "没有对应Word");
                    t_synonym t_synonym = new t_synonym() { code = i + "", name = s, remark = "" };
                    t_word t_word = new t_word() { code = i + "", type = "单品", name = s, remark = "" };
                    t_word_synonym t_word_synonym = new t_word_synonym() { word_code = i + "", synonym_code = i + "" };

                    synonymDao.add(t_synonym);
                    wordDao.add(t_word);
                    wsDao.add(t_word_synonym);
                    Debug.WriteLine("添加成功");
                    i++;
                }
                else
                {
                    Debug.WriteLine(s + "对应" + word.name + word.code);
                }
            }



        }

        [TestMethod()]
        public void test1Test()
        {
            try
            {
                BaseInfo bi = new BaseInfo();
                bi.init();
                SearchService ss = new SearchService();

                BrandProductDao bpDao = new BrandProductDao();

                ReadProduct rf = new ReadProduct();
                if (null != ReadProduct.list)
                {
                    int i = 0;
                    foreach (string s in ReadProduct.list)
                    {
                        Debug.WriteLine(s);

                        string brand = s.Split(',')[0];
                        string product = s.Split(',')[1];

                        string brand_code = string.Empty;
                        string product_word_code = string.Empty;

                        brand_code = String.IsNullOrWhiteSpace(brand) ? "" : (null == ss.getWordBySynonym(brand)) ? "" : ss.getWordBySynonym(brand).code;
                        if (String.IsNullOrWhiteSpace(brand_code))
                        {
                            Debug.WriteLine("没查出brand_code" + brand);
                            i++;
                            Debug.WriteLine(i);
                            continue;
                        }

                        product_word_code = String.IsNullOrWhiteSpace(product) ? "" : ss.getWordBySynonym(product).code;
                        if (String.IsNullOrWhiteSpace(product_word_code))
                            Debug.WriteLine("brand_code = " + brand + " ; product_word_code = " + product + " 无值");
                        else
                        {
                            //判断是否重复
                            List<t_brand_product> list = bpDao.getproductByBrandAndProduct(brand_code, product_word_code);
                            if (list.Count > 0)
                                Debug.WriteLine("brand_code = " + brand + " ; product_word_code = " + product + " 重复");
                            else
                            {
                                t_brand_product bp = new t_brand_product() { brand_code = brand_code, product_word_code = product_word_code };
                                bpDao.add(bp);
                                Debug.WriteLine("brand_code = " + brand + " ; product_word_code = " + product + " 可以写入");
                            }
                        }

                        i++;
                        Debug.WriteLine(i);

                    }

                }
                else
                {
                    Debug.WriteLine("list = null" + " ; path = " + ReadProduct.path);
                }
            }
            catch (DbEntityValidationException ex)
            {
                Debug.WriteLine(ex.ToString());

                StringBuilder errors = new StringBuilder();
                IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in validationResult)
                {
                    ICollection<DbValidationError> validationError = result.ValidationErrors;
                    foreach (DbValidationError err in validationError)
                    {
                        errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                    }
                }
                Debug.WriteLine(errors.ToString());
                //简写
                //var validerr = ex.EntityValidationErrors.First().ValidationErrors.First();
                //Console.WriteLine(validerr.PropertyName + ":" + validerr.ErrorMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }


        }

    }
}