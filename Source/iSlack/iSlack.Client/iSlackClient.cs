using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slack.Webhooks;

namespace iSlack
{
    public class Client
    {
        public SlackClient SlackClient { get; private set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        public string Channel { get; set; }

        public Client(string url, string userName, string channel)
        {
            URL = url;
            UserName = userName;
            Channel = channel;

        }
        public bool Post(ISessionInfo sessionInfo)
        {
            var message = new SlackMessage
            {
                Channel = Channel,
                Username = UserName,
            };
            var attachment = new SlackAttachment
            {
                Fallback = sessionInfo.Title,
                Text = sessionInfo.Title,
                Fields = sessionInfo.Fields,
            };
            message.Attachments = new List<SlackAttachment> { attachment };

            var client = new SlackClient(URL);
            return client.Post(message);
        }
    }

    public interface ISessionInfo
    {
        List<SlackField> Fields { get; set; }
        string Title { get; set; }
    }

    public class RaceSession : ISessionInfo
    {
        public List<SlackField> Fields
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
