using Application.Interfaces;
using Application.Shared;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Infra.RabbitMQ.Publishers
{
    public class VehicleSpecPub : IVehicleSpecPub, IDisposable
    {
        private readonly RabbitMQConnection connection;

        public VehicleSpecPub(RabbitMQConnection connection)
        {
            this.connection = connection;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public Task SendAsync(VehicleSpecCreated specCreated, CancellationToken cancellationToken)
        {
            var channel = connection.channel;

            channel.BasicPublish(
                exchange: "", 
                routingKey: "vehicleSpec_created", 
                basicProperties: null, 
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(specCreated)));

            return Task.CompletedTask;
        }
    }
}
