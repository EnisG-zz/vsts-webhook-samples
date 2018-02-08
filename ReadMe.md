This repository contains an ASP.NET 4.6.1 WebAPI to handle webhooks registered in TFS/VSTS. 

- ClosedDevelopmentTaskHandler: When a Task is closed, this handler checks whether all sibling tasks (under a User Story) are in "Closed" state and if so it changes parent User Story's state to "ReadyToTest" 
