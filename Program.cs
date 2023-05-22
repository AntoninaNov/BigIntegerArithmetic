using System;
using System.Text;

public class BigInteger
{
    private bool _isNegativeNumber;
    private int[] _digitArray;
    
    public BigInteger(string numberAsString)
    {
        // Convert string representation to char array and reverse it
        char[] charArray = numberAsString.ToCharArray();
        Array.Reverse(charArray);

        // Initialize _digitArray with the same length as the string
        _digitArray = new int[charArray.Length];

        // Convert each character to an integer and store it in _digitArray
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
        // Determine the longer and shorter number
        BigInteger longerNumber = number1;
        BigInteger shorterNumber = number2;

        if (number2._digitArray.Length > number1._digitArray.Length)
        {
            longerNumber = number2;
            shorterNumber = number1;
        }

        int maxLength = Math.Max(number1._digitArray.Length, number2._digitArray.Length);
        int minLength = Math.Min(number1._digitArray.Length, number2._digitArray.Length);

        int[] result = new int[maxLength + 1]; // Additional space for carry

        int carry = 0;
        int sum;

        for (int i = 0; i < maxLength; i++)
        {
            int digit1 = i < longerNumber._digitArray.Length ? longerNumber._digitArray[i] : 0;
            int digit2 = i < shorterNumber._digitArray.Length ? shorterNumber._digitArray[i] : 0;

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
    
    public static BigInteger operator -(BigInteger number1, BigInteger number2)
    {
        int maxLength = Math.Max(number1._digitArray.Length, number2._digitArray.Length);
        int[] result = new int[maxLength];
        int borrow = 0;

        for (int i = 0; i < maxLength; i++)
        {
            int digit1 = i < number1._digitArray.Length ? number1._digitArray[i] : 0;
            int digit2 = i < number2._digitArray.Length ? number2._digitArray[i] : 0;

            int difference = digit1 - borrow - digit2;

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
    
    public override string ToString()
    {
        // Convert _digitArray to string in forward order and return it
        StringBuilder sb = new StringBuilder(_digitArray.Length);

        for (int i = _digitArray.Length - 1; i >= 0; i--)
        {
            sb.Append(_digitArray[i]);
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
        //var x = new BigInteger("1313234242425");
        //Console.WriteLine(x);
        
        // Checking for the string "123434"
        BigInteger number1 = new BigInteger("123434");
        Console.WriteLine(number1.ToString());

        // Checking for the string "9876543210"
        BigInteger number2 = new BigInteger("9876543210");
        Console.WriteLine(number2.ToString());

        // Checking for the string "0"
        BigInteger number3 = new BigInteger("0");
        Console.WriteLine(number3.ToString());

        // Checking for an empty string
        BigInteger number4 = new BigInteger("");
        Console.WriteLine(number4.ToString());
        
        // Addition check
        BigInteger number5 = new BigInteger("12");
        BigInteger number6 = new BigInteger("67");

        BigInteger sum = number5 + number6;
        BigInteger difference = number6 - number5;

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Difference: " + difference);
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
}
