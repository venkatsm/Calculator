using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Worker
{
    public class MultiplicationOperator : IOperator
    {
        public static string Name => "*";

        public string Calculate(int firstNumber, int secondNumber)
        {
            try
            {
                return $"{firstNumber * secondNumber}";
            }
            catch (Exception)
            {
                return "NaN";
            }
        }
    }
}
