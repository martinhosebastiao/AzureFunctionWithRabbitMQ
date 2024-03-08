using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MASInovacoes.Function
{
    public class EmailNotificationQueueTrigger
    {
        private readonly ILogger<EmailNotificationQueueTrigger> _logger;

        public EmailNotificationQueueTrigger(ILogger<EmailNotificationQueueTrigger> logger)
        {
            _logger = logger;
        }

        [Function("EmailNotificationQueueTrigger")]
        public async Task  Run([RabbitMQTrigger("EmailQueue", ConnectionStringSetting = "RabbitMQConnection")] string message)
        { 
             _logger.LogInformation("Foi acionado uma função via Queue Trigger");

            var playload = JsonSerializer.Deserialize<dynamic>(message);

            // Bloco de instruções que deseja executar.!

            _logger.LogInformation($" A mensagem foi recebida com sucesso. Detalhes: {playload}");

            await Task.Delay(1000);
        }
    }
}
