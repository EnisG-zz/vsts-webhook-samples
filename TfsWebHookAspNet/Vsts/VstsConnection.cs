using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace TfsWebHookAspNet.Vsts
{
    public class VstsConnection : IVstsConnection
    {
        private readonly string URI = ConfigurationManager.AppSettings["tfsCollectionUrl"];
        private readonly string _personalAccessToken = ConfigurationManager.AppSettings["personalAccessToken"];

        public HttpClient Client { get; private set; }

        public string CollectionUrl
        {
            get { return URI; }
        }

        public VstsConnection()
        {
            var _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _personalAccessToken)));

            this.Client = new HttpClient();
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);
        }
    }
}