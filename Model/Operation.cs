using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculator.Model
{
    /// <summary>
    /// абстрактный класс операции
    /// </summary>
    abstract class Operation
    {
        /// <value>
        /// Исходный приоритет выражения
        /// </value>
        public uint Priority { get; set; }
        /// <value>
        /// Обозначение переменной для нахождения в выражении
        /// </value>
        public char Operand { get; set; }

        /// <summary>
        /// конструктор по умолчанию
        /// Вызывает метод создания операции
        /// Необходимо для автоматического добавление всех операций из папки "Operations"
        /// </summary>
        protected Operation() 
        {
            GetInstance();
        }
        /// <summary>
        /// Конструктор создания элемента
        /// </summary>
        /// <param name="priority">исходный приоритет</param>
        /// <param name="operand">обозначение операции в строке</param>
        protected Operation(uint priority, char operand)
        {
            Priority = priority;
            Operand = operand;
        }
        /// <summary>
        /// Метод вызова Конструктора создания операции
        /// </summary>
        /// <returns>экземпляр класса</returns>
        public abstract Operation GetInstance();
      /// <summary>
      /// Метод описывающий логику операции
      /// </summary>
      /// <param name="a">первое число</param>
      /// <param name="b">второе число</param>
      /// <returns></returns>
        public abstract double GetResult(double a,double b);
    }
}
