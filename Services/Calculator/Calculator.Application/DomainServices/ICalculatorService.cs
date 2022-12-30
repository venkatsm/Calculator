using Calculator.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Application.DomainServices
{
    public interface ICalculatorService
    {
        Task SubmitRequest(MathOperationRequestDto request);

        Task<List<MathOperationResponseDto>> GetAllOperations(string sessionId);
    }
}
