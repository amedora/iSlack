using System;
using System.Reactive.Linq;
using Xunit;

namespace iSlack.Test
{
    public class iSlackClientTest
    {
        [Fact(Skip = "Network access required.")]
        public void Post()
        {
            var pc = new
            {
                URL      = "https://hooks.slack.com/services/...",
                UserName = "...",
                Channel  = "#...",
            };

            var ps = new iSlack.RaceSession();
            var client = new iSlack.Client(pc.URL, pc.UserName, pc.Channel);
            Assert.True(client.Post(ps));
        }
    }
}
