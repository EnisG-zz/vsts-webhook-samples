namespace TfsWebHookAspNet.Vsts
{
    public interface IWorkItemStateChangeHandler
    {
        int WorkItemId { get; }
        void Execute();
    }
}