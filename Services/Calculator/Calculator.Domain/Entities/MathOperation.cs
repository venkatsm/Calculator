using Base.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Base.Constants.Constants.Constants.ClassificationValue;

namespace Calculator.Domain.Entities
{
    public class MathOperation : AuditEntity<Guid>
    {
        public MathOperation()
        {
            Type = "math-operation";
        }
        [JsonProperty("firstNumber")]
        public int FirstNumber { get; set; }
        [JsonProperty("secondNumber")]
        public int SecondNumber { get; set; }
        [JsonProperty("operator")]
        public string Operator { get; set; } = string.Empty;
        [JsonProperty("requestReceivedTime")]
        public DateTime? RequestReceivedTime { get; set; }
        [JsonProperty("workerEnqueueTime")]
        public DateTime? WorkerEnqueueTime { get; set; }
        [JsonProperty("workerDequeueTime")]
        public DateTime? WorkerDequeueTime { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; } = string.Empty;
        [JsonProperty("status")]
        public string Status { get; set; } = MathOpertionStatus.New;
    }
}
