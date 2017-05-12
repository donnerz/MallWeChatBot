//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace CRMSampleBat
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
//    using System.IO;
//    using Common;


//    [LuisModel("aa9aaa25-49a8-4ace-871f-caec89c6b09d", "fd0a1ed740f84f57a80544c68fa43411")]
//    [Serializable]
//    public class CRMDialog : LuisDialog<object>
//    {
//        public const string Entity_location = "Location";

//        //简单回答
//        [LuisIntent("")]
//        public async Task None(IDialogContext context, LuisResult result)
//        {
//            //清空数据
//            ClearData(true);
//            string message = $"您好！正大君不清楚您想要干什么？";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("我要停车")]
//        public async Task Park(IDialogContext context, LuisResult result)
//        {
//            string message = $"您好！地下1楼、地下2楼、地下3楼都可以停车。未来我们可以提供车位预定，停车费支付，停车券兑换等等服务。";
//            //message += "具体信息参见<href a='http://mp.weixin.qq.com/s?__biz=MjM5NDEwNDIwMA==&tempkey=kBHtkbZAY%2BYN%2BtS1nv3e8kd%2FjhmNTIwUYPDy945V6EEGFLaTe0eHA3uKTE3DggvOTJkGf0UOkM7YzUoPz%2BxTrxg42VmbGTF6i4%2BhkU5noyr1eECtBC4ly8uYkoYfbCI82t45YtQ951Vx2XD25CZ4HQ%3D%3D&chksm=3e8b35af09fcbcb9d8ad8b5d1e0df054a2755ced8a37eaf765270438f87bec852db09cccc239#rd' target='_blank'>消息</a>";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("我要看电影")]
//        public async Task SeeMovies(IDialogContext context, LuisResult result)  
//        {
//            string message = $"您好！电影院在8楼。未来我们可以提供电影票预定，电影票兑换等等服务。";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("我要唱歌")]
//        public async Task Sing(IDialogContext context, LuisResult result)
//        {
//            string message = $"您好！KTV在8楼哦。快去欢唱吧！";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("打招呼")]
//        public async Task SayHello(IDialogContext context, LuisResult result)
//        {
//            string message = $"您好！我是客服正大君，有什么可以帮到您的吗？";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("感谢")]
//        public async Task SayByeBye(IDialogContext context, LuisResult result)
//        {
//            //清空数据
//            ClearData(true);
//            string message = $"很高兴为您服务，谢谢！";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        //复杂回答 **************************************************************************
//        //目的
//        public static string intent = "";
//        //品牌
//        public static string brandText = "";
//        //业态
//        public static string businessFormatText = "";
//        //楼层
//        public static string floorText = "";
//        //单品
//        public static string productText = "";
        
//        //特约商户标志
//        public static bool vipFlag = false;

//        //清空数据
//        private void ClearData(bool intentFlag)
//        {
//            //意图先不清除
//            if (intentFlag)
//            {
//                CRMDialog.intent = "";
//            }
//            CRMDialog.brandText = "";
//            CRMDialog.businessFormatText = "";
//            CRMDialog.floorText = "";
//            CRMDialog.productText = "";

//            CRMDialog.vipFlag = false;
//        }

//        //设置基础信息信息
//        private void GetBasicInfo(IDialogContext context, LuisResult result)
//        {
//            //设置品牌
//            string brandText = "";
//            bool brandFlag = TryToFindBrand(result, out brandText);
//            if (brandFlag)
//            {
//                CRMDialog.brandText = brandText;
//            }
//            //设置业态
//            string businessFormatText = "";
//            bool businessFormatFlag = TryToFindBusinessFormat(result, out businessFormatText);
//            if (businessFormatFlag)
//            {
//                CRMDialog.businessFormatText = businessFormatText;
//            }
//            //设置楼层
//            string floorText = "";
//            bool floorFlag = TryToFindFloor(result, out floorText);
//            if (floorFlag)
//            {
//                CRMDialog.floorText = floorText;
//            }
//            //设置单品
//            string productText = "";
//            bool productFlag = TryToFindProduct(result, out productText);
//            if (productFlag)
//            {
//                CRMDialog.productText = productText;
//            }
//        }

//        //品牌
//        private bool TryToFindBrand(LuisResult result, out String brandText)
//        {
//            brandText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("品牌", out title))
//            {
//                brandText = title.Entity.Replace(" ", "");
//            }
//            else
//            {
//                brandText = "";
//            }
//            return !brandText.Equals("");
//        }

//        //业态
//        private bool TryToFindBusinessFormat(LuisResult result, out String businessFormatText)
//        {
//            businessFormatText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("业态", out title))
//            {
//                businessFormatText = title.Entity.Replace(" ", "");
//            }
//            else
//            {
//                businessFormatText = "";
//            }
//            return !businessFormatText.Equals("");
//        }

//        //楼层
//        private bool TryToFindFloor(LuisResult result, out String floorText)
//        {
//            floorText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("楼层", out title))
//            {
//                floorText = title.Entity.Replace(" ", "");
//            }
//            else
//            {
//                floorText = "";
//            }
//            return !floorText.Equals("");
//        }

//        //单品
//        private bool TryToFindProduct(LuisResult result, out String productText)
//        {
//            productText = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("单品", out title))
//            {
//                productText = title.Entity.Replace(" ", "");
//            }
//            else
//            {
//                productText = "";
//            }
//            return !productText.Equals("");
//        }

//        [LuisIntent("提供信息")]
//        public async Task ProvideInfo(IDialogContext context, LuisResult result)
//        {
//            //设置基础信息
//            GetBasicInfo(context, result);
//            //根据intent调用
//            if ("查询地点".Equals(CRMDialog.intent))
//            {
//                await QueryLocation(context, result);
//            }
//            else if ("有什么".Equals(CRMDialog.intent))
//            {
//                await HaveSomething(context, result);
//            }
//            else if ("我要吃饭".Equals(CRMDialog.intent))
//            {
//                await Eat(context, result);
//            }
//            else if ("我要购物".Equals(CRMDialog.intent))
//            {
//                await Buy(context, result);
//            }
//            else if ("查询特约商户".Equals(CRMDialog.intent))
//            {
//                await QueryMemberStore(context, result);
//            }
//            else
//            {
//                if (!String.IsNullOrWhiteSpace(CRMDialog.brandText))
//                {
//                    await QueryLocation(context, result);
//                }
//                else if (!String.IsNullOrWhiteSpace(CRMDialog.businessFormatText) || !String.IsNullOrWhiteSpace(CRMDialog.floorText))
//                {
//                    await HaveSomething(context, result);
//                }
//                if (!String.IsNullOrWhiteSpace(CRMDialog.productText))
//                {
//                    await QueryLocation(context, result);
//                }
//                else
//                {
//                    await context.PostAsync("正大君目前提供：地点查询，品牌查询等服务。请问您需要什么呢？");
//                }
//            }
//            context.Wait(MessageReceived);

//            //IList<EntityRecommendation> list = result.Entities;
//            //for (int i = 0; i < list.Count; i++)
//            //{
//            //    EntityRecommendation obj = list[i];
//            //    await context.PostAsync("Entity:" + obj.Entity + " Type:" + obj.Type + " Role" + obj.Role + " Score" + obj.Score);
//            //}
//            //context.Wait(MessageReceived);
//        }

//        [LuisIntent("查询地点")]
//        public async Task QueryLocation(IDialogContext context, LuisResult result)
//        {
//            CRMDialog.intent = "查询地点";
//            CRMDialog.vipFlag = false;

//            GetBasicInfo(context, result);
//            string replyString = "";
//            if (JudgeBrand1(out replyString))
//            {
//                await context.PostAsync(replyString);
//            }
//            else
//            {
//                if (JudgeBrand2(out replyString))
//                {
//                    await context.PostAsync(replyString);
//                }
//                else
//                {
//                    await context.PostAsync("对不起，您说的品牌" + CRMDialog.brandText + "正大君没查询到哦。");
//                }
//                //清空数据
//                ClearData(false);
//            }
//            context.Wait(MessageReceived);
//        }

//        //判断品牌1
//        private bool JudgeBrand1(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if ("".Equals(CRMDialog.brandText))
//            {
//                replyString = "正大君还不知道您想去哪哦。您可以回答：ZARA、HM等等";
//            }
//            return !replyString.Equals("");
//        }

//        //判断品牌2
//        private bool JudgeBrand2(out String replyString)
//        {
//            replyString = "";
//            if (ReadFile.brandDic.ContainsKey(CRMDialog.brandText) == true)
//            {
//                replyString = CRMDialog.brandText + "在" + ReadFile.brandDic[CRMDialog.brandText].Floor + " " + ReadFile.brandDic[CRMDialog.brandText].UnitNumber + "(门牌号)。";
//                replyString += ("特约商户".Equals(ReadFile.brandDic[CRMDialog.brandText].VipFee) ? "是特约商户，可以积分哦。" : "");
//                if ("餐饮".Equals(ReadFile.brandDic[CRMDialog.brandText].SecondBusiness))
//                {
//                    replyString += "未来我们还可以提供订位，排队，点餐等服务哦！";
//                }
//                else if ("餐饮".Equals(ReadFile.brandDic[CRMDialog.brandText].SecondBusiness) || "女装".Equals(ReadFile.brandDic[CRMDialog.brandText].SecondBusiness) || "男装".Equals(ReadFile.brandDic[CRMDialog.brandText].SecondBusiness))
//                {
//                    replyString += "未来我们还可以提供虚拟试衣、领取各种优惠券哦！";
//                }
//            }

//            return !replyString.Equals("");
//        }

//        [LuisIntent("有什么")]
//        public async Task HaveSomething(IDialogContext context, LuisResult result)
//        {
//            CRMDialog.intent = "有什么";
//            CRMDialog.vipFlag = false;

//            GetBasicInfo(context, result);
//            string replyString = "";
//            if (JudgeHaveSomething1(out replyString))
//            {
//                await context.PostAsync(replyString);
//                //清空数据
//                ClearData(false);
//            }
//            context.Wait(MessageReceived);
//        }

//        //判断有什么1
//        private bool JudgeHaveSomething1(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if (!"".Equals(CRMDialog.brandText))
//            {
//                if (JudgeBrand2(out replyString))
//                {
//                    return !replyString.Equals("");
//                }
//                else
//                {
//                    replyString = "对不起，您说的品牌" + CRMDialog.brandText + "正大君没查询到哦。";
//                    return !replyString.Equals("");
//                }
//            }
//            if (JudgeBusinessFormat1(out replyString))
//            {
//                return !replyString.Equals("");
//            }
//            return !replyString.Equals("");
//        }

//        //判断业态1
//        private bool JudgeBusinessFormat1(out String replyString)
//        {
//            replyString = "";
//            String brandStr = "";
//            //replyString += CRMDialog.intent + CRMDialog.vipFlag;
//            try
//            {
//                if (!String.IsNullOrWhiteSpace(CRMDialog.businessFormatText) || !String.IsNullOrWhiteSpace(CRMDialog.floorText))
//                {
//                    List<Brand> list = ReadFile.GetBrandList(CRMDialog.businessFormatText, CRMDialog.floorText, CRMDialog.vipFlag);
//                    //replyString += list.Count;
//                    if (list.Count > 0)
//                    {
//                        foreach (Brand brand in list)
//                        {
//                            Brand b = brand;
//                            brandStr = brandStr + b.BrandName + "、";
//                        }
//                        if (brandStr.Length > 0)
//                        {
//                            brandStr = brandStr.Substring(0, brandStr.Length - 1);
//                        }
//                        replyString += ("".Equals(CRMDialog.businessFormatText) ? "" : CRMDialog.businessFormatText) + (CRMDialog.vipFlag ? "的特约商户" : "") + "正大君"
//                            + ("".Equals(CRMDialog.floorText) ? "" : "在" + CRMDialog.floorText) + "找到了" + brandStr + "。您想去哪家呢？";
//                    }
//                    else
//                    {
//                        list = ReadFile.GetBrandList(CRMDialog.businessFormatText, null, CRMDialog.vipFlag);
//                        if (list.Count > 0)
//                        {
//                            foreach (Brand brand in list)
//                            {
//                                Brand b = brand;
//                                brandStr = brandStr + b.BrandName + "、";
//                            }
//                            if (brandStr.Length > 0)
//                            {
//                                brandStr = brandStr.Substring(0, brandStr.Length - 1);
//                            }
//                            replyString += "正大君" + ("".Equals(CRMDialog.floorText) ? "" : "在" + CRMDialog.floorText)
//                                + "没有找到" + CRMDialog.businessFormatText + (CRMDialog.vipFlag ? "的特约商户" : "") + "哦。" + CRMDialog.businessFormatText + "您可以尝试尝试" + brandStr;
//                        }
//                        else
//                        {
//                            replyString += "哎呀，正大君没有找到" + CRMDialog.businessFormatText + (CRMDialog.vipFlag ? "的特约商户" : "") + "哦。您换一个问法试试呗。";
//                        }

//                    }
//                }
//                else
//                { 
//                    List<Brand> list = ReadFile.GetAllBrandList(CRMDialog.vipFlag);
//                    foreach (Brand brand in list)
//                    {
//                        Brand b = brand;
//                        brandStr = brandStr + b.BrandName + "、";
//                    }
//                    if (brandStr.Length > 0)
//                    {
//                        brandStr = brandStr.Substring(0, brandStr.Length - 1);
//                    }
//                    if (CRMDialog.vipFlag)
//                    {
//                        replyString += "正大广场的特约商铺有好多呢。比如" + brandStr + "。问“7楼女装特约商铺”可以获取更详细的信息哦!";
//                    }
//                    else
//                    {
//                        replyString += "正大广场有好多好多商铺。比如" + brandStr + "。问“7楼有什么女装”可以获取更详细的信息哦!";
//                    }
//                }
                
//            }
//            catch (Exception e)
//            {
//                replyString += "我在JudgeBusinessFormat1报错啦" + e.StackTrace;
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("我要吃饭")]
//        public async Task Eat(IDialogContext context, LuisResult result)
//        {
//            CRMDialog.intent = "我要吃饭";
//            CRMDialog.vipFlag = false;

//            GetBasicInfo(context, result);
//            string replyString = "";
//            if (JudgeEat1(out replyString))
//            {
//                await context.PostAsync(replyString);
//                //清空数据
//                ClearData(false);
//            }
//            context.Wait(MessageReceived);
//        }

//        //判断吃饭1
//        private bool JudgeEat1(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if (!"".Equals(CRMDialog.brandText))
//            {
//                if (JudgeBrand2(out replyString))
//                {
//                    return !replyString.Equals("");
//                }
//                else
//                {
//                    replyString = "对不起，您说的品牌" + CRMDialog.brandText + "正大君没查询到哦。";
//                    return !replyString.Equals("");
//                }
//            }
//            if ("".Equals(CRMDialog.businessFormatText))
//            {
//                CRMDialog.businessFormatText = "餐饮";
//            }
//            if (JudgeBusinessFormat1(out replyString))
//            {
//                return !replyString.Equals("");
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("我要购物")]
//        public async Task Buy(IDialogContext context, LuisResult result)
//        {
//            CRMDialog.intent = "我要购物";
//            CRMDialog.vipFlag = false;

//            GetBasicInfo(context, result);
//            string replyString = "";
//            if (JudgeBuy1(out replyString))
//            {
//                await context.PostAsync(replyString);
//                //清空数据
//                ClearData(false);
//            }
//            context.Wait(MessageReceived);
//        }

//        //判断购物1
//        private bool JudgeBuy1(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if (!"".Equals(CRMDialog.brandText))
//            {
//                if (JudgeBrand2(out replyString))
//                {
//                    return !replyString.Equals("");
//                }
//                else
//                {
//                    replyString = "对不起，您说的品牌" + CRMDialog.brandText + "正大君没查询到哦。";
//                    return !replyString.Equals("");
//                }
//            }
//            if ("".Equals(CRMDialog.businessFormatText))
//            {
//                CRMDialog.businessFormatText = "男装";
//            }
//            if (JudgeBusinessFormat1(out replyString))
//            {
//                return !replyString.Equals("");
//            }
//            return !replyString.Equals("");
//        }

//        [LuisIntent("查询特约商户")]
//        public async Task QueryMemberStore(IDialogContext context, LuisResult result)
//        {
//            CRMDialog.intent = "查询特约商户";
//            CRMDialog.vipFlag = true;

//            GetBasicInfo(context, result);
//            string replyString = "";
//            if (JudgeMemberStore1(out replyString))
//            {
//                await context.PostAsync(replyString);
//                //清空数据
//                ClearData(true);
//            }
//            context.Wait(MessageReceived);
//        }

//        //判断特约商铺1
//        private bool JudgeMemberStore1(out String replyString)
//        {
//            replyString = "";
//            //判断开始
//            if (!"".Equals(CRMDialog.brandText))
//            {
//                if (JudgeBrand3(out replyString))
//                {
//                    return !replyString.Equals("");
//                }
//                else
//                {
//                    replyString = "对不起，您说的品牌" + CRMDialog.brandText + "正大君没查询到哦。";
//                    return !replyString.Equals("");
//                }
//            }
//            if (JudgeBusinessFormat1(out replyString))
//            {
//                return !replyString.Equals("");
//            }
//            return !replyString.Equals("");
//        }

//        //判断品牌3
//        private bool JudgeBrand3(out String replyString)
//        {
//            replyString = "";
//            if (ReadFile.brandDic.ContainsKey(CRMDialog.brandText) == true)
//            {
//                if ("特约商户".Equals(ReadFile.brandDic[CRMDialog.brandText].VipFee))
//                {
//                    replyString = CRMDialog.brandText + "是特约商户，可以积分哦！";
//                }
//                else
//                {
//                    replyString = CRMDialog.brandText + "目前还不是是特约商户，不可以积分！";
//                }
//            }
//            return !replyString.Equals("");
//        }

       
//    }
//}
