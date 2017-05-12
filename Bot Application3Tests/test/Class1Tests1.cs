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

namespace Bot_Application3.test.Tests
{
    [TestClass()]
    public class Class1Tests1
    {
        [TestMethod()]
        public void test1Test()
        {
            try
            {
                BaseInfo bi = new BaseInfo();
                bi.init();
                SearchService ss = new SearchService();

                SynonymDao synonymDao = new SynonymDao();
                WordDao wordDao = new WordDao();
                WordSynonymDao wsDao = new WordSynonymDao();
                BrandDao brandDao = new BrandDao();

                ReadFile rf = new ReadFile();
                if (null != ReadFile.brandDic)
                {
                    int i = 20000;
                    foreach (Brand b in ReadFile.brandDic.Values)
                    {
                        Debug.WriteLine(b.ToString());
                        t_synonym t_synonym = new t_synonym() { code = i + "", name = b.BrandName, remark = "" };
                        t_word t_word = new t_word() { code = i + "", type = "品牌", name = b.BrandName, remark = "" };
                        t_word_synonym t_word_synonym = new t_word_synonym() { word_code = i + "", synonym_code = i + "" };
                        t_brand t_brand = new t_brand { code = i + "", brand = i + "", floor = ss.getWordBySynonym(b.Floor).code, contract = b.BrandCode, house_number = b.UnitNumber, vip_flag = "特约商户".Equals(b.VipFee) ? 1 : 0 };

                        synonymDao.add(t_synonym);
                        wordDao.add(t_word);
                        wsDao.add(t_word_synonym);
                        brandDao.add(t_brand);
                        Debug.WriteLine("添加成功");
                        i++;
                    }
                }
                else
                {
                    Debug.WriteLine("brandDic = null" + " ; path = " + ReadFile.path);
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



        }

    }
}