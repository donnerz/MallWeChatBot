using Bot_Application3.Model;
using Common;

using Model.Domian;
using System;
using System.Collections.Generic;

using System.Text;


namespace Service
{
    [Serializable]
    public class ActivityService
    {
        SearchService searchService = new SearchService();

        public void JudgeActivity4(CRMInfo info, string baseUrl)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder activitysb = new StringBuilder();
            StringBuilder remarksb = new StringBuilder();
            String activityStr = "";
            if (!String.IsNullOrWhiteSpace(info.basicInfoText) || !String.IsNullOrWhiteSpace(info.floorText))
            {
                //该基础信息/楼层下的品牌
                List<t_activity> list = searchService.GetActivityListByBasciInfoAndFloor(info.basicInfoText, info.floorText, info.BuiltinDatetimeDateResolution);
                if (list.Count > 0)
                {
                    foreach (t_activity activity in list)
                    {
                        activitysb.Append(BaseInfo.wordDic[activity.activity].name).Append("、");
                        remarksb.Append("“"). Append(BaseInfo.wordDic[activity.activity].name).Append("：").Append(activity.remark).Append("”宣传语。");
                    }
                    if (activitysb.Length > 0)
                    {
                        activityStr = activitysb.ToString().Substring(0, activitysb.ToString().Length - 1);
                    }
                    //时间
                    sb.Append(info.BuiltinDatetimeDate).Append("(").Append(info.BuiltinDatetimeDateResolution).Append(")");

                    if (!String.IsNullOrWhiteSpace(info.floorText))
                        sb.Append(searchService.getWordNameBySynonym(info.floorText));
                    if (!String.IsNullOrWhiteSpace(info.basicInfoText))
                        sb.Append(searchService.getWordNameBySynonym(info.basicInfoText));
                    sb.Append("正大君");
                    sb.Append("找到的活动有");
                    sb.Append(activityStr);
                    sb.Append("。");
                    sb.Append(remarksb.ToString());
                    info.replyString = sb.ToString();
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(info.basicInfoText))
                    {
                        list = searchService.GetActivityListByBasciInfoAndFloor(info.basicInfoText, null, info.BuiltinDatetimeDateResolution);

                        //info.replyString = "count = " + list.Count + " code = " + list[0].code;
                        //return;
                        if (list.Count > 0)
                        {
                            foreach (t_activity activity in list)
                            {
                                activitysb.Append(BaseInfo.wordDic[activity.activity].name).Append("、");
                                remarksb.Append("“").Append(BaseInfo.wordDic[activity.activity].name).Append("：").Append(activity.remark).Append("”宣传语。");
                            }
                            if (activitysb.Length > 0)
                            {
                                activityStr = activitysb.ToString().Substring(0, activitysb.ToString().Length - 1);
                            }
                            //时间
                            sb.Append(info.BuiltinDatetimeDate).Append("(").Append(info.BuiltinDatetimeDateResolution).Append(")");

                            if (!String.IsNullOrWhiteSpace(info.floorText))
                                sb.Append(searchService.getWordNameBySynonym(info.floorText));
                            if (!String.IsNullOrWhiteSpace(info.basicInfoText))
                                sb.Append(searchService.getWordNameBySynonym(info.basicInfoText));
                            sb.Append("没有找到活动哦。");
                            sb.Append(searchService.getWordNameBySynonym(info.basicInfoText));
                            sb.Append("正大君");
                            sb.Append("找到的活动有");
                            sb.Append(activityStr);
                            sb.Append("。");
                            sb.Append(remarksb.ToString());
                            info.replyString = sb.ToString();
                            return;
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(info.floorText))
                    {
                        list = searchService.GetActivityListByBasciInfoAndFloor(null, info.floorText, info.BuiltinDatetimeDateResolution);
                        if (list.Count > 0)
                        {
                            foreach (t_activity activity in list)
                            {
                                activitysb.Append(BaseInfo.wordDic[activity.activity].name).Append("、");
                                remarksb.Append("“").Append(BaseInfo.wordDic[activity.activity].name).Append("：").Append(activity.remark).Append("”宣传语。");
                            }
                            if (activitysb.Length > 0)
                            {
                                activityStr = activitysb.ToString().Substring(0, activitysb.ToString().Length - 1);
                            }
                            //时间
                            sb.Append(info.BuiltinDatetimeDate).Append("(").Append(info.BuiltinDatetimeDateResolution).Append(")");

                            if (!String.IsNullOrWhiteSpace(info.floorText))
                                sb.Append(searchService.getWordNameBySynonym(info.floorText));
                            if (!String.IsNullOrWhiteSpace(info.basicInfoText))
                                sb.Append(searchService.getWordNameBySynonym(info.basicInfoText));
                            sb.Append("没有找到活动哦。");
                            sb.Append(searchService.getWordNameBySynonym(info.floorText));
                            sb.Append("正大君");
                            sb.Append("找到的活动有");
                            sb.Append(activityStr);
                            sb.Append("。");
                            sb.Append(remarksb.ToString());
                            info.replyString = sb.ToString();
                            return;
                        }
                    }

                    list = searchService.GetActivityListByBasciInfoAndFloor(null, null, info.BuiltinDatetimeDateResolution);
                    foreach (t_activity activity in list)
                    {
                        activitysb.Append(BaseInfo.wordDic[activity.activity].name).Append("、");
                        remarksb.Append("“").Append(BaseInfo.wordDic[activity.activity].name).Append("：").Append(activity.remark).Append("”宣传语。");
                    }
                    if (activitysb.Length > 0)
                    {
                        activityStr = activitysb.ToString().Substring(0, activitysb.ToString().Length - 1);
                    }

                    sb.Append("哎呀，");
                    //时间
                    sb.Append(info.BuiltinDatetimeDate).Append("(").Append(info.BuiltinDatetimeDateResolution).Append(")");

                    if (!String.IsNullOrWhiteSpace(info.floorText))
                        sb.Append(searchService.getWordNameBySynonym(info.floorText));
                    if (!String.IsNullOrWhiteSpace(info.basicInfoText))
                        sb.Append(searchService.getWordNameBySynonym(info.basicInfoText));
                    sb.Append("正大君");
                    sb.Append("没有找到活动哦。");
                    sb.Append("其他活动有");
                    sb.Append(activityStr);
                    sb.Append("。");
                    sb.Append(remarksb.ToString());
                    info.replyString = sb.ToString();
                    
                }
            }
            else
            {
                List<t_activity> list = searchService.GetActivityListByBasciInfoAndFloor(null, null, info.BuiltinDatetimeDateResolution);
                foreach (t_activity activity in list)
                {
                    activitysb.Append(BaseInfo.wordDic[activity.activity].name).Append("、");
                }
                if (activitysb.Length > 0)
                {
                    activityStr = activitysb.ToString().Substring(0, activitysb.ToString().Length - 1);
                }
                //时间
                sb.Append(info.BuiltinDatetimeDate).Append("(").Append(info.BuiltinDatetimeDateResolution).Append(")");

                sb.Append("正大广场");
                sb.Append("活动有好多呢。比如");
                sb.Append(activityStr);
                sb.Append("。询问活动名称、楼层、地点、时间，或者");
                sb.Append("问“黄金大道活动”，“9楼活动”等等，");
                sb.Append("可以获取更详细的信息哦!");


                sb.Append("\u0001新春市集集好货，欢迎到黄金大道选购");
                sb.Append("\u0001新春市集集好货，欢迎到黄金大道选购");
                sb.Append("\u0001").Append(baseUrl).Append("/images/huodong.jpg");
                sb.Append("\u0001http://www.baidu.com");

                sb.Append("\u0001LPL春季赛总冠军争夺战，齐聚正大厅");
                sb.Append("\u0001LPL春季赛总冠军争夺战，齐聚正大厅");
                sb.Append("\u0001").Append(baseUrl).Append("/images/huodong.jpg");
                sb.Append("\u0001http://www.baidu.com");

                sb.Append("\u0001已报名活动");
                sb.Append("\u0001您已报名参加此活动，点击查看活动详情");
                sb.Append("\u0001").Append(baseUrl).Append("/images/huodong.jpg");
                sb.Append("\u0001http://www.baidu.com");

                sb.Append("\u0001更多活动");
                sb.Append("\u0001更多活动请点击查看。");
                sb.Append("\u0001").Append(baseUrl).Append("/images/huodong.jpg");
                sb.Append("\u0001http://www.baidu.com");


                info.replyString = sb.ToString();
            }
        }

    }
}