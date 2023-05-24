using System;
using System.Text;

public class BigInteger
{
    private int[] _digitArray;
    private bool _isNegativeNumber;

    public BigInteger(string numberAsString)
    {
        if (numberAsString.StartsWith("-"))
        {
            _isNegativeNumber = true;
            numberAsString = numberAsString.Substring(1);
        }
        
        char[] charArray = numberAsString.ToCharArray();
        Array.Reverse(charArray);
        
        _digitArray = new int[charArray.Length];
        
        for (int i = 0; i < charArray.Length; i++)
        {
            _digitArray[i] = int.Parse(charArray[i].ToString());
        }
    }
    
    /*public BigInteger(string numericString)
    {
        _reversedDigits = new int[numericString.Length];
        
        for (int index = 0; index < numericString.Length; index++)
        {
            _reversedDigits[index] = (int)char.GetNumericValue(numericString[numericString.Length - 1 - index]);
        }
    }

    private BigInteger(int[] digitArray)
    {
        _reversedDigits = digitArray;
    }

    public BigInteger ShiftLeft()
    {
        int[] shiftedArray = new int[_reversedDigits.Length + 1];
        _reversedDigits.CopyTo(shiftedArray, 1);
        return new BigInteger(shiftedArray);
    }

    /*public override string ToString()
    {
        char[] chars = new char[_reversedDigits.Length];
        for (int index = 0; index < _reversedDigits.Length; index++)
        {
            chars[_reversedDigits.Length - index - 1] = (char)('0' + _reversedDigits[index]);
        }
        return new string(chars);
    }*/ 
    
    public static BigInteger operator +(BigInteger number1, BigInteger number2)
    {
        if (number1._isNegativeNumber && !number2._isNegativeNumber)
        {
            return SubtractAbsoluteValues(number2, number1.GetAbsoluteValue());
        }
        else if (!number1._isNegativeNumber && number2._isNegativeNumber)
        {
            return SubtractAbsoluteValues(number1, number2.GetAbsoluteValue());
        }
        else if (number1._isNegativeNumber && number2._isNegativeNumber)
        {
            BigInteger sum = AddAbsoluteValues(number1.GetAbsoluteValue(), number2.GetAbsoluteValue());
            sum._isNegativeNumber = true;
            return sum;
        }
        else
        {
            return AddAbsoluteValues(number1, number2);
        }
    }

    
    public static BigInteger operator -(BigInteger number1, BigInteger number2)
    {
        if (number1._isNegativeNumber && !number2._isNegativeNumber)
        {
            BigInteger difference = AddAbsoluteValues(number1.GetAbsoluteValue(), number2);
            difference._isNegativeNumber = true;
            return difference;
        }
        else if (!number1._isNegativeNumber && number2._isNegativeNumber)
        {
            return AddAbsoluteValues(number1, number2.GetAbsoluteValue());
        }
        else if (number1._isNegativeNumber && number2._isNegativeNumber)
        {
            // Compare the absolute values to determine the sign of the result
            int comparison = CompareAbsoluteValues(number1, number2);
            if (comparison == 0)
            {
                return new BigInteger("0");
            }
            else if (comparison > 0)
            {
                return SubtractAbsoluteValues(number1.GetAbsoluteValue(), number2.GetAbsoluteValue());
            }
            else
            {
                BigInteger difference = SubtractAbsoluteValues(number2.GetAbsoluteValue(), number1.GetAbsoluteValue());
                difference._isNegativeNumber = true;
                return difference;
            }
        }
        else
        {
            // Compare the absolute values to determine the sign of the result
            int comparison = CompareAbsoluteValues(number1, number2);
            if (comparison == 0)
            {
                return new BigInteger("0");
            }
            else if (comparison > 0)
            {
                return SubtractAbsoluteValues(number1, number2);
            }
            else
            {
                BigInteger difference = SubtractAbsoluteValues(number2, number1);
                difference._isNegativeNumber = true;
                return difference;
            }
        }
    }

    private static int CompareAbsoluteValues(BigInteger number1, BigInteger number2)
    {
        if (number1._digitArray.Length > number2._digitArray.Length)
        {
            return 1;
        }
        else if (number1._digitArray.Length < number2._digitArray.Length)
        {
            return -1;
        }
        else
        {
            for (int i = number1._digitArray.Length - 1; i >= 0; i--)
            {
                if (number1._digitArray[i] > number2._digitArray[i])
                {
                    return 1;
                }
                else if (number1._digitArray[i] < number2._digitArray[i])
                {
                    return -1;
                }
            }
            return 0;
        }
    }

    
    private static BigInteger AddAbsoluteValues(BigInteger number1, BigInteger number2)
    {
        int maxLength = Math.Max(number1._digitArray.Length, number2._digitArray.Length);
        int minLength = Math.Min(number1._digitArray.Length, number2._digitArray.Length);

        int[] result = new int[maxLength + 1];

        int carry = 0;
        int sum;

        for (int i = 0; i < maxLength; i++)
        {
            int digit1 = i < number1._digitArray.Length ? number1._digitArray[i] : 0;
            int digit2 = i < number2._digitArray.Length ? number2._digitArray[i] : 0;

            sum = carry + digit1 + digit2;
            result[i] = sum % 10;
            carry = sum / 10;
        }

        if (carry > 0)
        {
            result[maxLength] = carry;
        }

        BigInteger sumNumber = new BigInteger("");
        sumNumber._digitArray = result;

        return sumNumber;
    }

    private static BigInteger SubtractAbsoluteValues(BigInteger number1, BigInteger number2)
    {
        int maxLength = Math.Max(number1._digitArray.Length, number2._digitArray.Length);
        int[] result = new int[maxLength];
        int borrow = 0;

        for (int i = 0; i < maxLength; i++)
        {
            int digit1 = i < number1._digitArray.Length ? number1._digitArray[i] : 0;
            int digit2 = i < number2._digitArray.Length ? number2._digitArray[i] : 0;

            int difference = digit1 - digit2 - borrow;

            if (difference < 0)
            {
                difference += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }

            result[i] = difference;
        }

        BigInteger differenceNumber = new BigInteger("");
        differenceNumber._digitArray = result;

        return differenceNumber;
    }

    private BigInteger GetAbsoluteValue()
    {
        BigInteger absoluteValue = new BigInteger("");
        absoluteValue._digitArray = _digitArray;
        return absoluteValue;
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder(_digitArray.Length + (_isNegativeNumber ? 1 : 0));

        if (_isNegativeNumber)
        {
            sb.Append("-");
        }

        bool leadingZeros = true;
        for (int i = _digitArray.Length - 1; i >= 0; i--)
        {
            if (leadingZeros && _digitArray[i] == 0)
            {
                continue;
            }

            leadingZeros = false;
            sb.Append(_digitArray[i]);
        }

        // If all digits are zeros, append a single zero
        if (leadingZeros)
        {
            sb.Append("0");
        }

        return sb.ToString();
    }

    private int[] GetDigitsFromHalf(int startIndex, int endIndex)
    {
        int[] halfDigits = new int[endIndex - startIndex + 1];
        Array.Copy(_digitArray, startIndex, halfDigits, 0, endIndex - startIndex + 1);
        return halfDigits;
    }

    /*public BigInteger Add(BigInteger otherNumber)
    {
        // return new BigInteger, result of current + another
    }

    public BigInteger Subtract(BigInteger otherNumber)
    {
        // return new BigInteger, result of current - another
    }*/
    
    /*
    public BigInteger Multiply(BigInteger multiplier)
    {
        // single digit multiplication
        if (_reversedDigits.Length == 1 && multiplier._reversedDigits.Length == 1)
        {
            int multiplicationResult = _reversedDigits[0] * multiplier._reversedDigits[0];
            return new BigInteger(multiplicationResult.ToString());
        }

        // size of the numbers
        int midPoint = Math.Max(_reversedDigits.Length, multiplier._reversedDigits.Length) / 2;
        
        // split the input numbers
        // in two halves at the calculated midpoint
        BigInteger highDigitsOfFirstNumber = new BigInteger(GetDigitsFromHalf(midPoint, _reversedDigits.Length - 1));
        BigInteger lowDigitsOfFirstNumber = new BigInteger(GetDigitsFromHalf(0, midPoint - 1));
        BigInteger highDigitsOfSecondNumber = new BigInteger(multiplier.GetDigitsFromHalf(midPoint, multiplier._reversedDigits.Length - 1));
        BigInteger lowDigitsOfSecondNumber = new BigInteger(multiplier.GetDigitsFromHalf(0, midPoint - 1));

        // recursive steps
        BigInteger lowDigitsProduct = lowDigitsOfFirstNumber.Multiply(lowDigitsOfSecondNumber);
        BigInteger crossProduct = (lowDigitsOfFirstNumber.Add(highDigitsOfFirstNumber)).Multiply(lowDigitsOfSecondNumber.Add(highDigitsOfSecondNumber));
        BigInteger highDigitsProduct = highDigitsOfFirstNumber.Multiply(highDigitsOfSecondNumber);

        // combining the results
        return highDigitsProduct.ShiftLeft().Add((crossProduct.Subtract(lowDigitsProduct).Subtract(highDigitsProduct)).ShiftLeft()).Add(lowDigitsProduct);
    }
    */
    
    //public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    //public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    //public static BigInteger operator *(BigInteger a, BigInteger b) => a.Multiply(b);
}

class Program
{
    static void Main(string[] args)
    {
        BigInteger number5 = new BigInteger("-30");
        BigInteger number6 = new BigInteger("30");

        BigInteger sum = number5 + number6;
        BigInteger difference = number6 - number5;

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Difference: " + difference);
        
        BigInteger number7 = new BigInteger("123");
        BigInteger number8 = new BigInteger("-456");

        sum = number7 + number8;
        difference = number7 - number8;

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Difference: " + difference);
        
        BigInteger number9 = new BigInteger("-456");
        BigInteger number10 = new BigInteger("-98");

        sum = number9 + number10;
        difference = number9 - number10;

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Difference: " + difference);
    }
}
        
        /*Console.WriteLine("Enter the first number:");
        string firstNumber = Console.ReadLine();

        Console.WriteLine("Enter the second number:");
        string secondNumber = Console.ReadLine();

        BigInteger bigInteger1 = new BigInteger(firstNumber);
        BigInteger bigInteger2 = new BigInteger(secondNumber);

        BigInteger resultAdd = bigInteger1.Add(bigInteger2);
        BigInteger resultSub = bigInteger1.Subtract(bigInteger2);
        BigInteger resultMultiply = bigInteger1.Multiply(bigInteger2);

        Console.WriteLine("The result of the addition is: ");
        Console.WriteLine(resultAdd);

        Console.WriteLine("The result of the subtraction is: ");
        Console.WriteLine(resultSub);

        Console.WriteLine("The result of the multiplication is: ");
        Console.WriteLine(resultMultiply);
        */
    //}

