using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Model;

namespace TestCalculator.Operations
{
    /// <summary>
    /// Класс вычитания
    /// Наследуется от класса Operation
    /// </summary>
    class Subtraction : Operation
    {

        private Subtraction(uint priority, char operand) : base(priority, operand) { }
        public Subtraction() : base() { }
        public override Operation GetInstance()
        {

            return new Subtraction(1, '-');
        }
        public override double GetResult(double a, double b)
        {

            return a - b;
        }
    }
}
