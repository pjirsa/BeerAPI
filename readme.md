# BeerAPI

## Description
This is a sample project to have fun with dotnet core and ASP.net core 5.

## Getting started
1. Edit the appsettings.json in the BeerAPI project.
```
"AzureAd": {
    "AadInstance": "https://login.microsoftonline.com/{0}",
    "Tenant": "pjirsagmail.onmicrosoft.com",
    "Audience": "https://mnbeerapi.azurewebsites.net"
  },
```
Change the "Tenant" to be your AzureAD tenant.

1. Edit the appsettings.json in the BeerMVC project.
```
"AzureAd": {
    "ClientId": "7d726bd5-bd2c-4572-89c2-240c76f6e4b1",
    "Tenant": "pjirsagmail.onmicrosoft.com",
    "BeerAPIResourceId": "https://mnbeerapi.azurewebsites.net",
    "BeerAPIBaseUri": "https://mnbeerapi.azurewebsites.net",
    "AadInstance": "https://login.microsoftonline.com/{0}", // This is the public instance of Azure AD
    "PostLogoutRedirectUri": "https://localhost:44322/",
    "GraphResourceId": "https://graph.windows.net"
  }
```
- Change "Tenant" to be your AzureAD tenant.
- Update the "ClientID" with the client id for you Azure AD application.
- Add a "ClientSecret" value with the client secret of your Azure AD application.
- Change URLs of "BeerAPIResourceId", "BeerAPIBaseUri", and "PostLogoutRedirectUri" to reflect your Azure websites.
