This repository contains an ASP.NET 4.6.1 WebAPI to handle webhooks registered in TFS/VSTS. 

- ClosedDevelopmentTaskHandler: When a Task is closed, this handler checks whether all sibling tasks (under a User Story) are in "Closed" state and if so it changes parent User Story's state to "ReadyToTest" 

You can follow this documentation to create a webhook for Work Item State Change event. 
https://docs.microsoft.com/en-us/vsts/service-hooks/services/webhooks?view=vsts

To test your webhook, you can start this application and use tools like ngrok.com or ultrahook.com to redirect vsts webhooks to the application running in localhost. 

If you use tools like PostMan to directly call and debug your web api, you should send a request to following URL: 
http://localhost:20168/api/webhooks/incoming/vsts?code=83699ec7c1d794c0c780e49a5c72972590571fd8

Notice that you are sending a "code" in query string, you can use any random code but it should be also present in appSettings @web.config with the following key: MS_WebHookReceiverSecret_VSTS
