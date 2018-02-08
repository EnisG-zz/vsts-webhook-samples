using Microsoft.AspNet.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.WebHooks.Payloads;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using TfsWebHookAspNet.Vsts.Model; 

namespace TfsWebHookAspNet.Vsts
{
    public class VstsWebHookHandler : VstsWebHookHandlerBase
    {
        public override Task ExecuteAsync(WebHookHandlerContext context, WorkItemUpdatedPayload payload)
        {
            IWorkItemStateChangeHandler handler = null; 

            var newState = payload.Resource.Fields.SystemState.NewValue; 
            if (newState == "Closed")
            {
                var taskId = payload.Resource.WorkItemId;
                handler = new ClosedDevelopmentTaskHandler(taskId);
                handler.Execute(); 
            }
            return base.ExecuteAsync(context, payload);
        }
    }
}