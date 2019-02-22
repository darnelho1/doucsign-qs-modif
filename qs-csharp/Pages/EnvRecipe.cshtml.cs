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
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQkAAAABAAUABwCA-0DJAZnWSAgAgDtk10SZ1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4MACAKy62AZnWSDcAYEcIb3TtBU6HTBBHVB7pfg.XzvS6NvNsRnLuleMebjoiFmtzO2bP9V-G9DkccFqW6f982zeVOF_9VtFt07Wi4C7N5SchYqDo5kdQEes-T9jmDLSo8ZyNrfHKg3hSRPwiJG1chaeCYTt_VChFHfDrxkuwvdiebCfAvEimbI3UTzuH9iD8FCpV3Wr1fiE-u_TVLIoTzjC31IGu6KJWdUMbSgmLjDy5Q7Qkjq3Nbd-O4_AlhIoE0xd7JiSxxP-8uZ22KJ0QNr1PoovGcm3uWu3JUDXNMj0qFGrzi6HPs4uHWLgVnY3Vp_EfQR1s8LG_sG0Wz1jwBL8vxZs9-8HlP4N_AIXygCGYXp58DR0uIm8EemA4g";
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
