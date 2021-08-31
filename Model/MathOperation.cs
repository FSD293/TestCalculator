using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestCalculator.Operations;

namespace TestCalculator.Model
{
    /// <summary>
    /// Класс математических операций
    /// </summary>
    public static class MathOperations
    {
        /// <value>
        /// Глобальная переменная содержащая выражение
        /// </value>
        private static string Expression;
        /// <value>
        /// Глобальная переменная содержащая список операций
        /// </value>
        private static List<Operation> Operations;
        /// <value>
        /// Глобальная переменная содержащая текущую операцию
        /// </value>
        private static Operation CurrentOperation;
        /// <summary>
        /// Метод для решения выражений
        /// </summary>
        /// <param name="expression">выражение</param>
        /// <returns>Возвращает ответ заданного выражения</returns>
        public static double GetFinalResult(string expression)
        {
            /// Присвоение выражения глобальной переменной
            Expression = expression;
            /// Проверка корректности выражения
            if (!CheckCorrect())
            {
                Console.WriteLine("Wrong data");
                return 0;
            }
            /// Вызов метода получения арифметических операций
            GetOperations();
            /// Вызов метода преобразования выражения в List<string>
            var ExpressionList = ExpressionToList(expression);
            /// Начало вычисления
            while (ExpressionList.Count(x => x != "done") > 1)
            {
                /// Проверка наличия в коллекции элементов отличающихся от "done"
                if (ExpressionList.Count(x => x != "done") == 1)
                {
                    break;
                }
                /// Вызов метода получения индекса операции с наивысшим приоритетом
                var index = GetMaxPrioity(ExpressionList);
                /// <value>
                /// значение разности от индекса наивысшей операции и индекса близжайшего числа слева 
                /// </value>
                var min = 1;
                /// <value>
                /// значение разности от индекса наивысшей операции и индекса близжайшего числа справа
                /// </value>
                var plus = 1;
                /// Вычисление индексов близжайших чисел
                for (int i = 1; i < ExpressionList.Count() - index; i++)
                {
                    if (ExpressionList[index + i] != "done")
                    {
                        plus = i;
                        break;
                    }
                }
                for (int i = 1; i <= index; i++)
                {
                    if (ExpressionList[index - i] != "done")
                    {
                        min = i;
                        break;
                    }
                }
                /// Поиск элемента с индексом элемента наивысшего приоритета
                for (int i = 0; i < ExpressionList.Count(); i++)
                {
                    if (ExpressionList[i] == CurrentOperation.Operand.ToString())
                    {
                        /// <value>
                        /// переменная результата выражения
                        /// </value>
                        var result = 1.0;
                        /// Попытка получить результат операции с наивысшим приоритетом
                        try
                        {
                            result = CurrentOperation.GetResult(int.Parse(ExpressionList.ElementAt(index - min)), int.Parse(ExpressionList.ElementAt(index + plus)));
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Wrong Data"); ;
                        }
                        /// Замена элементов в коллекции
                        /// Заменяет 3 элемента
                        /// Первое число слева от элемента с наивысшим приоритетом заменяется на результат выражения
                        /// Элемент с наивысшим приоритетом заменяется на "done"
                        /// Первое число справа от элемента с наивысшим приоритетом заменяется на результат выражения
                        ExpressionList.RemoveAt(index + plus);
                        ExpressionList.Insert(index + plus, "done");
                        ExpressionList.RemoveAt(index);
                        ExpressionList.Insert(index, "done");
                        ExpressionList.RemoveAt(index - min);
                        ExpressionList.Insert(index - min, result.ToString());
                        i = 0;
                        break;
                    }
                }
            }
            /// Возврат ответа
            return double.Parse(ExpressionList.First());
        }
        /// <summary>
        /// Метод получения операций
        /// </summary>
        private static void GetOperations()
        {
            /// Инициализация объекта коллекции операций
            Operations = new List<Operation>();
            /// Получение всех типов из пространства имен "TestCalculator.Operations" (из папки "Operations") базовым типом которых является класс операции
            var TypeList = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == "TestCalculator.Operations").Where(x => x.BaseType == typeof(TestCalculator.Model.Operation)).ToList();
            /// Создание экземпляров классов полученных ранне
            /// Добавление элементов в коллекцию операций
            foreach (var operation in TypeList)
            {
                var tmp = System.Activator.CreateInstance(operation) as Operation;
                Operations.Add(tmp.GetInstance());

            }
        }
        /// <summary>
        /// Метод получения индекса элемента с наивысшим приоритетом
        /// </summary>
        /// <param name="expression">выражение</param>
        /// <returns>индекс элемента с максимальным приоритетом</returns>
        private static int GetMaxPrioity(List<string> expression)
        {
            /// <value>
            /// Контрольная переменная коллекции для проверки
            /// </value>
            var ControlList = ExpressionToList(Expression);
            /// <value>
            /// Переменная максимального приоритета
            /// </value>
            var MaxPriority = -1;
            /// <value>
            /// Переменная индекса максимальной переменной
            /// </value>
            var index = -1;
            /// Перебор элементов коллекции 
            /// Сравнение элементов с операндами(символ операции)
            for (int i = 0; i < expression.Count(); i++)
            {
                foreach (var operation in Operations)
                {
                    /// Сравнение элемента с операндом
                    if (expression[i] == operation.Operand.ToString())
                    {
                        /// Вычисление приоритета операции с учетом скобок
                        var priority = (int)operation.Priority + GetPriorityMultiplier(GetCurrentElementIndex(ControlList, i));
                        /// Сравнение приоритетов операций
                        /// Сохранение индекса переменной с наивысшим приоритетом
                        /// Сохранение операции с наивысшим приоритетом
                        if (priority > MaxPriority)
                        {
                            MaxPriority = priority;
                            index = i;
                            CurrentOperation = operation;
                            break;
                        }
                    }
                }
            }
            /// Возврат индекса операции с наивысшим приоритетом
            return index;
        }
        /// <summary>
        /// Метод преобразования выражения из stirng в List<string>
        /// </summary>
        /// <param name="expression">выражение</param>
        /// <returns>выражение в форме List<string></returns>
        private static List<string> ExpressionToList(string expression)
        {
            /// Удаление всех скобок
            expression = expression.Replace("(", "").Replace(")", "");
            /// Добавление пробелов между операндами
            /// Преобразование sting в List<stirng>
            foreach (var operation in Operations)
            {
                expression = expression.Replace(operation.Operand.ToString(), " " + operation.Operand.ToString() + " ");
            }
            /// Возврат List<string>
            return expression.Split(' ').ToList();
        }
        /// <summary>
        /// Метод проверки корректности введенного выражения
        /// </summary>
        /// <returns>
        /// true-проблем нет 
        /// false - проблема со скобками
        /// </returns>
        private static bool CheckCorrect()
        {
            /// Проверка соответсвия количества открытых и закрытых скобок
            return Expression.Count(x => x == '(') == Expression.Count(x => x == ')') ? true : false;
        }
        /// <summary>
        /// Метод получения множителя приоритета операции
        /// </summary>
        /// <param name="index">индекс операции</param>
        /// <returns>множитель приоритета операции</returns>
        private static int GetPriorityMultiplier(int index)
        {
            /// <value>
            /// Счетчик открытх скобок ")"
            /// </value>
            var counter1 = 0;
            /// <value>
            /// Счетчик закрытых скобок "("
            /// </value>
            var counter2 = 0;
            /// Подсчет скобок идущих после указанного индекса
            for (int i = index; i < Expression.Length; i++)
            {
                if (Expression[i] == '(')
                {
                    counter1++;
                }
                else if (Expression[i] == ')')
                {
                    counter2++;
                }
            }
            /// Возврат множителя приоритета операции
            /// Разница скобок показывает находится элементв скобках или нет
            return 5 * (counter2 - counter1);
        }
        /// <summary>
        /// Получение индекса элемента в оригинальном выражении по указанному индексу из коллекции
        /// Необходимо для вычисления приоритета скобок
        /// </summary>
        /// <param name="modifiedExpression">коллекция элементов выражения</param>
        /// <param name="index">индекс элемента в коллекции</param>
        /// <returns></returns>
        private static int GetCurrentElementIndex(List<string> modifiedExpression, int index)
        {
            /// Получение нужного элемента из коллекции по индексу
            var element = modifiedExpression.ElementAt(index);
            /// Счетчик одинаковых элементов
            /// Необходимо для нахождения конкретного элемента среди одинаковых
            var elementCounter = 0;
            /// Рассчет одинаковых элементов
            /// Высчитывает количество элементов равных исходному до самого элемента
            for (int i = 0; i < index; i++)
            {
                
                if (modifiedExpression[i] == element)
                {
                    elementCounter++;

                }
            }
            /// Поиск индекса нужного объекта в оригинальной строке на основе полученных данных
            for (int i = 0; i < Expression.Length; i++)
            {
                if (Expression[i].ToString() == element)
                {

                    if (elementCounter == 0)
                    {
                        return i;
                    }
                    elementCounter--;
                }
            }
            /// В случае провального поиска возвращает -1 
            /// Нужно для прекращения работы программы
            /// Предотвращает некорреную работу 
            return -1;

        }
    }
}
