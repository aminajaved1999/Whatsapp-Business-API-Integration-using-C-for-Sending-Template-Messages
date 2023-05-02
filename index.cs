using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace WhatsappApiIntegration
{
    class WhatsappMessage

    {
        static async Task Main(string[] args)
        {
            var ApiVersion = "API VERSION";
            var whatsappBusinessAccountId = "YOU WHATSAPP BUSINESS ACCOUNT ID";
            var phoneNumber = "PHONE NUMBER ON WHICH YOU WANT TO SEND THE MESSAGE";
            var templateName = "MESSAGE TEMPATE NAME";
            var templateLanguageCode = "en_US";
            var accessToken = "YOUR ACCESS TOKEN";
            var text = "MESSAGE TO BE SENT";
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://graph.facebook.com/"
                                                                                     + ApiVersion + "/"
                                                                                     + whatsappBusinessAccountId +
                                                                                     "/messages?access_token=" + accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer" + accessToken);
                    var json = "{" +
                                    "\"messaging_product\": \"whatsapp\", \"to\": \"" + phoneNumber + "\", \"type\": \"template\", " +
                                    "\"template\":  " +
                                    "{" +
                                        "\"name\": \""+ templateName + "\", \"language\": {\"code\": \""+templateLanguageCode+"\" }, " +
                                        " \"components\": " +
                                        "[" +
                                            "{" +
                                                "\"type\": \"body\","
                                                + "\"parameters\": [{\"type\": \"text\",\"text\": " +
                                                                                                 "\"," +
                                                                                                 text +
                                                                                                " \"" +
                                                                                                " }]" +
                                            "}" +
                                        "]" +
                                      "}" +
                                  "}";
                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await httpClient.SendAsync(request);
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: " + content);

                }
            }
        }
    }
}
