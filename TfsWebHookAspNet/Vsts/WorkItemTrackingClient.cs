using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using TfsWebHookAspNet.Vsts.Model;

namespace TfsWebHookAspNet.Vsts
{
    public class WorkItemTrackingClient
    {
        private IVstsConnection connection; 
        public WorkItemTrackingClient(IVstsConnection connection)
        {
            this.connection = connection; 
        }

        public string GetParentWorkItem(string childId)
        {
            var method = new HttpMethod("GET");
            var httpRequestMessage = new HttpRequestMessage(method, connection.CollectionUrl + "/_apis/wit/workItems/" + childId + "?api-version=2.0&$expand=relations");
            var httpResponseMessage = connection.Client.SendAsync(httpRequestMessage).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                WorkItemRelationsResult workItemQueryResult = httpResponseMessage.Content.ReadAsAsync<WorkItemRelationsResult>().Result;
                var wiUrl = workItemQueryResult.relations[0].url;
                var tmp = wiUrl.Split('/');
                return tmp[tmp.Count() - 1];
            }

            throw new Exception(""); // todo: handle this case
        }

        public WorkItemLink[] GetWorkItemsWithQuery(string wiqlRequest)
        {
            //serialize the wiql object into a json string   
            var postValue = new StringContent(wiqlRequest, Encoding.UTF8, "application/json"); //mediaType needs to be application/json for a post call

            var method = new HttpMethod("POST");
            var httpRequestMessage = new HttpRequestMessage(method, this.connection.CollectionUrl + "/_apis/wit/wiql?api-version=2.2") { Content = postValue };
            var httpResponseMessage = this.connection.Client.SendAsync(httpRequestMessage).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                WorkItemQueryResult workItemQueryResult = httpResponseMessage.Content.ReadAsAsync<WorkItemQueryResult>().Result;
                return workItemQueryResult.workItemRelations;
            }

            throw new Exception(""); // todo: handle this case
        }

        public void PatchWorkItem(Object[] patchDocument, string workItemId)
        {
            //serialize the fields array into a json string
            var patchValue = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, this.connection.CollectionUrl + "/_apis/wit/workitems/" + workItemId + "?api-version=2.2") { Content = patchValue };
            var response = this.connection.Client.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
            {
               throw new Exception(""); // todo: handle this case
            }
        }
    }
}