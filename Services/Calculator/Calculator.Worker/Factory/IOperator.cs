using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Worker
{
    public interface IOperator
    {
        static string Name { get; }
        string Calculate(int firstNumber, int secondNumber);
    }
}
