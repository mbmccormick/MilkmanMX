using System;
using System.Collections.Generic;
using IronCow.Rest;
using Windows.UI.Core;

namespace IronCow
{
    public class RestRequest : Request
    {
        private struct ResponseGetterAsyncState
        {
            public Func<Response> ResponseGetter;
            public CoreDispatcher CallbackDispatcher;

            public ResponseGetterAsyncState(Func<Response> responseGetter, CoreDispatcher callbackDispatcher)
            {
                ResponseGetter = responseGetter;
                CallbackDispatcher = callbackDispatcher;
            }
        }

        public string Method { get; set; }
        public Action<Response> Callback { get; set; }
        public Dictionary<string, string> Parameters { get; private set; }
        public bool ThrowOnError { get; set; }

        public RestRequest(string method)
            : this(method, null)
        {
        }

        public RestRequest(string method, Action<Response> callback)
        {
            Method = method;
            Callback = callback;
            ThrowOnError = true;
            Parameters = new Dictionary<string, string>();
        }

        public override void Execute(Rtm rtm)
        {
            rtm.GetResponse(Method, Parameters, ThrowOnError, (response) => {
                if (Callback != null)
                    Callback(response);
            });
        }
    }
}
