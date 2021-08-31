using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Model;
using TestCalculator.Operations;

namespace TestCalculator
{
/// <summary>
/// Основной класс для взаимодействия
/// Для добавления новых арифметических операций необходимо создать класс-наследник от абстрактного класса "Operation"
/// Все арифметические операции необходимо создавать в папке "Operations"(необходимо для автоматического добавления в список операций)
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите пример для решения");
            var expression = Console.ReadLine();
            Console.WriteLine(MathOperations.GetFinalResult(expression));
            Console.ReadLine();
           
        }
    }
}
