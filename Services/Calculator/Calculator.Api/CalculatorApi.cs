using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Base.Constants.Constants;
using Calculator.Application.Dtos;
using Calculator.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Notification.Client.Services;
using Notification.Client.Services.Impl;

namespace Calculator.Api
{
    public class CalculatorApi
    {
        private readonly ILogger<CalculatorApi> _logger;
        private readonly INotificationService _notificationService;

        public CalculatorApi(ILogger<CalculatorApi> log)
        {
            _logger = log;
            _notificationService = new NotificationService();
        }

        [FunctionName(nameof(SubmitMathOperationRequest))]
        [OpenApiOperation(operationId: nameof(SubmitMathOperationRequest), tags: new[] { "name" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MathOperationRequestDto), Description = nameof(MathOperationRequestDto), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json ", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> SubmitMathOperationRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "SubmitMathOperationRequest")] HttpRequest req,
            [CosmosDB(
                databaseName: "calculator-db",
                containerName: "math-operations",
                Connection = "CosmosDbConnectionString")]IAsyncCollector<MathOperation> documentsOut,
            [ServiceBus(
                queueOrTopicName: "calculator-app-events", 
                Connection = "ServiceBusConnectionString")]IAsyncCollector<MathOperation> messagesOut
        )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<MathOperationRequestDto>(requestBody);
            var operation = new MathOperation()
            {
                // create a random ID
                Key = Guid.NewGuid(),
                FirstNumber = request.FirstNumber,
                SecondNumber = request.SecondNumber,
                Operator = request.Operator,
                WorkerEnqueueTime = DateTime.Now,
                RequestReceivedTime = DateTime.Now,
                PartitionKey = request.SessionId,
            };

            await documentsOut.AddAsync(operation);
            await messagesOut.AddAsync(operation);

            await _notificationService.SendNotification(new Notification.Client.Dtos.NotificationRequest()
            {
                Id = operation.Key,
                SessionId = operation.PartitionKey,
                Result = operation.Result,
                Status = Constants.ClassificationValue.MathOpertionStatus.Enqueued
            });

            return new OkObjectResult(operation.Key);
        }
    }
}

