namespace BeksultanQA
{
    internal class BigInteger
    {
        private List<int> digits;
        private List<int> remainder = new List<int> {};


        // Конструктор, принимающий строку цифр
        public BigInteger(string number)
        {
            digits = new List<int>();
            for (int i = number.Length - 1; i >= 0; i--)
            {
                digits.Add(int.Parse(number[i].ToString()));
            }
        }

        // Конструктор, принимающий список цифр
        public BigInteger(List<int> digits)
        {
            this.digits = new List<int>(digits);
        }

        // Переводим число в строку для вывода
        public override string ToString()
        {
            string result = "Результат: ";
            string resultRemander = "";
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                result += digits[i];
            }
            if (remainder.Count > 0)
            {
                resultRemander += "\nОстаток: ";
                for (int i = remainder.Count - 1; i >= 0; i--)
                {
                    resultRemander += remainder[i];
                }
            }
            return result+resultRemander;
        }

        // Реализация операции сложения
        public static BigInteger operator +(BigInteger num1, BigInteger num2)
        {
            List<int> result = new List<int>();
            int carry = 0;

            int maxLength = Math.Max(num1.digits.Count, num2.digits.Count);

            for (int i = 0; i < maxLength || carry != 0; i++)
            {
                int sum = carry;
                if (i < num1.digits.Count) sum += num1.digits[i];
                if (i < num2.digits.Count) sum += num2.digits[i];

                result.Add(sum % 10);
                carry = sum / 10;
            }

            return new BigInteger(result);
        }

        // Реализация операции умножение
        public static BigInteger operator *(BigInteger num1, BigInteger num2)
        {

            List<int> result = new List<int>(new int[num1.digits.Count + num2.digits.Count]);

            for (int i = 0; i < num1.digits.Count; i++)
            {
                int carry = 0;

                for (int j = 0; j < num2.digits.Count || carry != 0; j++)
                {
                    int product = result[i + j] + carry + num1.digits[i] * ((j < num2.digits.Count) ? num2.digits[j] : 0);
                    result[i + j] = product % 10;
                    carry = product / 10;
                }
            }

            // Удаление лишних нулей
            while (result.Count > 1 && result[result.Count - 1] == 0)
            {
                result.RemoveAt(result.Count - 1);
            }

            return new BigInteger(result);
        }
        
        // Реализация операции вычитание
        public static BigInteger operator -(BigInteger num1, BigInteger num2)
        {
            List<int> result = new List<int>();
            int borrow = 0;

            int maxLength = Math.Max(num1.digits.Count, num2.digits.Count);

            for (int i = 0; i < maxLength; i++)
            {
                int digit1 = (i < num1.digits.Count) ? num1.digits[i] : 0;
                int digit2 = (i < num2.digits.Count) ? num2.digits[i] : 0;

                int diff = digit1 - digit2 - borrow;

                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result.Add(diff);
            }

            // Удаление лишних нулей
            while (result.Count > 1 && result[result.Count - 1] == 0)
            {
                result.RemoveAt(result.Count - 1);
            }

            return new BigInteger(result);
        }

        // Реализация операции деление
        public static BigInteger operator /(BigInteger numerator, BigInteger denominator)
        {
            if (denominator.IsZero())
            {
                throw new DivideByZeroException("Деление на ноль.");
            }

            BigInteger quotient = new BigInteger("0");

            while (numerator.CompareTo(denominator) >= 0)
            {
                numerator = numerator - denominator;
                quotient = quotient+ One();
            }

            quotient.remainder = numerator.digits;

            return quotient;
        }

        //Возвращает BigInteger который равен единичке
        public static BigInteger One()
        {
            return new BigInteger("1");
        }

        //проверка на ноль
        private bool IsZero()
        {
            return digits.Count == 1 && digits[0] == 0;
        }

        //Проверка BigIntegera
        int CompareTo(BigInteger other)
        {
            if (digits.Count < other.digits.Count)
            {
                return -1;
            }
            else if (digits.Count > other.digits.Count)
            {
                return 1;
            }
            else
            {
                // Сравнение по каждой цифре 
                for (int i = digits.Count - 1; i >= 0; i--)
                {
                    if (digits[i] < other.digits[i])
                    {
                        return -1;
                    }
                    else if (digits[i] > other.digits[i])
                    {
                        return 1;
                    }
                }

                return 0; // Числа равны
            }
        }

        //Проверка на строки на число
        public static bool TryParse(string input, out BigInteger result)
        {
            result = null;

            // Проверка на null и пустую строку
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Удаление лишних нулей
            input = input.TrimStart('0');

            // Проверка на пустую строку после удаления лишних нулей
            if (string.IsNullOrEmpty(input))
            {
                result = new BigInteger("0");
                return true;
            }

            // Проверка на наличие недопустимых символов
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            result = new BigInteger(input);
            return true;
        }
    }
}
