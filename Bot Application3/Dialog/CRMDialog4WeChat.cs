using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dialog
{

    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;

    using System.Net.Http;

    using Service;
    using Model.Domian;


    [LuisModel("", "")]
    [Serializable]
    public class CRMDialog4WeChat : LuisDialog<object>
    {
        public const string Entity_location = "Location";

        public string TestCustomer = "TestCustomer";

        public CRMDialog4WeChat(string from)
        {
            TestCustomer = from;
        }

        private CRMService4WeChat crmservice = new CRMService4WeChat();

        //简单回答
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            //清空数据
            crmservice.ClearData(info, true);
            string message = $"您好！正大君不清楚您想要干什么？";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        //[LuisIntent("我要停车")]
        //public async Task Park(IDialogContext context, LuisResult result)
        //{
        //    string message = $"您好！地下1楼、地下2楼、地下3楼都可以停车。未来我们可以提供车位预定，停车费支付，停车券兑换等等服务。";
        //    //message += "具体信息参见<href a='http://mp.weixin.qq.com/s?__biz=MjM5NDEwNDIwMA==&tempkey=kBHtkbZAY%2BYN%2BtS1nv3e8kd%2FjhmNTIwUYPDy945V6EEGFLaTe0eHA3uKTE3DggvOTJkGf0UOkM7YzUoPz%2BxTrxg42VmbGTF6i4%2BhkU5noyr1eECtBC4ly8uYkoYfbCI82t45YtQ951Vx2XD25CZ4HQ%3D%3D&chksm=3e8b35af09fcbcb9d8ad8b5d1e0df054a2755ced8a37eaf765270438f87bec852db09cccc239#rd' target='_blank'>消息</a>";
        //    await context.PostAsync(message);
        //    context.Wait(MessageReceived);
        //}

        [LuisIntent("我要看电影")]
        public async Task SeeMovies(IDialogContext context, LuisResult result)
        {
            string message = $"您好！电影院在8楼。未来我们可以提供电影票预定，电影票兑换等等服务。";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("我要唱歌")]
        public async Task Sing(IDialogContext context, LuisResult result)
        {
            string message = $"您好！KTV在8楼哦。快去欢唱吧！";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("打招呼")]
        public async Task SayHello(IDialogContext context, LuisResult result)
        {
            string message = $"您好！我是客服正大君，有什么可以帮到您的吗？" + TestCustomer;
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("感谢")]
        public async Task SayByeBye(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            //清空数据
            crmservice.ClearData(info, true);
            string message = $"很高兴为您服务，谢谢！";
            //message += BaseInfo.wordDic["1"].name + BaseInfo.synonymDic["1"].name + BaseInfo.wordDic[BaseInfo.brandDic["1"].brand].name;

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        //复杂回答 **************************************************************************

        [LuisIntent("提供信息")]
        public async Task ProvideInfo(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            //设置通用信息
            crmservice.SetCommonInfo(info, context, result);
            //await context.PostAsync(info.ToString());
            //根据intent调用

            if (!String.IsNullOrWhiteSpace(info.brandText))
            {
                if ("查询地点".Equals(info.intent))
                {
                    await QueryLocation(context, result);
                }
                else if ("有什么".Equals(info.intent))
                {
                    await HaveSomething(context, result);
                }
                else if ("我要吃饭".Equals(info.intent))
                {
                    await Eat(context, result);
                }
                else if ("我要购物".Equals(info.intent))
                {
                    await Buy(context, result);
                }
                else if ("查询特约商户".Equals(info.intent))
                {
                    await QueryMemberStore(context, result);
                }
                else if ("查询优惠券".Equals(info.intent))
                {
                    await QueryCoupon(context, result);
                }
                else
                {
                    await QueryLocation(context, result);
                }
            }
            else if (!String.IsNullOrWhiteSpace(info.basicInfoText))
            {
                if ("查询基础信息".Equals(info.intent))
                {
                    await QueryBasicInfo(context, result);
                }
                else if ("查询活动".Equals(info.intent))
                {
                    await QueryActivitys(context, result);
                }
                else
                {
                    await QueryBasicInfo(context, result);
                }
            }
            else if (!String.IsNullOrWhiteSpace(info.activityText))
            {
                if ("查询活动".Equals(info.intent))
                {
                    await QueryActivitys(context, result);
                }
                else
                {
                    await QueryActivitys(context, result);
                }

            }
            else if (!String.IsNullOrWhiteSpace(info.businessFormatText) || !String.IsNullOrWhiteSpace(info.productText))
            {
                if ("有什么".Equals(info.intent))
                {
                    await HaveSomething(context, result);
                }
                else if ("我要吃饭".Equals(info.intent))
                {
                    await Eat(context, result);
                }
                else if ("我要购物".Equals(info.intent))
                {
                    await Buy(context, result);
                }
                else if ("查询特约商户".Equals(info.intent))
                {
                    await QueryMemberStore(context, result);
                }
                else
                {
                    await HaveSomething(context, result);
                }
            }
            else if (!String.IsNullOrWhiteSpace(info.floorText))
            {
                if ("有什么".Equals(info.intent))
                {
                    await HaveSomething(context, result);
                }
                else if ("我要吃饭".Equals(info.intent))
                {
                    await Eat(context, result);
                }
                else if ("我要购物".Equals(info.intent))
                {
                    await Buy(context, result);
                }
                else if ("查询特约商户".Equals(info.intent))
                {
                    await QueryMemberStore(context, result);
                }
                else if ("查询活动".Equals(info.intent))
                {
                    await QueryActivitys(context, result);
                }
                else
                {
                    await HaveSomething(context, result);
                }
            }
            else
            {
                bool b = true;
                //有默认值必须最后判断
                if (!String.IsNullOrWhiteSpace(info.BuiltinDatetimeDate))
                {
                    if ("查询活动".Equals(info.intent))
                    {
                        b = false;
                        await QueryActivitys(context, result);
                    }
                }

                if (b)
                {
                    //await context.PostAsync(info.ToString());
                    await context.PostAsync("正大君目前提供：地点查询，品牌查询，活动查询等服务。请问您需要什么呢？");
                }
            }

            context.Wait(MessageReceived);



            //if ("查询地点".Equals(info.intent))
            //{
            //    await QueryLocation(context, result);
            //}
            //else if ("有什么".Equals(info.intent))
            //{
            //    await HaveSomething(context, result);
            //}
            //else if ("我要吃饭".Equals(info.intent))
            //{
            //    await Eat(context, result);
            //}
            //else if ("我要购物".Equals(info.intent))
            //{
            //    await Buy(context, result);
            //}
            //else if ("查询特约商户".Equals(info.intent))
            //{
            //    await QueryMemberStore(context, result);
            //}
            //else if ("查询基础信息".Equals(info.intent))
            //{
            //    await QueryBasicInfo(context, result);
            //} 
            //else
            //{
            //    if (!String.IsNullOrWhiteSpace(info.brandText))
            //    {
            //        await QueryLocation(context, result);
            //    }
            //    else if (!String.IsNullOrWhiteSpace(info.businessFormatText) || !String.IsNullOrWhiteSpace(info.productText) ||!String.IsNullOrWhiteSpace(info.floorText))
            //    {
            //        await HaveSomething(context, result);
            //    }
            //    else if (!String.IsNullOrWhiteSpace(info.basicInfoText))
            //    {
            //        await QueryBasicInfo(context, result);
            //    }
            //    else
            //    {
            //        await context.PostAsync("正大君目前提供：地点查询，品牌查询等服务。请问您需要什么呢？");
            //    }
            //}
            //context.Wait(MessageReceived);

            //IList<EntityRecommendation> list = result.Entities;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    EntityRecommendation obj = list[i];
            //    await context.PostAsync("Entity:" + obj.Entity + " Type:" + obj.Type + " Role" + obj.Role + " Score" + obj.Score);
            //}
            //context.Wait(MessageReceived);
        }

        [LuisIntent("查询地点")]
        public async Task QueryLocation(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询地点";
            info.VipFlag = false;
            //设置通用信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeBrand1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("有什么")]
        public async Task HaveSomething(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "有什么";
            info.VipFlag = false;
            //设置通用信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeHaveSomething1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("我要吃饭")]
        public async Task Eat(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "我要吃饭";
            info.VipFlag = false;
            //设置通用信息 
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeEat1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("我要购物")]
        public async Task Buy(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "我要购物";
            info.VipFlag = false;
            //设置通用信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeBuy1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询特约商户")]
        public async Task QueryMemberStore(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询特约商户";
            info.VipFlag = true;
            //设置通用信息信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeMemberStore1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, true);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询基本信息")]
        public async Task QueryBasicInfo(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询基本信息";
            info.VipFlag = true;
            //设置通用信息信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeBasicInfo1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, true);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询活动")]
        public async Task QueryActivitys(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询活动";
            info.VipFlag = true;
            //设置通用信息信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.JudgeActivity1(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }
        [LuisIntent("导引")]
        public async Task Guide(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "导引";
            info.VipFlag = true;
            //设置通用信息信息
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.TryGuide(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询停车费")]
        public async Task QueryParkingFee(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询停车费";
            info.VipFlag = false;
            //设置通用信息 
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.QueryParkingFee(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询停车场")]
        public async Task QueryParkingLot(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询停车场";
            info.VipFlag = false;
            //设置通用信息 
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.QueryParkingLot(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("积分")]
        public async Task QueryPoint(IDialogContext context, LuisResult result)
        {
            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "积分";
            info.VipFlag = false;
            //设置通用信息 
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.QueryPoint(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, true);
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询优惠券")]
        public async Task QueryCoupon(IDialogContext context, LuisResult result)
        {
            //string message = $"您好！正大君查询到的热门优惠券有“星巴克买一送十”，“KTV通宵欢唱”，“电影院5场连看”，更多优惠券请点击XXXXX。";
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);

            //获取用户会话
            CRMInfo info = crmservice.GetCustomerSession(TestCustomer);
            info.intent = "查询优惠券";
            info.VipFlag = false;
            //设置通用信息 
            crmservice.SetCommonInfo(info, context, result);
            if (crmservice.QueryCoupon(info))
            {
                await context.PostAsync(info.replyString);
                //清空数据
                crmservice.ClearData(info, false);
            }
            context.Wait(MessageReceived);
        }




    }
}
