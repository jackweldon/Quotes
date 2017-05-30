using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Quotes.Core.Services
{
    public static class QuotesApi
    {

        public static async Task<List<QuoteModel>> FetchQuotesAsync(string url)
        {
            try
            {

                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";

                using (var client = new HttpClient())
                {
                    var response =
                            await client.GetAsync(url, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                        // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var res = JsonConvert.DeserializeObject<List<QuoteModel>>(responseContent);
                        return res;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
