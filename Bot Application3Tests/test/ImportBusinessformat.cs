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
    public class ImportBusinessformat
    {
        [TestMethod()]
        public void ttt()
        {
            ReadBusinessformat rf = new ReadBusinessformat();
            foreach (Business obj in ReadBusinessformat.dic.Values)
            {
                Debug.WriteLine(obj);
            }

        }

        //[TestMethod()]
        //public void importBusiness()
        //{
        //    ReadBusinessformat rf = new ReadBusinessformat();

        //    HashSet<string> set = new HashSet<string>();
        //    foreach (Business obj in ReadBusinessformat.dic.Values)
        //    {
        //        //Debug.WriteLine(obj);
        //        if (!String.IsNullOrWhiteSpace(obj.Business1))
        //            set.Add(obj.Business1);
        //        if (!String.IsNullOrWhiteSpace(obj.Business2))
        //            set.Add(obj.Business2);
        //        if (!String.IsNullOrWhiteSpace(obj.Business3))
        //            set.Add(obj.Business3);

        //    }

        //    BaseInfo bi = new BaseInfo();
        //    bi.init();

        //    SearchService ss = new SearchService();

        //    SynonymDao synonymDao = new SynonymDao();
        //    WordDao wordDao = new WordDao();
        //    WordSynonymDao wsDao = new WordSynonymDao();


        //    int i = 40000;
        //    foreach (string s in set)
        //    {
        //        //Debug.WriteLine(s);

        //        t_word word = ss.getWordBySynonym(s);
        //        if (null == word)
        //        {
        //            Debug.WriteLine(s + "没有对应Word");
        //            t_synonym t_synonym = new t_synonym() { code = i + "", name = s, remark = "" };
        //            t_word t_word = new t_word() { code = i + "", type = "业态", name = s, remark = "" };
        //            t_word_synonym t_word_synonym = new t_word_synonym() { word_code = i + "", synonym_code = i + "" };

        //            synonymDao.add(t_synonym);
        //            wordDao.add(t_word);
        //            wsDao.add(t_word_synonym);
        //            Debug.WriteLine("添加成功");
        //            i++;
        //        }
        //        else
        //        {
        //            Debug.WriteLine(s + "对应" + word.name + word.code);
        //        }


        //    }
               

        //}


        //[TestMethod()]
        //public void test1Test()
        //{
        //    try
        //    {
        //        BaseInfo bi = new BaseInfo();
        //        bi.init();
        //        SearchService ss = new SearchService();

        //        BrandBusinessformatDao bbDao = new BrandBusinessformatDao();

        //        ReadBusinessformat rf = new ReadBusinessformat();
        //        if (null != ReadBusinessformat.dic)
        //        {
        //            int i = 0;
        //            foreach (Business obj in ReadBusinessformat.dic.Values)
        //            {
        //                Debug.WriteLine(obj.ToString());

        //                string brand_code = string.Empty;
        //                string businessformat_word_code = string.Empty;

        //                brand_code = String.IsNullOrWhiteSpace(obj.Brand) ? "" : (null == ss.getWordBySynonym(obj.Brand)) ? "" : ss.getWordBySynonym(obj.Brand).code;
        //                if (String.IsNullOrWhiteSpace(brand_code))
        //                {
        //                    Debug.WriteLine("没查出brand_code" + obj.Brand);
        //                    i++;
        //                    Debug.WriteLine(i);
        //                    continue;
        //                }

        //                businessformat_word_code = String.IsNullOrWhiteSpace(obj.Business1) ? "" : ss.getWordBySynonym(obj.Business1).code;
        //                if (String.IsNullOrWhiteSpace(businessformat_word_code))
        //                    Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business1 + " 无值");
        //                else
        //                {
        //                    //判断是否重复
        //                    List<t_brand_businessformat> list = bbDao.getBusinessformatByBrandAndBf(brand_code, businessformat_word_code);
        //                    if (list.Count > 0)
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business1 + " 重复");
        //                    else
        //                    {
        //                        t_brand_businessformat bb = new t_brand_businessformat() { brand_code = brand_code, businessformat_word_code = businessformat_word_code };
        //                        bbDao.add(bb);
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business1 + " 可以写入"); 
        //                    }
        //                }

        //                businessformat_word_code = String.IsNullOrWhiteSpace(obj.Business2) ? "" : ss.getWordBySynonym(obj.Business2).code;
        //                if (String.IsNullOrWhiteSpace(businessformat_word_code))
        //                    Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business2 + " 无值");
        //                else
        //                {
        //                    //判断是否重复
        //                    List<t_brand_businessformat> list = bbDao.getBusinessformatByBrandAndBf(brand_code, businessformat_word_code);
        //                    if (list.Count > 0)
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business2 + " 重复");
        //                    else
        //                    {
        //                        t_brand_businessformat bb = new t_brand_businessformat() { brand_code = brand_code, businessformat_word_code = businessformat_word_code };
        //                        bbDao.add(bb);
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business2 + " 可以写入");
        //                    }
        //                }

        //                businessformat_word_code = String.IsNullOrWhiteSpace(obj.Business3) ? "" : ss.getWordBySynonym(obj.Business3).code;
        //                if (String.IsNullOrWhiteSpace(businessformat_word_code))
        //                    Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business3 + " 无值"); 
        //                else
        //                {
        //                    //判断是否重复
        //                    List<t_brand_businessformat> list = bbDao.getBusinessformatByBrandAndBf(brand_code, businessformat_word_code);
        //                    if (list.Count > 0)
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business3 + " 重复");
        //                    else
        //                    {
        //                        t_brand_businessformat bb = new t_brand_businessformat() { brand_code = brand_code, businessformat_word_code = businessformat_word_code };
        //                        bbDao.add(bb);
        //                        Debug.WriteLine("brand_code = " + obj.Brand + " ; businessformat_word_code = " + obj.Business3 + " 可以写入");
        //                    }
        //                }

        //                //Debug.WriteLine("添加成功");
        //                i++;
        //                Debug.WriteLine(i);
        //            }
                   
        //        }
        //        else
        //        {
        //            Debug.WriteLine("dic = null" + " ; path = " + ReadBusinessformat.path);
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        Debug.WriteLine(ex.ToString());

        //        StringBuilder errors = new StringBuilder();
        //        IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
        //        foreach (DbEntityValidationResult result in validationResult)
        //        {
        //            ICollection<DbValidationError> validationError = result.ValidationErrors;
        //            foreach (DbValidationError err in validationError)
        //            {
        //                errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
        //            }
        //        }
        //        Debug.WriteLine(errors.ToString());
        //        //简写
        //        //var validerr = ex.EntityValidationErrors.First().ValidationErrors.First();
        //        //Console.WriteLine(validerr.PropertyName + ":" + validerr.ErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //    }


        //}

    }
}