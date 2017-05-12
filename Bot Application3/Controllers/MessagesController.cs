using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Dialog;

namespace Bot_Application3
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        //public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        //{
        //    if (activity.Type == ActivityTypes.Message)
        //    {
        //        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
        //        // calculate something for us to return
        //        int length = (activity.Text ?? string.Empty).Length;

        //        // return our reply to the user
        //        Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
        //        await connector.Conversations.ReplyToActivityAsync(reply);
        //    }
        //    else
        //    {
        //        HandleSystemMessage(activity);
        //    }
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    return response;
        //}

        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {

            if (activity.Type == ActivityTypes.Message)
            {
                //await Conversation.SendAsync(activity, () => new WeatherDialog());
                try
                {
                    //connectorclient connector = new connectorclient(new uri(activity.serviceurl));
                    //activity reply = activity.createreply("channelid: " + activity.channelid);
                    //await connector.conversations.replytoactivityasync(reply);
                    if ("directline".Equals(activity.ChannelId))
                    {
                        await Conversation.SendAsync(activity, () => new CRMDialog4WeChat(activity.From.Id));
                    } else
                    {
                        await Conversation.SendAsync(activity, () => new CRMDialog(activity.From.Id));
                    }
                }
                catch (Exception e)
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    // calculate something for us to return
                    int length = (activity.Text ?? string.Empty).Length;

                    // return our reply to the user
                    Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters" + " error :" + e.ToString());
                    //await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else
            {
                //add code to handle errors, or non-messaging activities
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }


        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}