using Bot_Application3.Model;
using Common;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Model.Domian;
using System;
using System.Collections.Generic;

using System.Text;


namespace Service
{
    [Serializable]
    public class CRMService
    {

        public static Dictionary<string, CRMInfo> customerDic = new Dictionary<string, CRMInfo>();

        public static int TRUE_1 = 1;
        public static int FALSE_0 = 0;

        SearchService searchService = new SearchService();
        ActivityService activityService = new ActivityService();

        //清空数据
        public void ClearData(CRMInfo info, bool intentFlag)
        {
            //存在顾客会话
            if (customerDic.ContainsKey(info.customer))
            {
                CRMInfo tmp = new CRMInfo();
                //意图标志 true 删除意图
                if (!intentFlag)
                {
                    tmp.intent = info.intent;
                }
                tmp.customer = info.customer;
                customerDic[info.customer] = tmp;    
            }          
        }

        //获取用户会话
        public CRMInfo GetCustomerSession(String customer)
        {
            CRMInfo tmp = null;
            if (customerDic.ContainsKey(customer))
            {
                tmp = customerDic[customer];
            }
            else
            {
                tmp = new CRMInfo();
                tmp.customer = customer;
                customerDic.Add(customer, tmp);
            }
            return tmp;
        }

        //设置基础信息信息
        public void SetCommonInfo(CRMInfo info, IDialogContext context, LuisResult result)
        {
            //设置品牌
            bool brandFlag = TryToFindBrand(info, result);
            //设置业态
            bool businessFormatFlag = TryToFindBusinessFormat(info, result);
            //设置楼层
            bool floorFlag = TryToFindFloor(info, result);
            //设置单品
            bool productFlag = TryToFindProduct(info, result);
            //设置基础信息
            bool BasicInfoFlag = TryToFindBasicInfo(info, result);
            //设置互动
            bool ActivityFlag = TryToFindActivity(info, result);

            //设置builtin.datetime.date
            bool builtinDateTimeDateFlag = TryToFindBuiltinDatetimeDate(info, result);
            //设置builtin.datetime.time
            bool builtinDateTimeTimeFlag = TryToFindBuiltinDatetimeTime(info, result);
            //设置导引
            TryToGuide(info, result);
        }

        //品牌
        public bool TryToFindBrand(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("品牌", out entity))
            {
                info.brandText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.brandText);
        }

        //业态
        public bool TryToFindBusinessFormat(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("业态", out entity))
            {
                info.businessFormatText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.businessFormatText);
        }

        //楼层
        public bool TryToFindFloor(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("楼层", out entity))
            {
                info.floorText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.floorText);
        }

        //单品
        public bool TryToFindProduct(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("单品", out entity))
            {
                info.productText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.productText);
        }

        //基础信息
        public bool TryToFindBasicInfo(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("基础信息", out entity))
            {
                info.basicInfoText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.basicInfoText);
        }

        //活动
        public bool TryToFindActivity(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("活动", out entity))
            {
                info.activityText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.activityText);
        }

        //builtin.datetime.date
        public bool TryToFindBuiltinDatetimeDate(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("builtin.datetime.date", out entity))
            {
                info.BuiltinDatetimeDate = entity.Entity.Replace(" ", "");
                info.BuiltinDatetimeDateResolution = entity.Resolution["date"];
            }
            return !String.IsNullOrWhiteSpace(info.activityText);
        }

        //builtin.datetime.time
        public bool TryToFindBuiltinDatetimeTime(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("builtin.datetime.time", out entity))
            {
                info.builtinDatetimeTime = entity.Entity.Replace(" ", "");
                info.builtinDatetimeTimeResolution = entity.Resolution["time"];
            }
            return !String.IsNullOrWhiteSpace(info.activityText);
        }

        //判断品牌1
        public bool JudgeBrand1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.brandText))
            {
                JudgeBrand2(info);
            }
            else if (!String.IsNullOrWhiteSpace(info.basicInfoText))
            {
                JudgeBasicInfo2(info);
            }
            else
            {
                info.replyString = "正大君还不知道您想去哪哦。您可以回答：星巴克、同道大叔等等";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断品牌2
        public bool JudgeBrand2(CRMInfo info)
        {
            //查询品牌
            t_brand brand = searchService.getBrandByName(info.brandText);
            if (null != brand)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(BaseInfo.wordDic[brand.brand].name);
                sb.Append("在");
                sb.Append(BaseInfo.wordDic[brand.floor].name);
                sb.Append(brand.house_number).Append("(门牌号)。");
                if (TRUE_1 == brand.vip_flag)
                    sb.Append("是特约商户，可以积分哦。");
                
                //加入业态判断
                List<t_brand_businessformat> list = searchService.getBusinessformatByBrandAndBf(brand.code, null);

                foreach (t_brand_businessformat bb in list)
                {
                    if ("15".Equals(bb.businessformat_word_code) || "22".Equals(bb.businessformat_word_code) || "19".Equals(bb.businessformat_word_code) || "33".Equals(bb.businessformat_word_code))
                    {
                        sb.Append("（可以接入点餐，订座，排队等第三方服务）！");
                        break;
                    }
                    else if ("27".Equals(bb.businessformat_word_code) || "34".Equals(bb.businessformat_word_code))
                    {
                        sb.Append("（可以接入虚拟试衣，领取优惠券等第三方服务）！");
                        break;
                    }
                }

                info.replyString = sb.ToString();
            }
            else
            {
                info.replyString = "对不起，您说的品牌" + info.brandText + "正大君没查询到哦。";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断有什么1
        public bool JudgeHaveSomething1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.brandText))
            {
                JudgeBrand2(info);          
            }
            else if (!String.IsNullOrWhiteSpace(info.productText))
            {
                JudgeProduct1(info);
            }
            else
            {
                JudgeBusinessFormat1(info);
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断业态1
        public bool JudgeBusinessFormat1(CRMInfo info)
        {
            //从数据库查询
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder brandsb = new StringBuilder();
                String brandStr = "";
                if (!String.IsNullOrWhiteSpace(info.businessFormatText) || !String.IsNullOrWhiteSpace(info.floorText))
                {
                    //该业态/楼层下的品牌
                    List<t_brand> list = searchService.GetBrandListByBusinessFormatAndFloor(info.businessFormatText, info.floorText, info.VipFlag);

                    //t_brand tmp = list[list.Count - 1];
                    //sb.Append("info.businessFormatText = " + info.businessFormatText + " info.floorText = " + info.floorText + " test = " + tmp.code + " count = " + (list.Count  -1));
                    //list.RemoveAt(list.Count - 1);

                    if (list.Count > 0)
                    {
                        foreach (t_brand brand in list)
                        {
                            brandsb.Append(BaseInfo.wordDic[brand.brand].name).Append("、");
                        }
                        if (brandsb.Length > 0)
                        {
                            brandStr = brandsb.ToString().Substring(0, brandsb.ToString().Length - 1);
                        }
                        if (!String.IsNullOrWhiteSpace(info.businessFormatText))
                            sb.Append(searchService.getWordNameBySynonym(info.businessFormatText));
                        if (info.VipFlag)
                            sb.Append("特约商户");
                        else
                            sb.Append("商户");
                        sb.Append("正大君");
                        if (!String.IsNullOrWhiteSpace(info.floorText))
                            sb.Append("在").Append(searchService.getWordNameBySynonym(info.floorText));
                        sb.Append("找到了");
                        sb.Append(brandStr);
                        sb.Append("。您想去哪家呢?");
                        info.replyString = sb.ToString();
                    }
                    else
                    {
                        list = searchService.GetBrandListByBusinessFormatAndFloor(info.businessFormatText, null, info.VipFlag);
                        if (list.Count > 0)
                        {
                            foreach (t_brand brand in list)
                            {
                                brandsb.Append(BaseInfo.wordDic[brand.brand].name).Append("、");
                            }
                            if (brandsb.Length > 0)
                            {
                                brandStr = brandsb.ToString().Substring(0, brandsb.ToString().Length - 1);
                            }
                            sb.Append("正大君");
                            if (!String.IsNullOrWhiteSpace(info.floorText))
                                sb.Append("在").Append(searchService.getWordNameBySynonym(info.floorText));
                            sb.Append("没有找到");
                            sb.Append(searchService.getWordNameBySynonym(info.businessFormatText));
                            if (info.VipFlag)
                                sb.Append("的特约商户");
                            else
                                sb.Append("的商户");
                            sb.Append("哦。");
                            sb.Append(searchService.getWordNameBySynonym(info.businessFormatText));
                            sb.Append("您可以尝试");
                            sb.Append(brandStr);
                            info.replyString = sb.ToString();
                        }
                        else
                        {
                            sb.Append("哎呀，正大君没有找到");
                            sb.Append(searchService.getWordNameBySynonym(info.businessFormatText));
                            if (info.VipFlag)
                                sb.Append("的特约商户");
                            else
                                sb.Append("的商户");
                            sb.Append("哦。您换一个别的业态问问呗。");
                            info.replyString = sb.ToString();
                        }
                    }
                }
                else
                {
                    List<t_brand> list = searchService.GetAllBrandList(info.VipFlag);
                    foreach (t_brand brand in list)
                    {
                        brandsb.Append(BaseInfo.wordDic[brand.brand].name).Append("、");
                    }
                    if (brandsb.Length > 0)
                    {
                        brandStr = brandsb.ToString().Substring(0, brandsb.ToString().Length - 1);
                    }
                    sb.Append("正大广场");
                    if (info.VipFlag)
                        sb.Append("的特约商户");
                    else
                        sb.Append("的商户");
                    sb.Append("有好多呢。比如");
                    sb.Append(brandStr);
                    sb.Append("。问“");
                    if (info.VipFlag)
                        sb.Append("7楼女装特约商铺");
                    else
                        sb.Append("7楼有什么女装");
                    sb.Append("”可以获取更详细的信息哦!");

                    info.replyString = sb.ToString();
                }
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
            catch (Exception e)
            {
                info.replyString += "我在JudgeBusinessFormat1报错啦" + e.StackTrace;
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
        }

        //判断吃饭1
        public bool JudgeEat1(CRMInfo info)
        {
            try
            {
                //判断开始
                if (!String.IsNullOrWhiteSpace(info.brandText))
                {
                    JudgeBrand2(info);
                }
                else if (!String.IsNullOrWhiteSpace(info.productText))
                {
                    JudgeProduct1(info);
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(info.businessFormatText))
                    {
                        info.businessFormatText = "餐饮";
                    }
                    JudgeBusinessFormat1(info);
                }
            }
            catch (Exception e)
            {
                info.replyString = "JudgeEat1 error = " + e.ToString();
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断购物1
        public bool JudgeBuy1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.brandText))
            {
                JudgeBrand2(info);
            }
            else if (!String.IsNullOrWhiteSpace(info.productText))
            {
                JudgeProduct1(info);
            }
            else
            {
                if (String.IsNullOrWhiteSpace(info.businessFormatText))
                {
                    //TODO 根据顾客的基本资料来设定
                    info.businessFormatText = "女装";
                }
                JudgeBusinessFormat1(info);
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断特约商铺1
        public bool JudgeMemberStore1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.brandText))
            {
                JudgeBrand3(info);
            }
            else
            {
                JudgeBusinessFormat1(info);
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断品牌3
        private bool JudgeBrand3(CRMInfo info)
        {
            t_brand brand = searchService.getBrandByName(info.brandText);
            if (null != brand)
            {
                StringBuilder sb = new StringBuilder();
                if (TRUE_1 == brand.vip_flag)
                {
                    sb.Append(BaseInfo.wordDic[brand.brand].name);
                    sb.Append("是特约商户，可以积分哦。");
                }
                else
                {
                    sb.Append(BaseInfo.wordDic[brand.brand].name);
                    sb.Append("目前还不是是特约商户，不可以积分！");
                }
                info.replyString = sb.ToString();
            }
            else
            {
                info.replyString = "对不起，您说的品牌" + info.brandText + "正大君没查询到哦。";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断单品1
        private bool JudgeProduct1(CRMInfo info)
        {
            //从数据库查询
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder brandsb = new StringBuilder();
                String brandStr = "";
                //该单品（楼层）下的品牌
                List<t_brand> list = searchService.GetBrandListByProductAndFloor(info.productText, info.floorText, info.VipFlag);

                if (list.Count > 0)
                {
                    foreach (t_brand brand in list)
                    {
                        brandsb.Append(BaseInfo.wordDic[brand.brand].name).Append("、");
                    }
                    if (brandsb.Length > 0)
                    {
                        brandStr = brandsb.ToString().Substring(0, brandsb.ToString().Length - 1);
                    }
                    sb.Append("卖");
                    sb.Append(searchService.getWordNameBySynonym(info.productText));
                    if (info.VipFlag)
                        sb.Append("的特约商户");
                    else
                        sb.Append("的商户");
                    sb.Append("正大君");
                    if (!String.IsNullOrWhiteSpace(info.floorText))
                        sb.Append("在").Append(searchService.getWordNameBySynonym(info.floorText));
                    sb.Append("找到了");
                    sb.Append(brandStr);
                    sb.Append("。您想去哪家呢?");
                    info.replyString = sb.ToString();
                }
                else
                {
                    list = searchService.GetBrandListByProductAndFloor(info.productText, null, info.VipFlag);
                    if (list.Count > 0)
                    {
                        foreach (t_brand brand in list)
                        {
                            brandsb.Append(BaseInfo.wordDic[brand.brand].name).Append("、");
                        }
                        if (brandsb.Length > 0)
                        {
                            brandStr = brandsb.ToString().Substring(0, brandsb.ToString().Length - 1);
                        }
                        sb.Append("正大君");
                        if (!String.IsNullOrWhiteSpace(info.floorText))
                            sb.Append("在").Append(searchService.getWordNameBySynonym(info.floorText));
                        sb.Append("没有找到卖");
                        sb.Append(searchService.getWordNameBySynonym(info.productText));
                        if (info.VipFlag)
                            sb.Append("的特约商户");
                        else
                            sb.Append("的商户");
                        sb.Append("哦。");
                        sb.Append(searchService.getWordNameBySynonym(info.productText));
                        sb.Append("您可以尝试");
                        sb.Append(brandStr);
                        info.replyString = sb.ToString();
                    }
                    else
                    {
                        sb.Append("哎呀，正大君没有找到卖");
                        sb.Append(searchService.getWordNameBySynonym(info.productText));
                        if (info.VipFlag)
                            sb.Append("的特约商户");
                        else
                            sb.Append("的商户");
                        sb.Append("哦。您换一个别的单品问问呗。");
                        info.replyString = sb.ToString();
                    }
                }

                return !String.IsNullOrWhiteSpace(info.replyString);
            }
            catch (Exception e)
            {
                info.replyString += "我在JudgeProduct1报错啦" + e.StackTrace;
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
        }

        //判断基础信息1
        public bool JudgeBasicInfo1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.basicInfoText))
            {
                JudgeBasicInfo2(info);
            }
            else
            {
                info.replyString = "正大君还不知道您想查询什么基础信息哦。您可以回答：停车场、停车费、营业时间等等";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断基础信息2
        public bool JudgeBasicInfo2(CRMInfo info)
        {
            //查询基础信息
            t_basic_info basic_info = searchService.getBasicInfoByName(info.basicInfoText);
            if (null != basic_info)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(basic_info.remark);
 
                info.replyString = sb.ToString();
            }
            else
            {
                info.replyString = "对不起，您说的基础信息" + info.basicInfoText + "正大君没查询到哦。";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断活动1
        public bool JudgeActivity1(CRMInfo info)
        {
            //判断开始
            if (!String.IsNullOrWhiteSpace(info.activityText))
            {
                JudgeActivity2(info);
            }
            else
            {
                JudgeActivity3(info);
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断活动2
        public bool JudgeActivity2(CRMInfo info)
        {
            //查询活动
            try
            {
                t_activity activity = searchService.getActivityByName(info.activityText);
                if (null != activity)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(BaseInfo.wordDic[activity.activity].name);
                    sb.Append("于");
                    sb.Append(activity.begin.Value.ToString("yyyy-MM-dd"));
                    sb.Append("到");
                    sb.Append(activity.end.Value.ToString("yyyy-MM-dd"));
                    sb.Append("在");
                    if (!String.IsNullOrWhiteSpace(activity.floor) || !String.IsNullOrWhiteSpace(activity.basic_info))
                    {
                        if (!String.IsNullOrWhiteSpace(activity.floor))
                            sb.Append(BaseInfo.wordDic[activity.floor].name);
                        if (!String.IsNullOrWhiteSpace(activity.basic_info))
                            sb.Append(BaseInfo.wordDic[activity.basic_info].name);
                    }
                    else
                        sb.Append("全馆");
                    sb.Append("开展。");
                    sb.Append("“");
                    sb.Append(activity.remark);
                    sb.Append("”宣传语。");

                    if (DateTime.Compare(DateTime.Now, activity.begin.Value) < 0)
                        sb.Append("目前该活动尚未开始！");
                    else if (DateTime.Compare(DateTime.Now, activity.end.Value) > 0)
                        sb.Append("目前该活动已经结束咯！");
                    else
                        sb.Append("目前该活动正在火热进行中！");
                           
                    info.replyString = sb.ToString();
                }
                else
                {
                    info.replyString = "对不起，您说的活动" + info.activityText + "正大君没查询到哦。";
                }
            }
            catch (Exception e)
            {
                info.replyString += "我在JudgeActivity2报错啦" + e.ToString();
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //判断单品1
        private bool JudgeActivity3(CRMInfo info)
        {
            //从数据库查询
            try
            {
                activityService.JudgeActivity4(info, null);
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
            catch (Exception e)
            {
                info.replyString += "我在JudgeActivity3报错啦" + e.ToString();
                return !String.IsNullOrWhiteSpace(info.replyString);
            }
        }

        //导引
        public bool TryToGuide(CRMInfo info, LuisResult result)
        {
            EntityRecommendation entity;
            if (result.TryFindEntity("导引", out entity))
            {
                info.businessFormatText = entity.Entity.Replace(" ", "");
            }
            return !String.IsNullOrWhiteSpace(info.floorText);
        }
        //导引结果返回
        public bool TryGuide(CRMInfo info)
        {
            var businessFormatText = info.businessFormatText;
            if (businessFormatText != null)
            {
                if (businessFormatText.Contains("东方明珠"))
                {
                    info.replyString = "您要去东方明珠，请走东门上天桥左转。";
                }
                else if (businessFormatText.Contains("国金"))
                {
                    info.replyString = "您要去国金，请走东门上天桥右转。";
                }
                else if (businessFormatText.Contains("2号线"))
                {
                    info.replyString = "您要去2号线，请走东门上天桥右转，到平安银行大厦坐电梯从1号口进入。";
                }
                else if (businessFormatText.Contains("滨江大道"))
                {
                    info.replyString = "您要去滨江大道，请走西门二楼平台过道。";
                }
                else if (businessFormatText.Contains("香格里拉"))
                {
                    info.replyString = "您要去香格里拉，请走1楼侧门。";
                }
                else
                {
                    info.replyString = "请告知您想去哪里？";
                }
            }
            else
            {
                info.replyString = "请告知您想去哪里？";
            }
            return !String.IsNullOrWhiteSpace(info.replyString);
        }

        //积分
        public bool QueryPoint(CRMInfo info)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("i am in queryPoint");

            info.replyString = sb.ToString();

            return !String.IsNullOrWhiteSpace(info.replyString);
        }


    }
}