using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;

namespace AttendanceBackend.Services
{
    public class TwilioService
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string fromNumber;

        public TwilioService(IConfiguration config)
        {
            accountSid = config[""];
            authToken = config[""];
            fromNumber = config[""];

            TwilioClient.Init(accountSid, authToken);
        }

        public string SendSms(string to, string body)
        {
            var message = MessageResource.Create(
                body: body,
                from: new PhoneNumber(fromNumber),
                to: new PhoneNumber(to)
            );

            return message.Sid;
        }
    }
}