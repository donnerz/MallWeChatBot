//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WeatherSample
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using Microsoft.Bot.Builder.Dialogs;
//    using Microsoft.Bot.Builder.Luis;
//    using Microsoft.Bot.Builder.Luis.Models;
//    using Newtonsoft.Json;
//    using Newtonsoft.Json.Linq;
//    using System.Xml;
//    using System.Net.Http;

//    [LuisModel("e9c3754f-6a6f-4db6-89ec-9f2a7033eb8b", "f184f4ba386646d4aaa06a2e03398c84")]
//    [Serializable]
//    public class WeatherDialog : LuisDialog<object>
//    {
//        public const string Entity_location = "Location";



//        [LuisIntent("查询天气")]
//        public async Task QueryWeather(IDialogContext context, LuisResult result)
//        {
//            string location = string.Empty;
//            string replyString = "";

//            if (TryToFindLocation(result, out location))
//            {
//                replyString = GetWeather(location);

//                JObject WeatherResult = (JObject)JsonConvert.DeserializeObject(replyString);
//                var weatherinfo = new
//                {
//                    城市 = WeatherResult["weatherinfo"]["city"].ToString(),
//                    温度 = WeatherResult["weatherinfo"]["temp"].ToString(),
//                    湿度 = WeatherResult["weatherinfo"]["SD"].ToString(),
//                    风向 = WeatherResult["weatherinfo"]["WD"].ToString(),
//                    风力 = WeatherResult["weatherinfo"]["WS"].ToString()
//                };


//                await context.PostAsync(weatherinfo.城市 + "的天气情况: 温度" + weatherinfo.温度 + "度;湿度" + weatherinfo.湿度 + ";风力" + weatherinfo.风力 + ";风向" + weatherinfo.风向);
//            }
//            else
//            {

//                await context.PostAsync("亲你要查询哪个地方的天气信息呢，快把城市的名字发给我吧");
//            }
//            context.Wait(MessageReceived);

//        }
//        private string GetWeather(string location)
//        {
//            string weathercode = "";
//            XmlDocument citycode = new XmlDocument();
//            citycode.Load("https://wqbot.blob.core.windows.net/botdemo/CityCode.xml");
//            XmlNodeList xnList = citycode.SelectNodes("//province//city//county");
//            foreach (XmlElement xnl in xnList)
//            {
//                if (xnl.GetAttribute("name").ToString() == location)
//                    weathercode = xnl.GetAttribute("weatherCode").ToString();
//            }
//            HttpClient client = new HttpClient();
//            string result = client.GetStringAsync("http://www.weather.com.cn/data/sk/" + weathercode + ".html").Result;
//            return result;
//        }
//        private bool TryToFindLocation(LuisResult result, out String location)
//        {
//            location = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("地点", out title))
//            {
//                location = title.Entity;
//            }
//            else
//            {
//                location = "";
//            }
//            return !location.Equals("");
//        }

//        //[LuisIntent("交钱")]
//        //public async Task PayMoney(IDialogContext context, LuisResult result)
//        //{
//        //    string expenseCategory = string.Empty;
//        //    string type = string.Empty;
//        //    string replyString = "";

//        //    if (TryToFindExpenseCategory(result, out expenseCategory, out type))
//        //    {
//        //        replyString = GetPrice(type);

//        //        await context.PostAsync("expenseCategory :" + expenseCategory + "; type:" + type + " 。 " + replyString);
//        //    }
//        //    else
//        //    {

//        //        await context.PostAsync("亲你要查询哪个什么费用信息呢，快把费用类别发给我吧");
//        //    }
//        //    context.Wait(MessageReceived);

//        //}
//        //private bool TryToFindExpenseCategory(LuisResult result, out String expenseCategory, out String type)
//        //{
//        //    expenseCategory = "";
//        //    type = "";
//        //    EntityRecommendation title;
//        //    type = "费用";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::空调费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::电费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::租金";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::物业管理费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::水费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::推广费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }
//        //    type = "费用::电话费";
//        //    if (result.TryFindEntity(type, out title))
//        //    {
//        //        expenseCategory = title.Entity;
//        //        return !expenseCategory.Equals("");
//        //    }

//        //    return !expenseCategory.Equals("");

//        //} 

//        //private String GetPrice(String type)
//        //{
//        //    string result = "";
//        //    if ("费用".Equals(type))
//        //    {
//        //        result = "哎呀，您说的费用正大君不知道哦。您可以问我：水费、电费、空调费、租金、物业管理费、推广费";
//        //    }
//        //    else if ("费用::水费".Equals(type) || "费用::电费".Equals(type) || "费用::空调费".Equals(type))
//        //    {
//        //        result = "具体详见正大商业《招商基准物业相关收费表》";
//        //    }
//        //    else if ("费用::租金".Equals(type))
//        //    {
//        //        result = "一般按月预收下月租金；具体视租赁铺位所在楼层、区域位置以及品牌业态决定价格及抽成比例；1-3年每年递增不低于8%；4-8年每2年递增不低于10%";
//        //    }
//        //    else if ("费用::物业管理费".Equals(type))
//        //    {
//        //        result = "具体详见正大商业《招商基准物业相关收费表》";
//        //    }
//        //    else if ("费用::电话费".Equals(type))
//        //    {
//        //        result = "电话费";
//        //    }
//        //    else if ("费用::推广费".Equals(type))
//        //    {
//        //        result = "税前月营业额的1.5%";
//        //    }
//        //    else
//        //    {
//        //        result = "";
//        //    }

//        //    return result;
//        //}

//        [LuisIntent("")]
//        public async Task None(IDialogContext context, LuisResult result)
//        {
//            //清空数据
//            ClearData();
//            string message = $"您好啊，有什么可以帮到您的吗？正大君目前提供：租赁咨询、查询费用、查询基本信息、查询租赁信息";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        //租赁咨询
//        //目的
//        public static string intent = "";
//        //项目
//        public static string projectText = "";
//        public static string projectType = "";
//        //楼层
//        public static string floorText = "";
//        //public string floorType = "";
//        //业态
//        public static string businessFormatText = "";
//        public static string businessFormatType = "";
//        //面积
//        public static string areaText = "";
//        //费用
//        public static string expenseCategoryText = "";
//        public static string expenseCategoryType = "";
//        //购物中心基本信息
//        public static string basicInfoText = "";
//        public static string basicInfoType = "";
//        //租赁信息
//        public static string rentInfoText = "";
//        public static string rentInfoType = "";

//        //清空数据
//        private void ClearData()
//        {
//            WeatherDialog.projectText = "";
//            WeatherDialog.projectType = "";
//            WeatherDialog.floorText = "";
//            WeatherDialog.businessFormatText = "";
//            WeatherDialog.businessFormatType = "";
//            WeatherDialog.areaText = "";
//            WeatherDialog.expenseCategoryText = "";
//            WeatherDialog.expenseCategoryType = "";
//            WeatherDialog.intent = "";
//            WeatherDialog.rentInfoText = "";
//            WeatherDialog.rentInfoType = "";
//        }

//        //设置基础信息信息
//        private void GetBasicInfo(IDialogContext context, LuisResult result)
//        {
//            //设置项目
//            string projectText = "";
//            string projectType = "";
//            bool projectFlag = TryToFindProject(result, out projectText, out projectType);
//            if (projectFlag)
//            {
//                WeatherDialog.projectText = projectText;
//                WeatherDialog.projectType = projectType;
//            }
//            //设置楼层
//            string floorText = "";
//            bool floorFlag = TryToFindFloor(result, out floorText);
//            if (floorFlag)
//            {
//                WeatherDialog.floorText = floorText;
//            }
//            //设置业态
//            string businessFormatText = "";
//            string businessFormatType = "";
//            bool businessFormatFlag = TryToFindBusinessFormat(result, out businessFormatText, out businessFormatType);
//            if (businessFormatFlag)
//            {
//                WeatherDialog.businessFormatText = businessFormatText;
//                WeatherDialog.businessFormatType = businessFormatType;
//            }
//            //设置面积
//            string areaText = "";
//            bool areaFlag = TryToFindArea(result, out areaText);
//            if (areaFlag)
//            {
//                WeatherDialog.areaText = areaText;
//            }
//            //设置费用
//            string expenseCategoryText = "";
//            string expenseCategoryType = "";
//            bool expenseCategoryFlag = TryToFindExpenseCategory(result, out expenseCategoryText, out expenseCategoryType);
//            if (expenseCategoryFlag)
//            {
//                WeatherDialog.expenseCategoryText = expenseCategoryText;
//                WeatherDialog.expenseCategoryType = expenseCategoryType;
//            }
//            //设置购物中心基础信息
//            string basicInfoText = "";
//            string basicInfoType = "";
//            bool basicInfoFlag = TryToFindBasicInfo(result, out basicInfoText, out basicInfoType);
//            if (basicInfoFlag)
//            {
//                WeatherDialog.basicInfoText = basicInfoText;
//                WeatherDialog.basicInfoType = basicInfoType;
//            }
//            //设置租赁信息
//            string rentInfoText = "";
//            string rentInfoType = "";
//            bool rentInfoFlag = TryToFindRentInfo(result, out rentInfoText, out rentInfoType);
//            if (rentInfoFlag)
//            {
//                WeatherDialog.rentInfoText = rentInfoText;
//                WeatherDialog.rentInfoType = rentInfoType;
//            }
//        }

//        [LuisIntent("获取信息")]
//        public async Task GetInfo(IDialogContext context, LuisResult result)
//        {
//            //设置基础信息
//            GetBasicInfo(context, result);
//            //根据intent调用
//            if ("租赁咨询".Equals(WeatherDialog.intent))
//            {
//                await QueryCommon(context, result);
//            }
//            else if ("查询费用".Equals(WeatherDialog.intent))
//            {
//                await QueryPrice(context, result);
//            }
//            else if ("查询基本信息".Equals(WeatherDialog.intent))
//            {
//                await QueryBasicInfo(context, result);
//            }
//            else if ("查询租赁信息".Equals(WeatherDialog.intent))
//            {
//                await QueryRentInfo(context, result);
//            }
//            else
//            {
//                await context.PostAsync("正大君目前提供：查询费用、查询基本信息、查询租赁信息。请问您需要什么服务呢？");
//            }
//            context.Wait(MessageReceived);
//        }


//        [LuisIntent("租赁咨询")]
//        public async Task QueryCommon(IDialogContext context, LuisResult result)
//        {
//            WeatherDialog.intent = "租赁咨询";
//            //设置基础信息
//            GetBasicInfo(context, result);
//            string replyString = "";
//            //await context.PostAsync(WeatherDialog.intent);
//            if (JudgeCommon(out replyString))
//            {
//                await context.PostAsync(replyString);
//            }
//            else
//            {
//                await context.PostAsync("好的，基础信息正大君知道啦，您想问些什么呢？正大君目前提供：查询费用、查询基本信息。");
//            }
//            context.Wait(MessageReceived);      
//        }

//        //判断通用
//        private bool JudgeCommon(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if ("".Equals(WeatherDialog.projectType))
//            {
//                replyString = "正大君还不知道您想了解的是哪个商场哦。您可以回答：正大广场陆家嘴店、正大乐城徐汇店等等";
//            }
//            else if ("".Equals(WeatherDialog.floorText))
//            {
//                replyString = "正大君还不知道您想了解的是哪个楼层哦。您可以回答：一楼、负一楼等等";
//            }
//            else if ("".Equals(WeatherDialog.businessFormatType))
//            {
//                replyString = "正大君还不知道您想了解的是哪个业态哦。您可以回答：零售、非零售等等";
//            }
//            else if ("".Equals(WeatherDialog.areaText))
//            {
//                replyString = "正大君还不知道您想租多大面积哦。您可以回答：100平米等等";
//            }
//            else if ("项目".Equals(WeatherDialog.projectType))
//            {
//                replyString = "哎呀，您说的商场“" + WeatherDialog.projectText + "”正大君不认识哦。您可以问我：正大广场陆家嘴店、正大乐城徐汇店等等";
//            }
//            else if ("业态".Equals(WeatherDialog.businessFormatType))
//            {
//                replyString = "哎呀，您说的业态“" + WeatherDialog.businessFormatText + "”正大君不认识哦。您可以问我：零售、非零售、餐饮、服装等等";
//            }
//            return !replyString.Equals("");
//        }

//        //项目
//        private bool TryToFindProject(LuisResult result, out String projectText, out String projectType)
//        {
//            projectText = "";
//            projectType = "";
//            EntityRecommendation title;
//            string[] projectTypeArr = {"项目", "项目::正大广场陆家嘴店", "项目::正大乐成徐汇店" };
//            foreach(string str in projectTypeArr)
//            {
//                projectType = str;
//                if (result.TryFindEntity(projectType, out title))
//                {
//                    projectText = title.Entity;
//                    return !projectText.Equals("");
//                }
//            }
//            return !projectText.Equals("");
//        }
//        //楼层
//        private bool TryToFindFloor(LuisResult result, out String floorText)
//        {
//            floorText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("楼层", out title))
//            {
//                floorText = title.Entity;
//            }
//            else
//            {
//                floorText = "";
//            }
//            return !floorText.Equals("");
//        }
//        //业态
//        private bool TryToFindBusinessFormat(LuisResult result, out String businessFormatText, out String businessFormatType)
//        {
//            businessFormatText = "";
//            businessFormatType = "";
//            EntityRecommendation title;
//            string[] businessFormatTypeArr = { "业态", "业态::零售", "业态::非零售" };
//            foreach (string str in businessFormatTypeArr)
//            {
//                businessFormatType = str;
//                if (result.TryFindEntity(businessFormatType, out title))
//                {
//                    businessFormatText = title.Entity;
//                    return !businessFormatText.Equals("");
//                }
//            }
//            return !businessFormatText.Equals("");
//        }
//        //楼层
//        private bool TryToFindArea(LuisResult result, out String areaText)
//        {
//            areaText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("面积", out title))
//            {
//                areaText = title.Entity;
//            }
//            else
//            {
//                areaText = "";
//            }
//            return !areaText.Equals("");
//        }
//        //费用
//        private bool TryToFindExpenseCategory(LuisResult result, out String expenseCategoryText, out String expenseCategoryType)
//        {
//            expenseCategoryText = "";
//            expenseCategoryType = "";
//            EntityRecommendation title;
//            string[] expenseCategoryTypeArr = { "费用", "费用::空调费", "费用::电费", "费用::租金", "费用::推广费", "费用::保证金", "费用::物业管理费", "费用::水费", "费用::保证金" };
//            foreach (string str in expenseCategoryTypeArr)
//            {
//                expenseCategoryType = str;
//                if (result.TryFindEntity(expenseCategoryType, out title))
//                {
//                    expenseCategoryText = title.Entity;
//                    return !expenseCategoryText.Equals("");
//                }
//            }
//            return !expenseCategoryText.Equals("");
//        }
//        //购物中心基础信息
//        private bool TryToFindBasicInfo(LuisResult result, out String basicInfoText, out String basicInfoType)
//        {
//            basicInfoText = "";
//            basicInfoType = "";
//            EntityRecommendation title;
//            string[] basicInfoTypeArr = { "购物中心基本信息", "购物中心基本信息::数量", "购物中心基本信息::地址",
//                "购物中心基本信息::档次定位", "购物中心基本信息::物业", "购物中心基本信息::入驻品牌", "购物中心基本信息::人流量", "购物中心基本信息::营业时间" };
//            foreach (string str in basicInfoTypeArr)
//            {
//                basicInfoType = str;
//                if (result.TryFindEntity(basicInfoType, out title))
//                {
//                    basicInfoText = title.Entity;
//                    return !basicInfoText.Equals("");
//                }
//            }
//            return !basicInfoText.Equals("");
//        }
//        //租赁信息
//        private bool TryToFindRentInfo(LuisResult result, out String rentInfoText, out String rentInfoType)
//        {
//            rentInfoText = "";
//            rentInfoType = "";
//            EntityRecommendation title;
//            string[] rentInfoTypeArr = { "租赁信息", "租赁信息::装修期"};
//            foreach (string str in rentInfoTypeArr)
//            {
//                rentInfoType = str;
//                if (result.TryFindEntity(rentInfoType, out title))
//                {
//                    rentInfoText = title.Entity;
//                    return !rentInfoText.Equals("");
//                }
//            }
//            return !rentInfoText.Equals("");
//        }

//        [LuisIntent("查询费用")]
//        public async Task QueryPrice(IDialogContext context, LuisResult result)
//        {
//            WeatherDialog.intent = "查询费用";
//            GetBasicInfo(context, result);
//            string replyString = "";
//            //await context.PostAsync(WeatherDialog.intent);
//            if (JudgeCommon(out replyString))
//            {
//                await context.PostAsync(replyString);
//            }
//            else
//            {
//                if (JudgePrice(out replyString))
//                {
//                    await context.PostAsync(replyString);
//                }
//                else
//                {
//                    await context.PostAsync("正大君还不知道您想了解的费用是什么哦。您可以回答：水费、电费、空调费、租金、物业管理费、推广费");
//                }
//            }
//            context.Wait(MessageReceived);
//        }
//        //判断价格
//        private bool JudgePrice(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if ("费用".Equals(expenseCategoryType))
//            {
//                replyString = "哎呀，您说的费用“" + WeatherDialog.expenseCategoryText + "”正大君不知道哦。您可以问我：水费、电费、空调费、租金、物业管理费、推广费";
//            }
//            else if ("费用::水费".Equals(WeatherDialog.expenseCategoryType) || "费用::电费".Equals(WeatherDialog.expenseCategoryType) || "费用::空调费".Equals(WeatherDialog.expenseCategoryType))
//            {
//                replyString = WeatherDialog.expenseCategoryText + "请详见" + WeatherDialog.projectText + "《招商基准物业相关收费表》";
//            }
//            else if ("费用::租金".Equals(WeatherDialog.expenseCategoryType))
//            {
//                string money = new Random().Next(65, 90).ToString();
//                replyString = "您想要查询“" + WeatherDialog.projectText + "”“" + WeatherDialog.floorText + "”“" + WeatherDialog.businessFormatText + "”的预估租金为：" + money + "元/平米/天。此价格为参考价，更精确的报价请您先注册/登录吧！";
//            }
//            else if ("费用::物业管理费".Equals(WeatherDialog.expenseCategoryType))
//            {
//                replyString = WeatherDialog.expenseCategoryText + "请详见" + WeatherDialog.projectText + "《招商基准物业相关收费表》";
//            }
//            else if ("费用::推广费".Equals(WeatherDialog.expenseCategoryType))
//            {
//                replyString = WeatherDialog.expenseCategoryText + "一般为税前月营业额的1.5%，具体视实际合同谈判情况。";
//            }
//            else if ("费用::保证金".Equals(WeatherDialog.expenseCategoryType))
//            {
//                replyString = "根据《招商基准物业相关收费表》，在您入驻之前需要交纳履约" + WeatherDialog.expenseCategoryText + "，一般为合同期内最高月的租金总收入（租金+物业管理费）的3倍。";
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("查询基本信息")]
//        public async Task QueryBasicInfo(IDialogContext context, LuisResult result)
//        {
//            WeatherDialog.intent = "查询基本信息";
//            GetBasicInfo(context, result);
//            string replyString = "";
//            //await context.PostAsync(WeatherDialog.intent);
//            if (JudgeBasicInfo(out replyString))
//            {
//                await context.PostAsync(replyString);
//            }
//            else
//            {
//                await context.PostAsync("OK，接下来您可以问我：购物中心数量、地址、档次定位是什么、物业谁管理、入驻品牌有哪些、人流量有多少");
//            }
//            context.Wait(MessageReceived);
//        }
//        //判断基础信息
//        private bool JudgeBasicInfo(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if ("购物中心基本信息".Equals(WeatherDialog.basicInfoType))
//            {
//                replyString = "哎呀，您说的这类基本信息“" + WeatherDialog.expenseCategoryText + "”正大君不知道哦。您可以问我：购物中心有几家、地址、档次定位是什么、物业谁管理、入驻品牌有哪些、人流量有多少";
//            }
//            else if ("购物中心基本信息::数量".Equals(WeatherDialog.basicInfoType))
//            {
//                replyString = "您好，正大商业购物中心共分正大广场陆家嘴店，正大乐城徐汇店，正大乐城宝山店，正大乐城无锡店，正大乐城郑州店，正大乐城杨高店共6家店";
//            }
//            else
//            {
//                if ("".Equals(WeatherDialog.projectType))
//                {
//                    replyString = "正大君还不知道您想了解的是哪个商场哦。您可以回答：正大广场陆家嘴店、正大乐城徐汇店等等";
//                }
//                else if ("项目".Equals(WeatherDialog.projectType))
//                {
//                    replyString = "哎呀，您说的商场“" + WeatherDialog.projectText + "”正大君不认识哦。您可以问我：正大广场陆家嘴店、正大乐城徐汇店等等";
//                }
//                else
//                {
//                    if ("购物中心基本信息::地址".Equals(WeatherDialog.basicInfoType))
//                    {
//                        if ("项目::正大广场陆家嘴店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "在上海市浦东新区陆家嘴西路168号";
//                        }
//                        else if ("项目::正大乐成徐汇店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "在上海市徐汇区中山南二路699号";
//                        }
//                    }
//                    else if ("购物中心基本信息::档次定位".Equals(WeatherDialog.basicInfoType))
//                    {
//                        if ("项目::正大广场陆家嘴店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "位于“东方华尔街”之称的上海陆家嘴核心商圈，入驻品牌以中端为主，主要客户群为周边居民及商圈白领";
//                        }
//                        else if ("项目::正大乐成徐汇店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "位于徐家汇附近，入驻品牌以中端为主，主要客户群为周边居民及商圈白领";
//                        }
//                    }
//                    else if ("购物中心基本信息::入驻品牌".Equals(WeatherDialog.basicInfoType))
//                    {
//                        if ("项目::正大广场陆家嘴店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "入驻品牌有ZARA、HM等等";
//                        }
//                        else if ("项目::正大乐成徐汇店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "入驻品牌有卜蜂莲花、面包新语等等";
//                        }
//                    }
//                    else if ("购物中心基本信息::人流量".Equals(WeatherDialog.basicInfoType))
//                    {
//                        if ("项目::正大广场陆家嘴店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "人流量在日均6万人次左右";
//                        }
//                        else if ("项目::正大乐成徐汇店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "人流量在日均3万人次左右";
//                        }
//                    }
//                    else if ("购物中心基本信息::营业时间".Equals(WeatherDialog.basicInfoType))
//                    {
//                        if ("项目::正大广场陆家嘴店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "营业时间为：10:00—22:00（周一到周日）";
//                        }
//                        else if ("项目::正大乐成徐汇店".Equals(WeatherDialog.projectType))
//                        {
//                            replyString = WeatherDialog.projectText + "营业时间为：10:00—22:00（周一到周日）";
//                        }
//                    }
//                }
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("查询租赁信息")]
//        public async Task QueryRentInfo(IDialogContext context, LuisResult result)
//        {
//            WeatherDialog.intent = "查询租赁信息";
//            GetBasicInfo(context, result);
//            string replyString = "";
//            //await context.PostAsync(WeatherDialog.intent);

//            if (JudgeCommon(out replyString))
//            {
//                await context.PostAsync(replyString);
//            }
//            else
//            {
//                if (JudgeRentInfo(out replyString))
//                {
//                    await context.PostAsync(replyString);
//                }
//                else
//                {
//                    await context.PostAsync("您想了解什么租赁信息呢？您可以问我：装修期多久？是否免租？");
//                }
//            }
//            context.Wait(MessageReceived);
//        }
//        //判断租赁信息
//        private bool JudgeRentInfo(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if ("租赁信息".Equals(WeatherDialog.rentInfoType))
//            {
//                replyString = "哎呀，您想问的租赁信息“" + WeatherDialog.rentInfoText + "”正大君不知道哦。您可以问我：装修期多久？是否免租？";
//            }
//            else if ("租赁信息::装修期".Equals(WeatherDialog.rentInfoType))
//            {
//                string days = new Random().Next(10, 50).ToString();
//                replyString = "您想要查询“" + WeatherDialog.projectText + "”“" + WeatherDialog.floorText + "”“" + WeatherDialog.businessFormatText + "”“" + WeatherDialog.rentInfoText 
//                    + "”的预估装修期为：" + days + "天左右，仅供参考。是否免租视具体合同决定";
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("感谢")]
//        public async Task Thanks(IDialogContext context, LuisResult result)
//        {
//            //清空数据
//            ClearData();
//            string message = $"很高兴为您服务，谢谢！";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//    }
//}
