using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Model;

namespace TestCalculator.Operations
{
    /// <summary>
    /// Класс сложения
    /// Наследуется от класса Operation
    /// </summary>
    class Addition:Operation
    {
        
        private Addition(uint priority, char operand) : base(priority, operand) { }

        public Addition():base() { }
        public override Operation GetInstance()
        {
            return new Addition(0, '+');
            
        }
        public override double GetResult(double a, double b)
        {
            return a + b;
        }

        
    }
}
