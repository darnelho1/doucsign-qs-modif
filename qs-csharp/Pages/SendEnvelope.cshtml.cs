using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;

namespace qs_csharp.Pages
{
    public class SendEnvelopeModel : PageModel
    {
        // Constants need to be set:
        private const string accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQkAAAABAAUABwCAnIr2YpjWSAgAgNytBKaY1kgCAJ8giZesq0hAr93A-qwGx1gVAAEAAAAYAAEAAAAFAAAADQAkAAAAZjBmMjdmMGUtODU3ZC00YTcxLWE0ZGEtMzJjZWNhZTNhOTc4MACA8wPcYZjWSDcAYEcIb3TtBU6HTBBHVB7pfg.odP1vQkwKGD7uV7fmCKru3Ij0blgIJBCkB-4sSx9vyRrKDpxdTrdJ-t6Y0CEM5HcqO_axno-VT9YyviSPF6Bpo_XXPDU2lyNfAGekQLcHFzAL3QlTnH2R8t6OxdbRyt7tT3xwjot79BsNv11dDDd7dJ1V1rEv-WF09514u7vhAgVQGh-Kd1QwVk6c7yqurpELR2EWYNcHzHP0LYYjawCgvEsvII_NAc0tOhaG0c3U1uyIvWO2i_xz-04LzXi3-lmfsOgO0naxqhqYz5f9q1FQDlZ8zgM8tcyXWW8ZJ88ZgsQBz-tHrW-bElveGCXS9KguVngEROStZm5GUsM1SLB7g";
        private const string accountId = "3094776";
        private const string signerName = "Darnell Holder";
        private const string signerEmail = "darnelho1@gmail.com";
        private const string docName = "World_Wide_Corp_lorem.pdf";

        // Additional constants
        private const string basePath = "https://demo.docusign.net/restapi";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Send envelope. Signer's request to sign is sent by email.
            // 1. Create envelope request obj
            // 2. Use the SDK to create and send the envelope

            // 1. Create envelope request object
            //    Start with the different components of the request
            //    Create the document object
            Document document = new Document
            {
                DocumentBase64 = Convert.ToBase64String(ReadContent(docName)),
                Name = "Lorem Ipsum",
                FileExtension = "pdf",
                DocumentId = "1"
            };
            Document[] documents = new Document[] { document };

            // Create the signer recipient object 
            Signer signer = new Signer
            { Email = signerEmail, Name = signerName,
              RecipientId = "1", RoutingOrder = "1"
            };

            // Create the sign here tab (signing field on the document)
            SignHere signHereTab = new SignHere
            { DocumentId = "1", PageNumber = "1", RecipientId = "1",
              TabLabel = "Sign Here Tab", XPosition = "195", YPosition = "147"
            };
            SignHere[] signHereTabs = new SignHere[] { signHereTab };

            // Add the sign here tab array to the signer object.
            signer.Tabs = new Tabs { SignHereTabs = new List<SignHere>(signHereTabs) };
            // Create array of signer objects
            Signer[] signers = new Signer[] { signer };
            // Create recipients object
            Recipients recipients = new Recipients { Signers = new List<Signer>(signers) };
            // Bring the objects together in the EnvelopeDefinition
            EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition
            {
                EmailSubject = "Please sign the document",
                Documents = new List<Document>(documents),
                Recipients = recipients,
                Status = "sent"
            };

            // 2. Use the SDK to create and send the envelope
            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient.Configuration);
            EnvelopeSummary results = envelopesApi.CreateEnvelope(accountId, envelopeDefinition);
            ViewData["results"] = $"Envelope status: {results.Status}. Envelope ID: {results.EnvelopeId}";

            return new PageResult();
        }

        /// <summary>
        /// This method read bytes content from the project's Resources directory
        /// </summary>
        /// <param name="fileName">resource path</param>
        /// <returns>return bytes array</returns>
        internal static byte[] ReadContent(string fileName)
        {
            byte[] buff = null;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    long numBytes = new FileInfo(path).Length;
                    buff = br.ReadBytes((int)numBytes);
                }
            }

            return buff;
        }
    }
}
