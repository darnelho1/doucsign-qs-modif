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
    public class dlEnvelopeModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQkAAAABAAUABwCAcnfylJjWSAgAgLKaANiY1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4MACA8wPcYZjWSDcAYEcIb3TtBU6HTBBHVB7pfg.aJn81whnuKK2SzA7036Mz8TyH0q_bIWpGdsW2gzkWMmvJDO1Vt-bjarmjVtJQ5j1p-K249I-UVOD53H9nwxJzKSbedO9cDOMSL53vKQI79FwtwN_ctekIAcZriIwGQyuoHPp5U2wyO8uCxV5x_SAUKfoiQ0LI6X5ul3M43K-FaBSu2lMNIfPOAaO42AvzlZNCkq2J2ZiLczRJa-3R997KS7nJbr7M7DPpBPgZd-I2F-dWRxs6mjlWMbRX4uRd4D4Pm8o6fhdqz70MP1XJUH18SXOrGYtfI4p1pWE68x88VxuFGvNeAI0NzfUtsIYsUIAIvXp-MNCEBWVZiZtY3MFjQ";
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
