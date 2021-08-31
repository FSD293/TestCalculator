using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Model;

namespace TestCalculator.Operations
{
    /// <summary>
    /// Класс умножения
    /// Наследуется от класса Operation
    /// </summary>
    class Multiplication : Operation
    {

        private Multiplication(uint priority, char operand) : base(priority, operand) { }
        public Multiplication() : base() { }
        public override Operation GetInstance()
        {
            return new Multiplication(1, '*');

        }
        public override double GetResult(double a, double b)
        {

            return a * b;
        }
    }
}
