using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using static DocuSign.eSign.Api.EnvelopesApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace qs_csharp.Pages
{
    public class embedEnvSnederModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQoAAAABAAUABwCAGYgbDpnWSAgAgFmrKVGZ1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4EgABAAAACwAAAGludGVyYWN0aXZlMAAAg-8aDpnWSDcAYEcIb3TtBU6HTBBHVB7pfg.4-vx-SFYq2aqmyXNhs8kfI0MxHhHcg9fBPkgblLF7pdR2_D_haTjTKY2NmGvNel4NfVOXvnq63YfloL-eFub7W6Fu7yqD6umvKZt6Gma5tB5OktCRoNDtNmY8KsXnnInHyvUC63w_bnI9YWsg8Ll2vvMDhOv6ptdgbFPSrH1MOtnnYWB3GHmPnupKyTw_u99GapTtR6NiCNlETuyDQ7ivfdXzaVfTtbyqS_UMjEUaxAg4NvnKR_F6MebkY8BFzbG9G91LdeaoI_P6TOeMLkwRTLSQc_PM-B40q6-7Z2C-yvRSbuqljUGm94BvbGMP7p05ZFVRFyY3hSz4NegVzAlIQ";
        private const string accountId = "3094776";
        private const string envelopeId = "2c0da344-e63b-4ba7-b39e-a7ac3322e761";

        // Additional constants
        private const string basePath = "https://demo.docusign.net/restapi";

        public void OnGet()
        {
            ReturnUrlRequest options = new ReturnUrlRequest();
            options.ReturnUrl = "https://www.docusign.com";

            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);

            EnvelopesApi envelopesApi = new EnvelopesApi();

            // generate the embedded sending URL
            ViewUrl senderView = envelopesApi.CreateSenderView(accountId, envelopeId, options);

            // print the JSON response
            //Console.WriteLine("ViewUrl:\n{0}", JsonConvert.SerializeObject(senderView));

            // Start the embedded sending session
            System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", senderView.Url);


            // Prettyprint the results
            //string json = JsonConvert.SerializeObject(results);
            //string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            //ViewData["embedEnvSneder"] = JsonConvert.SerializeObject(senderView);

        }
    }
}
