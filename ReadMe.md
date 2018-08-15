This repository contains an ASP.NET 4.6.1 WebAPI to handle webhooks registered in TFS/VSTS. 

- ClosedDevelopmentTaskHandler: When a Task is closed, this handler checks whether all sibling tasks (under a User Story) are in "Closed" state and if so it changes parent User Story's state to "ReadyToTest" 

You can follow this documentation to create a webhook for Work Item State Change event. 
https://docs.microsoft.com/en-us/vsts/service-hooks/services/webhooks?view=vsts

To test your webhook, you can start this application and use tools like ngrok.com or ultrahook.com to redirect vsts webhooks to the application running in localhost. 
