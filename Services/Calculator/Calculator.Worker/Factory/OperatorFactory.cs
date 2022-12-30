using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Worker
{
    public class OperatorFactory
    {
        public static IOperator GetOperator(string operatorName)
        {
            IOperator operatorObj = null;

            switch(operatorName)
            {
                case "+":
                    operatorObj = new AdditionOperator();
                    break;
                case "-":
                    operatorObj = new SubstractionOperator();
                    break;
                case "*":
                    operatorObj = new MultiplicationOperator();
                    break;
                case "/":
                    operatorObj = new DivisionOperator();
                    break;
            }

            return operatorObj;
        }
    }
}
