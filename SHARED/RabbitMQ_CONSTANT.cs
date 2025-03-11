using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public static class RabbitMQConstants
    {
        public static class QueueNames
        {
            public const string OrderCreated = "order-created-queue";
            public const string PaymentCompleted = "payment-completed-queue";
            public const string NotificatonCompleted = "notification-completed-queue";
            public const string StockCheck = "stock-check-queue";
            public const string StockRollback = "stock-rollback-queue";
            public const string PaymentNotCompleted = "payment-not-completed-queue";
        }
    }
}