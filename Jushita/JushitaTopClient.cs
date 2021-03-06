﻿using System.Collections.Generic;
using Top.Api.Request;
using System.Net;

namespace Top.Api.Jushita
{
    /// <summary>
    /// 聚石塔专用TOP客户端。
    /// </summary>
    public class JushitaTopClient
    {
        private const string SYNC_CENTER_URL = "http://synccenter.taobao.com/api";

        private DefaultTopClient topClient;

        public JushitaTopClient(string serverUrl, string appKey, string appSecret)
        {
            this.topClient = new DefaultTopClient(serverUrl, appKey, appSecret);
            this.topClient.SetDisableParser(true);
        }

        public JushitaTopClient(string appKey, string appSecret)
            : this(SYNC_CENTER_URL, appKey, appSecret)
        {
        }

        public JushitaTopClient(string serverUrl, string appKey, string appSecret, int timeout)
            : this(serverUrl, appKey, appSecret)
        {
            this.topClient.SetTimeout(timeout);
        }

        public JushitaTopClient(string serverUrl, string appKey, string appSecret, string format, int timeout)
            : this(serverUrl, appKey, appSecret)
        {
            this.topClient.SetFormat(format);
            this.topClient.SetTimeout(timeout);
        }


        public string Execute(string apiName, IDictionary<string, string> parameters, string session)
        {
            JushitaRequest request = new JushitaRequest();
            request.ApiName = apiName;
            request.Parameters = parameters;
            try
            {
                JushitaResponse response = topClient.Execute(request, session);
                return response.Body;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
                        return streamReader.ReadToEnd();
                }
            }
        }
       
    }

    

    public class JushitaRequest : ITopRequest<JushitaResponse>
    {
        public string ApiName { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

        public string GetApiName()
        {
            return this.ApiName;
        }

        public IDictionary<string, string> GetParameters()
        {
            return this.Parameters;
        }

        public void Validate()
        {
        }
    }

    public class JushitaResponse : TopResponse
    {
    }
}
