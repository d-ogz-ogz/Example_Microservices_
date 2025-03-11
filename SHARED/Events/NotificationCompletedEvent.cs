using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Events
{
    public class NotificationCompletedEvent
    {
        public Guid OrderId { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = "";
    }
}
