using Bot_Application3.Model;
using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class BaseInfo
    {
        public static Dictionary<string, t_word> wordDic = new Dictionary<string, t_word>();

        public static Dictionary<string, t_synonym> synonymDic = new Dictionary<string, t_synonym>();

        public static Dictionary<string, t_brand> brandDic = new Dictionary<string, t_brand>();
        
        public static Dictionary<string, t_brand> vipBrandDic = new Dictionary<string, t_brand>();

        public static Dictionary<string, t_basic_info> basicInfoDic = new Dictionary<string, t_basic_info>();

        public static Dictionary<string, t_activity> activityDic = new Dictionary<string, t_activity>();

        public BaseInfo ()
        {

        }

        public void init()
        {
            setWordDic();
            setSynonymDic();
            setBrandDic();
            setVipBrandDic();
            setBasicInfoDic();
            setActivityDic();
        }

        private void setWordDic()
        {
            WordDao dao = new WordDao();
            List<t_word> list = dao.getAllData();
            foreach (t_word po in list)
            {
                if (String.IsNullOrWhiteSpace(po.code))
                    continue;
                if (wordDic.ContainsKey(po.code))
                {
                    wordDic[po.code] = po;
                }
                else
                {
                    wordDic.Add(po.code, po);
                }
            }
        }

        private void setSynonymDic()
        {
            //因为是顾客说的，所以要用name做KEY
            SynonymDao dao = new SynonymDao();
            List<t_synonym> list = dao.getAllData();
            foreach (t_synonym po in list)
            {
                if (String.IsNullOrWhiteSpace(po.name))
                    continue;
                if (synonymDic.ContainsKey(po.name))
                {
                    synonymDic[po.name] = po;
                }
                else
                {
                    synonymDic.Add(po.name, po);
                }
            }
        }

        private void setBrandDic()
        {
            //用brand查询
            BrandDao dao = new BrandDao();
            List<t_brand> list = dao.getAllData(false);
            foreach (t_brand po in list)
            {
                if (String.IsNullOrWhiteSpace(po.brand))
                    continue;
                if (brandDic.ContainsKey(po.brand))
                {
                    brandDic[po.brand] = po;
                }
                else
                {
                    brandDic.Add(po.brand, po);
                }
            }
        }

        private void setVipBrandDic()
        {
            //用brand查询
            BrandDao dao = new BrandDao();
            List<t_brand> list = dao.getAllData(true);
            foreach (t_brand po in list)
            {
                if (String.IsNullOrWhiteSpace(po.brand))
                    continue;
                if (vipBrandDic.ContainsKey(po.brand))
                {
                    vipBrandDic[po.brand] = po;
                }
                else
                {
                    vipBrandDic.Add(po.brand, po);
                }
            }
        }

        private void setBasicInfoDic()
        {
            BasicInfoDao dao = new BasicInfoDao();
            List<t_basic_info> list = dao.getAllData();
            foreach (t_basic_info po in list)
            {
                if (String.IsNullOrWhiteSpace(po.basic_info_code))
                    continue;
                if (basicInfoDic.ContainsKey(po.basic_info_code))
                {
                    basicInfoDic[po.basic_info_code] = po;
                }
                else
                {
                    basicInfoDic.Add(po.basic_info_code, po);
                }
            }
        }

        private void setActivityDic()
        {
            ActivityDao dao = new ActivityDao();
            List<t_activity> list = dao.getAllData();
            foreach (t_activity po in list)
            {
                if (String.IsNullOrWhiteSpace(po.activity))
                    continue;
                if (activityDic.ContainsKey(po.activity))
                {
                    activityDic[po.activity] = po;
                }
                else
                {
                    activityDic.Add(po.activity, po);
                }
            }
        }
    }
}