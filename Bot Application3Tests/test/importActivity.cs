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
    public class ImportActivity
    {
        [TestMethod()]
        public void ttt()
        {
            ReadActivity rf = new ReadActivity();
            foreach (Activity obj in ReadActivity.dic.Values)
            {
                Debug.WriteLine(obj);
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

                SynonymDao synonymDao = new SynonymDao();
                WordDao wordDao = new WordDao();
                WordSynonymDao wsDao = new WordSynonymDao();
                ActivityDao activityDao = new ActivityDao();

                ReadActivity rf = new ReadActivity();
                if (null != ReadActivity.dic)
                {
                    int i = 30000;
                    foreach (Activity obj in ReadActivity.dic.Values)
                    {
                        Debug.WriteLine(obj.ToString());
                        t_synonym t_synonym = new t_synonym() { code = i + "", name = obj.Word, remark = "" };
                        t_word t_word = new t_word() { code = i + "", type = "活动", name = obj.Word, remark = "" };
                        t_word_synonym t_word_synonym = new t_word_synonym() { word_code = i + "", synonym_code = i + "" };
                        //t_brand t_brand = new t_brand { code = i + "", brand = i + "", floor = ss.getWordBySynonym(b.Floor).code, contract = b.BrandCode, house_number = b.UnitNumber, vip_flag = "特约商户".Equals(b.VipFee) ? 1 : 0 };

                        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

                        dtFormat.ShortDatePattern = "MM/dd/yyyy";

                        t_activity t_activity = new t_activity { code = i + "", activity = i + "" , floor = String.IsNullOrEmpty(obj.Floor)? "" : ss.getWordBySynonym(obj.Floor).code ,
                            basic_info = String.IsNullOrEmpty(obj.BasicInfo) ? "" : ss.getWordBySynonym(obj.BasicInfo).code, remark = obj.Remark ,
                            begin = Convert.ToDateTime(obj.Begin, dtFormat), end = Convert.ToDateTime(obj.End, dtFormat)};

                        synonymDao.add(t_synonym);
                        wordDao.add(t_word);
                        wsDao.add(t_word_synonym);
                        activityDao.add(t_activity);
                        Debug.WriteLine("添加成功");
                        i++;
                    }
                }
                else
                {
                    Debug.WriteLine("dic = null" + " ; path = " + ReadActivity.path);
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
                Debug.WriteLine(ex.Message);
            }


        }

    }
}