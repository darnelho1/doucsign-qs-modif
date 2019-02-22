﻿using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using static DocuSign.eSign.Api.EnvelopesApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace qs_csharp.Pages
{
    public class EnvRecipeModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQoAAAABAAUABwCAGYgbDpnWSAgAgFmrKVGZ1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4EgABAAAACwAAAGludGVyYWN0aXZlMAAAg-8aDpnWSDcAYEcIb3TtBU6HTBBHVB7pfg.4-vx-SFYq2aqmyXNhs8kfI0MxHhHcg9fBPkgblLF7pdR2_D_haTjTKY2NmGvNel4NfVOXvnq63YfloL-eFub7W6Fu7yqD6umvKZt6Gma5tB5OktCRoNDtNmY8KsXnnInHyvUC63w_bnI9YWsg8Ll2vvMDhOv6ptdgbFPSrH1MOtnnYWB3GHmPnupKyTw_u99GapTtR6NiCNlETuyDQ7ivfdXzaVfTtbyqS_UMjEUaxAg4NvnKR_F6MebkY8BFzbG9G91LdeaoI_P6TOeMLkwRTLSQc_PM-B40q6-7Z2C-yvRSbuqljUGm94BvbGMP7p05ZFVRFyY3hSz4NegVzAlIQ";
        private const string accountId = "3094776";
        private const string envelopeId = "f23acc3f-0f03-4130-8b3d-716d536ad776";
        //private const int envelopesAgeDays = -10;

        // Additional constants
        private const string basePath = "https://demo.docusign.net/restapi";

        public void OnGet()
        {
            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient.Configuration);
            Recipients recips = envelopesApi.ListRecipients(accountId, envelopeId);


            // Prettyprint the results
            string json = JsonConvert.SerializeObject(recips);
            string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            ViewData["EnvelopeRecipients:"] = jsonFormatted;
            Console.WriteLine(jsonFormatted);

            return;
        }
    }
}
