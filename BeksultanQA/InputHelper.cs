namespace BeksultanQA
{
    internal class InputHelper
    {
        //Проверка строки на Null и пробелы
        public static string ReadString(string inputMessage)
        {
            Console.WriteLine(inputMessage);
            string? value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("Пожалуйста, попробуйте еще раз");
            return ReadString(inputMessage);
        }

        //Проверка строки на число
        public static BigInteger ReadNumber(string inputMessage)
        {
            string value = ReadString(inputMessage);
            if (BigInteger.TryParse(value, out BigInteger number))
            {
                return number ;
            }

            Console.WriteLine("Введите число, используя цифры");
            return ReadNumber(inputMessage);
        }
    }
}
