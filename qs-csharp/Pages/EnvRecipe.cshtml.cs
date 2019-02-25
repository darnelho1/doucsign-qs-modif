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
    public class EnvRecipeModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQoAAAABAAUABwAAK2SWNJnWSAgAAGuHpHeZ1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4EgABAAAACwAAAGludGVyYWN0aXZlMACAZ5qUNJnWSDcAYEcIb3TtBU6HTBBHVB7pfg.5kSOxbBdy5df2F3n5qlVr2HNerIW8EB69Op81PlPNgK8v-AXrQypy1EZrIBc8NGjmusfRCdR0iPDBt214Ob5I2-sv9tsIMOwlSQ21djTDU3dTAI_9TwPICnQFZv952QOOG_fNV64RVds1O_R4al1UPX5xjGAOiTjB-PZ17lcj2OmnbE_Z7v0VGbRia9SygvlVEKdhGEGtTj-2NZPBu1H3mA7oHOw6dsnlvlGI5XzpQgnjuzBZqN85vy0V6lEw5Aa6evf63EOwTvd-TrBGdUFBfMXI1adML41lnngWlmhFJAlRkvrsiLAdFYBLyS-2ktWMdttZnNMbFLGR44lXHjxOw";
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
            //Console.WriteLine(jsonFormatted);

            return;
        }
    }
}
