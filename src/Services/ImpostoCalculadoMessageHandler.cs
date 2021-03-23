using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using ModCidadao.Repositories;
using ModCidadao.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ModCidadao.Services
{
    public class ImpostoCalculadoService : BackgroundService
    {
        const string topico = "stur-imposto-calculado";
        private IConsumer<Ignore, string> kafkaConsumer;

        private readonly IServiceScopeFactory scopeFactory;

        public ImpostoCalculadoService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        } 

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            return Task.CompletedTask;
        }        

        protected void StartConsumerLoop(CancellationToken stoppingToken)
        {
            var scopeDI = scopeFactory.CreateScope();
            var kafkaConfig = scopeDI.ServiceProvider.GetRequiredService<IOptions<KafkaConfig>>();
            var iPTUService = scopeDI.ServiceProvider.GetRequiredService<IPTUService>();

            var conf = new ConsumerConfig
            {
                GroupId = "stur_imposto_group",
                BootstrapServers = kafkaConfig.Value.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            this.kafkaConsumer = new ConsumerBuilder<Ignore, string>(conf).Build();

            kafkaConsumer.Subscribe(topico);
            Console.WriteLine($"    Conectando ao tópico: {topico}              " );

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine($"    Conectando ao tópico: {topico}   while           " );                    
                    var consumo = this.kafkaConsumer.Consume(stoppingToken);
                    
                    if (!string.IsNullOrEmpty(consumo.Message.Value)) {
                        Console.Write($"KAFKA: {consumo.Message.Value}");                    
                        var IPTU = JsonSerializer.Deserialize<IPTU>(consumo.Message.Value);
                        iPTUService.AtualizarImposto(IPTU);
                    }                            
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Kafka operação cancelada");
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    //TO DO ver a possibilidade de mudar para continue
                }
            }            
            Console.WriteLine($" KAFKA SAIR DO TÓPICO!!!!!!!   " );
        }
    
        public override void Dispose()
        {
            Console.WriteLine($" KAFKA DISPOSE!!!!!!!   " );
            this.kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            this.kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}
