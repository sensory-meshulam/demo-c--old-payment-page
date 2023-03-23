using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using static demo_api_old.Models.paymentModels;

namespace demo_api_old.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController
    {
      
        const string MeshulamApiUrl = "https://sandbox.meshulam.co.il/api/light/server/1.0/";

        [HttpPost]
        public async Task<GenericResultDto> GetPaymentLink([FromBody] GetPaymentLinkRequest req)
        {
            var result = new GenericResultDto();
            var pageCode = "b73ca07591f8"; // generic-page
                //credit: '0b7a16e03b25'
                //google-pay: '77a2993849cd'
                //apple-pay: '9eeea7787d67'
                //bit: 'e20c9458e9f3'
                //bit QR: '39bf173ce7d0'
            var userId = "4ec1d595ae764243";
            var successUrl = "https://localhost:44374/Client/success.html?success=1";
            var failureUrl = "https://localhost:44374/Client/failure.html?failure=1";
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(pageCode), "pageCode");
            form.Add(new StringContent(userId), "userId");
            //form.Add(new StringContent(MeshulamApiKey), "apiKey");
            form.Add(new StringContent(req.Sum.ToString()), "sum"); // סכום   
            form.Add(new StringContent(req.PaymentsNum.ToString()), "paymentNum"); // מספר תשלומים
            form.Add(new StringContent(successUrl), "successUrl");
            form.Add(new StringContent(failureUrl), "cancelUrl");
            form.Add(new StringContent(req.Description), "description");
            // Here you can use the two parameters you chose for your payment-page. In this case full name and phone number.
            // You can send them here, or not and the user will fill them in
            form.Add(new StringContent("John Smit"), "pageField[fullName]");
            form.Add(new StringContent("0500000000"), "pageField[phone]");
            // With the help of cFields you can transfer information that will be retrieved on the success page (limited to 5 cFields)
            form.Add(new StringContent(pageCode), "cField1");
            form.Add(new StringContent("blabla"), "cField2");
            form.Add(new StringContent("blabla"), "cField3");
            form.Add(new StringContent("blabla"), "cField4");
            form.Add(new StringContent("blabla"), "cField5");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await client.PostAsync(MeshulamApiUrl + "createPaymentProcess", form);
            var responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<GetPaymentLinkResponse>(responseString);
            result.IsSuccess = res.Status > 0;
            result.Message = result.IsSuccess ? res.Data.Url : res.Err.message;
            return result;
        }
    }
}


