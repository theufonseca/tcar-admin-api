using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ.Models
{
    public class RabbitMQConfig
    {
        public string Host { get; set; } = default!;
        public string Port { get; set; } = default!;
        public string QueueName { get; set; } = default!;
    }
}
