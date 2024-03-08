using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace MASInovacoes.Function
{
    public class EmailNotificationHttpTrigger
    {
        private readonly ILogger<EmailNotificationHttpTrigger> _logger;

        public EmailNotificationHttpTrigger(ILogger<EmailNotificationHttpTrigger> logger)
        {
            _logger = logger;
        }

        [Function("EmailNotificationHttpTrigger")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Foi acionado uma função via HTTP Trigger");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync(cancellationToken);
            var message = requestBody;
            var factory = new ConnectionFactory(){
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel()){
                channel.QueueDeclare(queue: "EmailQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "EmailQueue", basicProperties: null, body: body);

                _logger.LogInformation($"A mensagem foi enviada, detalhes : {message} ");
            }
            
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
