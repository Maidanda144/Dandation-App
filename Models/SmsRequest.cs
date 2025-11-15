using System.Collections.Generic;

namespace AttendanceBackend.Models
{
    public class SmsMessage
    {
        public string To { get; set; } = "";
        public string Message { get; set; } = "";
    }

    public class SmsRequest
    {
        public List<SmsMessage> Req { get; set; } = new();
    }
}