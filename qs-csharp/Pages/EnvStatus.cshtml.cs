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
    public class EnvStatusModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQkAAAABAAUABwCAnIr2YpjWSAgAgNytBKaY1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4MACA8wPcYZjWSDcAYEcIb3TtBU6HTBBHVB7pfg.odP1vQkwKGD7uV7fmCKru3Ij0blgIJBCkB-4sSx9vyRrKDpxdTrdJ-t6Y0CEM5HcqO_axno-VT9YyviSPF6Bpo_XXPDU2lyNfAGekQLcHFzAL3QlTnH2R8t6OxdbRyt7tT3xwjot79BsNv11dDDd7dJ1V1rEv-WF09514u7vhAgVQGh-Kd1QwVk6c7yqurpELR2EWYNcHzHP0LYYjawCgvEsvII_NAc0tOhaG0c3U1uyIvWO2i_xz-04LzXi3-lmfsOgO0naxqhqYz5f9q1FQDlZ8zgM8tcyXWW8ZJ88ZgsQBz-tHrW-bElveGCXS9KguVngEROStZm5GUsM1SLB7g";
        private const string accountId = "3094776";
        private const int envelopesAgeDays = -10;

        // Additional constants
        private const string basePath = "https://demo.docusign.net/restapi";

        public void OnGet()
        {
            // List the user's envelopes created in the last 10 days
            // 1. Create request options
            // 2. Use the SDK to list the envelopes

            // 1. Create request options
            ListStatusChangesOptions options = new ListStatusChangesOptions();
            DateTime date = DateTime.Now.AddDays(envelopesAgeDays);
            options.fromDate = date.ToString("yyyy/MM/dd");

            // 2. Use the SDK to list the envelopes
            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient.Configuration);
            EnvelopesInformation results = envelopesApi.ListStatusChanges(accountId, options);

            // Prettyprint the results
            string json = JsonConvert.SerializeObject(results);
            string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            ViewData["results"] = jsonFormatted;

            return;
        }
    }
}
