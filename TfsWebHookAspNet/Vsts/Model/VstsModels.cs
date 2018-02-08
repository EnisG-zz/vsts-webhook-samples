using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TfsWebHookAspNet.Vsts.Model
{
    public class Workitem
    {
        public int id { get; set; }
        public string url { get; set; }
    }
    public class WorkItemLink
    {
        public string rel { get; set; }
        public Workitem source { get; set; }
        public Workitem target { get; set; }
    }
    public class Column
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
    public class WorkItemQueryResult
    {
        public string queryType { get; set; }
        public string queryResultType { get; set; }
        public DateTime asOf { get; set; }
        public Column[] columns { get; set; }
        public Workitem[] workItems { get; set; }
        public WorkItemLink[] workItemRelations { get; set; }
    }
    public class WorkItemRelationsResult
    {
        public WorkItemRelation[] relations { get; set; }
    }
    public class WorkItemRelation
    {
        public string rel { get; set; }
        public string url { get; set; }
    }
}