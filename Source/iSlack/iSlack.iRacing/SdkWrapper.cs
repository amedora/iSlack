using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

using iRacingSdkWrapper;

namespace iSlack.Extensions
{
    // Why do we need a Publish() and RefCount()?
    // See http://qiita.com/toRisouP/items/c955e36610134c05c860
    public static class SdkWrapperExtension
    {
        public static IObservable<SdkWrapper.SessionInfoUpdatedEventArgs> SessionInfoUpdatedAsObservable(this SdkWrapper wrapper)
        {
            return Observable.FromEventPattern<SdkWrapper.SessionInfoUpdatedEventArgs>(wrapper, "SessionInfoUpdated")
                .Select(x => x.EventArgs)
                .Publish()
                .RefCount();
        }

        public static IObservable<SdkWrapper.TelemetryUpdatedEventArgs> TelemetryUpdatedAsObservable(this SdkWrapper wrapper)
        {
            return Observable.FromEventPattern<SdkWrapper.TelemetryUpdatedEventArgs>(wrapper, "TelemetryUpdated")
                .Select(x => x.EventArgs)
                .Publish()
                .RefCount();
        }
    }
}
