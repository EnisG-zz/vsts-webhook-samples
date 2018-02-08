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
    public class ClosedDevelopmentTaskHandler : IWorkItemStateChangeHandler
    {
        public int WorkItemId { get; private set; }
        public WorkItemTrackingClient wiClient { get; set; }
        public ClosedDevelopmentTaskHandler(int workItemId)
        {
            this.WorkItemId = workItemId;
            this.wiClient = new WorkItemTrackingClient(new VstsConnection());
        }

        public void Execute()
        {
            var userStoryId = this.wiClient.GetParentWorkItem(this.WorkItemId.ToString());
            if (areAllDevelopmentTasksCompleted(userStoryId))
            {
                updateUserStoryState(userStoryId);
            }
        }
        private bool areAllDevelopmentTasksCompleted(string userStoryId)
        {
            var wiql = new { query = "Select [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] From WorkItemLinks WHERE (Source.[System.Id] = " + userStoryId + " and  Source.[System.WorkItemType] = 'User Story'  and Source.[System.State] <> 'Closed') and ([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') and (Target.[System.WorkItemType] = 'Task' and Target.[System.State] <> 'Closed') mode(Recursive)" };
            var wi = this.wiClient.GetWorkItemsWithQuery(JsonConvert.SerializeObject(wiql));
            return wi.Count() == 1;
        }
        private void updateUserStoryState(string userStoryId)
        {
            Object[] patchDocument = new Object[1];
            patchDocument[0] = new { op = "add", path = "/fields/System.State", value = "ReadyToTest" };

            this.wiClient.PatchWorkItem(patchDocument, userStoryId.ToString());
        }

    }
}