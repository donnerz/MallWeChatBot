using Bot_Application3.Common;
using Bot_Application3.Model;
using Bot_Application3.Model.Vo;
using Common;
using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class SearchService
    {
        public static int TRUE_1 = 1;
        public static int FALSE_0 = 0;

        WordSynonymDao wsdao = new WordSynonymDao();

        BrandBusinessformatDao bbDao = new BrandBusinessformatDao();
        BrandProductDao bpDao = new BrandProductDao();

        BrandDao brandDao = new BrandDao();

        ActivityDao activityDao = new ActivityDao();

        public BrandVo getBrandVo()
        {
            BrandVo vo = new BrandVo();

            return vo;
        }

        //根据基础信息名称查询基础信息
        public t_basic_info getBasicInfoByName(String basicInfoName)
        {
            if (String.IsNullOrWhiteSpace(basicInfoName))
                return null;

            if (!BaseInfo.synonymDic.ContainsKey(basicInfoName))
                return null;

            t_synonym synonym = BaseInfo.synonymDic[basicInfoName];

            if (String.IsNullOrWhiteSpace(synonym.code))
                return null;

            t_word_synonym word_synonym = wsdao.getDataByCode(synonym.code);

            if (String.IsNullOrWhiteSpace(word_synonym.word_code))
                return null;

            if (!BaseInfo.basicInfoDic.ContainsKey(word_synonym.word_code))
                return null;

            return BaseInfo.basicInfoDic[word_synonym.word_code];
        }

        //根据同义词查词汇名称
        public String getWordNameBySynonym(String synonymName)
        {
            if (String.IsNullOrWhiteSpace(synonymName))
                return String.Empty;

            if (!BaseInfo.synonymDic.ContainsKey(synonymName))
                return synonymName;

            t_synonym synonym = BaseInfo.synonymDic[synonymName];

            if (String.IsNullOrWhiteSpace(synonym.code))
                return synonymName;

            t_word_synonym word_synonym = wsdao.getDataByCode(synonym.code);

            if (String.IsNullOrWhiteSpace(word_synonym.word_code))
                return synonymName;

            if (!BaseInfo.wordDic.ContainsKey(word_synonym.word_code))
                return synonymName;

            return BaseInfo.wordDic[word_synonym.word_code].name;
        }

        //根据同义词查词汇
        public t_word getWordBySynonym(String synonymName)
        {
            if (String.IsNullOrWhiteSpace(synonymName))
                return null;

            if (!BaseInfo.synonymDic.ContainsKey(synonymName))
                return null;

            t_synonym synonym = BaseInfo.synonymDic[synonymName];

            if (String.IsNullOrWhiteSpace(synonym.code))
                return null;

            t_word_synonym word_synonym = wsdao.getDataByCode(synonym.code);

            if (String.IsNullOrWhiteSpace(word_synonym.word_code))
                return null;

            if (!BaseInfo.wordDic.ContainsKey(word_synonym.word_code))
                return null;

            return BaseInfo.wordDic[word_synonym.word_code];
        }

        //根据品牌名称查品牌
        public t_brand getBrandByName(String brandName)
        {
            if (String.IsNullOrWhiteSpace(brandName))
                return null;

            if (!BaseInfo.synonymDic.ContainsKey(brandName))
                return null;

            t_synonym synonym = BaseInfo.synonymDic[brandName];

            if (String.IsNullOrWhiteSpace(synonym.code))
                return null;

            t_word_synonym word_synonym = wsdao.getDataByCode(synonym.code);

            if (String.IsNullOrWhiteSpace(word_synonym.word_code))
                return null;

            if (!BaseInfo.brandDic.ContainsKey(word_synonym.word_code))
                return null;

            return BaseInfo.brandDic[word_synonym.word_code];
        }

        //通过品牌和业态查询t_brand_businessformat
        public List<t_brand_businessformat> getBusinessformatByBrandAndBf(String brandCode, String businessformatCode)
        {
            if (String.IsNullOrWhiteSpace(brandCode))
                return new List<t_brand_businessformat>();

            List<t_brand_businessformat> list = bbDao.getBusinessformatByBrandAndBf(brandCode, businessformatCode);
            return list;
        }
        
        //查询全部品牌
        public List<t_brand> GetAllBrandList (bool VipFlag)
        {
            List<t_brand> brandList = new List<t_brand>();
            //全brand
            foreach (t_brand brand in BaseInfo.brandDic.Values)
            {
                if (VipFlag && !(TRUE_1 == brand.vip_flag))
                    continue;
                brandList.Add(brand);
                if (brandList.Count >= 5)
                    break;
                
            }
            return brandList;
        }

        //根据业态，楼层，特约商户，查询品牌列表
        public List<t_brand> GetBrandListByBusinessFormatAndFloor(String businessFormatText, String floorText, bool VipFlag)
        {
            List<t_brand> brandList = new List<t_brand>();
            //品牌列表
            List<String> code_list = new List<String>();
            //楼层
            String floor_code = String.Empty;

            //判断业态是否为空
            if (!String.IsNullOrWhiteSpace(businessFormatText))
            {
                t_word bf_word = getWordBySynonym(businessFormatText);
                //判断业态是否可以查询到
                if (null != bf_word)
                {
                    //查询到则获得bb列表
                    List<t_brand_businessformat> bb_list = bbDao.getBusinessformatByBrandAndBf(null, bf_word.code);
                    //t_brand_businessformat有把brand_code存起来
                    if (bb_list.Count > 0)
                    {
                        foreach (t_brand_businessformat brand_businessformat in bb_list)
                            code_list.Add(brand_businessformat.brand_code);
                    }
                    //t_brand_businessformat没有就返回空列表
                    else
                        return brandList;
                }
                //业态查询不到就返回空列表
                else
                    return brandList;
            }

            //判断楼层是否为空
            if (!String.IsNullOrWhiteSpace(floorText))
            {
                t_word f_word = getWordBySynonym(floorText);
                //判断楼层是否可以查询到
                if (null != f_word)
                    floor_code = f_word.code;
                //楼层查询不到就返回空列表
                else
                    return brandList;
            }

            brandList = brandDao.getBrandByCondition(code_list.ToArray(), floor_code, VipFlag);

            //t_brand tmp = new t_brand();

            //String a = "";
            //foreach (String s in code_list)
            //{
            //    a += s + "、";
            //}

            //tmp.code = " code_list = " + a + " floor = " + floor_code + " vipflag = " + VipFlag;

            //brandList.Add(tmp);

            //取前5条
            if (brandList.Count >=5)
            {
                brandList = brandList.Take(5).ToList();
            }
            return brandList;
        }

        //根据单品，楼层，特约商户，查询品牌列表
        public List<t_brand> GetBrandListByProductAndFloor(String productText, String floorText, bool VipFlag)
        {
            List<t_brand> brandList = new List<t_brand>();
            //品牌列表
            List<String> code_list = new List<String>();
            //楼层
            String floor_code = String.Empty;

            //判断单品是否为空
            if (!String.IsNullOrWhiteSpace(productText))
            {
                t_word p_word = getWordBySynonym(productText);
                //判断业态是否可以查询到
                if (null != p_word)
                {
                    //查询到则获得bp列表
                    List<t_brand_product> bp_list = bpDao.getproductByBrandAndProduct(null, p_word.code);
                    //t_brand_product有把brand_code存起来
                    if (bp_list.Count > 0)
                    {
                        foreach (t_brand_product brand_product in bp_list)
                            code_list.Add(brand_product.brand_code);
                    }
                    //t_brand_businessformat没有就返回空列表
                    else
                        return brandList;
                }
                //业态查询不到就返回空列表
                else
                    return brandList;
            }

            //判断楼层是否为空
            if (!String.IsNullOrWhiteSpace(floorText))
            {
                t_word f_word = getWordBySynonym(floorText);
                //判断楼层是否可以查询到
                if (null != f_word)
                    floor_code = f_word.code;
                //楼层查询不到就返回空列表
                else
                    return brandList;
            }

            brandList = brandDao.getBrandByCondition(code_list.ToArray(), floor_code, VipFlag);

            if (brandList.Count >= 5)
            {
                brandList = brandList.Take(5).ToList();
            }
            return brandList;
        }

        //根据活动名称查询活动
        public t_activity getActivityByName(String activityName)
        {
            if (String.IsNullOrWhiteSpace(activityName))
                return null;

            if (!BaseInfo.synonymDic.ContainsKey(activityName))
                return null;

            t_synonym synonym = BaseInfo.synonymDic[activityName];

            if (String.IsNullOrWhiteSpace(synonym.code))
                return null;

            t_word_synonym word_synonym = wsdao.getDataByCode(synonym.code);

            if (String.IsNullOrWhiteSpace(word_synonym.word_code))
                return null;

            if (!BaseInfo.activityDic.ContainsKey(word_synonym.word_code))
                return null;

            return BaseInfo.activityDic[word_synonym.word_code];
        }

        //根据基础信息、楼层，查询活动列表
        public List<t_activity> GetActivityListByBasciInfoAndFloor(String basicInfoText, String floorText, String builtinDatetimeDateResolution)
        {
            List<t_activity> activityList = new List<t_activity>();

            //楼层
            String basic_info_code = String.Empty;
            //楼层
            String floor_code = String.Empty;

            //判断基础信息是否为空
            if (!String.IsNullOrWhiteSpace(basicInfoText))
            {
                t_word bi_word = getWordBySynonym(basicInfoText);
                //判断基础信息是否可以查询到
                if (null != bi_word)
                    basic_info_code = bi_word.code;
                //基础信息查询不到就返回空列表
                else
                    return activityList;
            }

            //判断楼层是否为空
            if (!String.IsNullOrWhiteSpace(floorText))
            {
                t_word f_word = getWordBySynonym(floorText);
                //判断楼层是否可以查询到
                if (null != f_word)
                    floor_code = f_word.code;
                //楼层查询不到就返回空列表
                else
                    return activityList;
            }

            activityList = activityDao.getDataByCondition(basic_info_code, floor_code, MSDateUtil.getBegin(builtinDatetimeDateResolution), MSDateUtil.getEnd(builtinDatetimeDateResolution));

            if (activityList.Count >= 5)
            {
                activityList = activityList.Take(5).ToList();
            }


            return activityList;
        }
    }
}