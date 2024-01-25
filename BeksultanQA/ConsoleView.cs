namespace BeksultanQA
{
    internal static class ConsoleView
    {
        public static void start()
        {
            //Примерная демонстрации работы с BigInter 
            //Принимаем числа от пользователя
            BigInteger numberOne = InputHelper.ReadNumber("Введите первое число:");
            BigInteger numberTwo = InputHelper.ReadNumber("Введите второе число:");

            //Временная хранилище будущего результата 
            BigInteger result = new BigInteger("");

            Console.WriteLine("Выберите операцию: +, -, *, /");

            //Обработка исключений 
            try
            {
                //Принимаем операцию от пользователя 
                char operation = Convert.ToChar(Console.ReadLine());
                switch (operation)
                {
                    case '+':
                        result = numberOne + numberTwo;
                        break;

                    case '-':
                        result = numberOne - numberTwo;
                        break;

                    case '*':
                        result = numberOne * numberTwo;
                        break;

                    case '/':

                        result = numberOne / numberTwo;
                        break;

                    default:
                        Console.WriteLine("Ошибка: неверная операция.");
                        return;
                }
                Console.WriteLine(result);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Ошибка: неверная операция.");
                Console.WriteLine("Выберите операцию: +, -, *, /");
            }

            catch (Exception ex)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Непредвиденное исключение: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine("----------------------------");
            }
            finally
            {
                start();
            }
        }
    }
}
