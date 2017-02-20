using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSdkWrapper;
using iSlack.Extensions;
using System.Reactive.Linq;
using Xunit;
using System.IO;

namespace iSlack.Test
{
    public static class SdkWrapperExtension
    {
        public static void RaiseSessionInfoUpdated(this SdkWrapper wrapper, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            if (!e.SessionInfo.IsValidYaml) throw new ArgumentException("Invalid " + nameof(SdkWrapper.SessionInfoUpdatedEventArgs));
            wrapper.AsDynamic().OnSessionInfoUpdated(e);
        }

        public static void RaiseTelemetryUpdated(this SdkWrapper wrapper, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            wrapper.AsDynamic().OnTelemetryUpdated(e);
        }
    }

    public class SdkWrapperTest
    {
        [Fact]
        public void SdkWrapper_Runs_Properly()
        {
            var wrapper = new SdkWrapper();
            wrapper.SessionInfoUpdatedAsObservable().Subscribe(e =>
            {
                var trackName = e.SessionInfo["WeekendInfo"]["TrackName"].Value;
                trackName.Is("southboston");
            });

            string sessioninfo = @"WeekendInfo:
 TrackName: southboston
 TrackID: 14
 TrackLength: 0.59 km
 SeriesID: 0
 SeasonID: 0
 SessionID: 0
 SubSessionID: 0";
            wrapper.RaiseSessionInfoUpdated(new SdkWrapper.SessionInfoUpdatedEventArgs(sessioninfo, 0));
        }

        [Fact]
        public void YamlDumper()
        {
            var wrapper = new SdkWrapper();
            wrapper.SessionInfoUpdatedAsObservable().Subscribe(e =>
            {
                var trackName = e.SessionInfo["WeekendInfo"]["TrackName"].Value;
                trackName.Is("southboston");

                File.WriteAllText(MakeYamlFileName(), e.SessionInfo.Yaml);
            });

            string sessioninfo = @"WeekendInfo:
 TrackName: southboston
 TrackID: 14
 TrackLength: 0.59 km
 SeriesID: 0
 SeasonID: 0
 SessionID: 0
 SubSessionID: 0";
            wrapper.RaiseSessionInfoUpdated(new SdkWrapper.SessionInfoUpdatedEventArgs(sessioninfo, 0));
        }

        private string MakeYamlFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
        }
    }
}
