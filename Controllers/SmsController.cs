using Microsoft.AspNetCore.Mvc;
using AttendanceBackend.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AttendanceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SmsController(IConfiguration config)
        {
            _config = config;
            TwilioClient.Init(_config[""], _config[""]);
        }

        [HttpPost("send")]
        public IActionResult SendSms([FromBody] SmsRequest request)
        {
            if (request.Req == null || request.Req.Count == 0)
                return BadRequest("Le champ 'req' est obligatoire et doit contenir au moins un message.");

            foreach (var msg in request.Req)
            {
                if (string.IsNullOrWhiteSpace(msg.To) || string.IsNullOrWhiteSpace(msg.Message))
                    return BadRequest("Chaque message doit contenir 'To' et 'Message'.");

                MessageResource.Create(
                    to: new PhoneNumber(msg.To),
                    from: new PhoneNumber(_config["Twilio:PhoneNumber"]),
                    body: msg.Message
                );
            }

            return Ok(new { Status = "SMS envoyés avec succès !" });
        }
    }
}