using Calculator.Application.Dtos;

namespace Calculator.Application.DomainServices.Impl
{
    public class CalculatorService : ICalculatorService
    {
        public Task<List<MathOperationResponseDto>> GetAllOperations(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task SubmitRequest(MathOperationRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}