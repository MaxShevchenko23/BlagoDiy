using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BlagoDiy.Controllers
{
    //заметки максиму сигме
    //это контроллер для оплат ликвидпея короче вот
    [ApiController]
    [Route("api/pay")]
    public class LiqPayController : ControllerBase
    {
        //мои ключи которые при регестрации дали
        private const string publicKey = "sandbox_i40382871027";
        private const string privateKey = "sandbox_Cyo1KScLfPBoa4PMKZsRhfphOXq7Q1MVIROetKqh";

        //метод который будет делать запрос на оплату
        [HttpPost]
        public IActionResult GetPayment([FromBody] PaymentRequest request)
        {
            var payload = new
            {
                public_key = publicKey,
                version = "3",// их версия апи
                action = "pay",// тип действия 
                amount = request.Amount,
                currency = "UAH",
                description = "Допомога BlagoDiy",
                order_id = $"donate_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}", // уникальный айдишник
                sandbox = 1, // как раз значение для тестовых списаний и тд чтоб оно прям работало
                result_url = $"http://localhost:3000/campaigns/donate/checkout?status=success&amount={request.Amount}&campaignId={request.CampaignId}&message={request.Message}" // юзера перекинет на страничку зачисления
            };

            // передача всех этих параметров через base64 потомучто так ликвид пей сказал 
            string json = JsonConvert.SerializeObject(payload);
            string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            //это проверка что именно ты формиурешь запрос по хешированию твоего приватного ключа
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(privateKey + data + privateKey));
            string signature = Convert.ToBase64String(hash);// по сути разрешение при корректных данных

            return Ok(new {data, signature});// эти строки я получаю на фронте чтобы отправить в ликвид пей форму

        }

        public class PaymentRequest
        {
            public decimal Amount { get; set; }
            public string Message { get; set; }
            public int CampaignId { get; set; }
        }
    }
}