using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocuSign.eSign.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Owin;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace qs_csharp
{
    public class Program
    {
        // Point to DocuSign Demo (sandbox) environment for requests
        public const string RestApiUrl = "https://demo.docusign.net/restapi";

        // These items are all registered at the DocuSign Admin console and are required 
        // to perform the OAuth flow.
        public const string client_id = "47f1113d-6380-4886-b0a6-cba02f960879";
        public const string client_secret = "22f9f658-b1dd-41c3-b38d-d41b4a2a52d8";
        public const string redirect_uri = "http://localhost:5050";

        // This is an application-speicifc param that may be passed around during the OAuth
        // flow. It allows the app to track its flow, in addition to more security.
        public const string stateOptional = "testState";

        // This will be returned to the test via the callback url after the
        // user authenticates via the browser.
        public static string AccessCode { get; internal set; }

        // This will be filled in with the access_token retrieved from the token endpoint using the code above.
        // This is the Bearer token that will be used to make API calls.
        public static string AccessToken { get; set; }
        public static string StateValue { get; internal set; }

        public static string AccountId { get; set; }
        public static string BaseUri { get; set; }

        // This event handle is used to block the self-hosted Web service in the test
        // until the OAuth login is completed.
        public static ManualResetEvent WaitForCallbackEvent = null;

        // main entry method
        static void Main(String[] args)
        {
            /////////////////////////////////////////////////////////////////
            // Run Code Samples        
            /////////////////////////////////////////////////////////////////
            Program samples = new Program();

            // first we use the OAuth authorization code grant to get an API access_token
            samples.OAuthAuthorizationCodeFlowTest();
            CreateWebHostBuilder(args).Build().Run();
            
        }

        public void OAuthAuthorizationCodeFlowTest()
        {

            // Make an API call with the token
            ApiClient apiClient = new ApiClient(RestApiUrl);
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;

            // Initiate the browser session to the Authentication server
            // so the user can login.
            string accountServerAuthUrl = apiClient.GetAuthorizationUri(client_id, redirect_uri, true, stateOptional);
            Process.Start("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe",accountServerAuthUrl);
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        

    }
}
