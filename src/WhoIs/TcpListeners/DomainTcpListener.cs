using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WhoIs.Common;
using WhoIs.Infrastructure.Repositories;

namespace WhoIs.TcpListeners
{
    public class DomainTcpListener : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DomainTcpListener> _logger;

        private readonly TcpListener _listener;

        public DomainTcpListener(IServiceProvider serviceProvider, ILogger<DomainTcpListener> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            _listener = new TcpListener(IPAddress.Any, 43);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            _logger.LogInformation("TCP listener started");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogDebug("Waiting for a connection...");

                    using var client = await _listener.AcceptTcpClientAsync();

                    _logger.LogDebug("Connected!");

                    await using var stream = client.GetStream();
                    using var streamReader = new StreamReader(stream);

                    var requestString = await streamReader.ReadLineAsync() ?? string.Empty;

                    _logger.LogInformation("Received via tcp: {Data}", requestString);

                    string resultString;

                    try
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var domainService = scope.ServiceProvider.GetRequiredService<IDomainRepository>();

                        var result = await domainService.GetDomainsAsync(requestString);
                        resultString = JsonSerializer.Serialize(result);
                    }
                    catch (NotFoundException e)
                    {
                        resultString = "Domain not found";
                    }
                    catch (Exception e)
                    {
                        resultString = $"Exception: {e.Message}";
                    }

                    var response = Encoding.ASCII.GetBytes(resultString);
                    await stream.WriteAsync(response, 0, response.Length, stoppingToken);

                    _logger.LogInformation("Sent via tcp: {Reply}", resultString);

                    client.Close();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception in TCP listener: {Exception}", e);
            }
            finally
            {
                _listener.Stop();
            }
        }
    }
}
