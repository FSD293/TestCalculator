using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Model;

namespace TestCalculator.Operations
{
    /// <summary>
    /// Класс деления
    /// Наследуется от класса Operation
    /// </summary>
    class Division : Operation
    {

        private Division(uint priority, char operand) : base(priority, operand) { }
        public Division() : base() { }
        public override Operation GetInstance()
        {

            return new Division(1, '/');

        }
        public override double GetResult(double a, double b)
        {
            return a / b ;
        }
    }
}
