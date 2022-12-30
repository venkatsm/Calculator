using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Application.Dtos
{
    public class MathOperationRequestDto
    {
        public string SessionId { get; set; } = string.Empty;
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public string Operator { get; set; } = string.Empty;
    }
}
