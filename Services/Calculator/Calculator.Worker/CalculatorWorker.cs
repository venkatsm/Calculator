using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Base.Constants.Constants;
using Calculator.Domain.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notification.Client;
using Notification.Client.Services;
using Notification.Client.Services.Impl;
using Refit;

namespace Calculator.Worker
{
    public class CalculatorWorker
    {
        private readonly ILogger<CalculatorWorker> _logger;
        private readonly INotificationService _notificationService;

        public CalculatorWorker(ILogger<CalculatorWorker> log)
        {
            _logger = log;
            _notificationService = new NotificationService();
        }

        [FunctionName(nameof(ProcessMathOperationRequest))]
        public async Task ProcessMathOperationRequest(
            [ServiceBusTrigger(
                "calculator-app-events", 
                "calculator-worker-request", 
                Connection = "ServiceBusConnectionString")]string mathOperationMsg,
            [CosmosDB(
                databaseName: "calculator-db",
                containerName: "math-operations",
                Connection = "CosmosDbConnectionString")]IAsyncCollector<MathOperation> documentsOut
        )
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mathOperationMsg}");


            var operation = JsonConvert.DeserializeObject<MathOperation>(mathOperationMsg);

            await _notificationService.SendNotification(new Notification.Client.Dtos.NotificationRequest()
            {
                Id = operation.Key,
                SessionId = operation.PartitionKey,
                Result = operation.Result,
                Status = Constants.ClassificationValue.MathOpertionStatus.InProgress
            });

            Thread.Sleep(5000);

            operation.WorkerDequeueTime = DateTime.Now;
            operation.ModifiedTime = DateTime.Now;

            try
            {
                operation.Result = OperatorFactory
                    .GetOperator(operation.Operator)
                    .Calculate(operation.FirstNumber, operation.SecondNumber);
                operation.Status = Constants.ClassificationValue.MathOpertionStatus.Success;
                await documentsOut.AddAsync(operation);
            }
            catch (Exception)
            {
                operation.Status = Constants.ClassificationValue.MathOpertionStatus.Error;
                await documentsOut.AddAsync(operation);
            }

            await _notificationService.SendNotification(new Notification.Client.Dtos.NotificationRequest()
            {
                Id = operation.Key,
                SessionId = operation.PartitionKey,
                Result = operation.Result,
                Status = operation.Status
            });
        }
    }
}
