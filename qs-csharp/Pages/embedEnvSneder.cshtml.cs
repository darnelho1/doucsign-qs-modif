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
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQoAAAABAAUABwAAK2SWNJnWSAgAAGuHpHeZ1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4EgABAAAACwAAAGludGVyYWN0aXZlMACAZ5qUNJnWSDcAYEcIb3TtBU6HTBBHVB7pfg.5kSOxbBdy5df2F3n5qlVr2HNerIW8EB69Op81PlPNgK8v-AXrQypy1EZrIBc8NGjmusfRCdR0iPDBt214Ob5I2-sv9tsIMOwlSQ21djTDU3dTAI_9TwPICnQFZv952QOOG_fNV64RVds1O_R4al1UPX5xjGAOiTjB-PZ17lcj2OmnbE_Z7v0VGbRia9SygvlVEKdhGEGtTj-2NZPBu1H3mA7oHOw6dsnlvlGI5XzpQgnjuzBZqN85vy0V6lEw5Aa6evf63EOwTvd-TrBGdUFBfMXI1adML41lnngWlmhFJAlRkvrsiLAdFYBLyS-2ktWMdttZnNMbFLGR44lXHjxOw";
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

            // Start the embedded sending session
            System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", senderView.Url);

        }
    }
}
