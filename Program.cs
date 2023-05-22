public class BigInteger
{
    private bool _isNegativeNumber;
    private int[] _reversedDigits;

    public BigInteger(string numericString)
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

    public override string ToString()
    {
        char[] chars = new char[_reversedDigits.Length];
        for (int index = 0; index < _reversedDigits.Length; index++)
        {
            chars[_reversedDigits.Length - index - 1] = (char)('0' + _reversedDigits[index]);
        }
        return new string(chars);
    }

    private int[] GetDigitsFromHalf(int startIndex, int endIndex)
    {
        int[] halfDigits = new int[endIndex - startIndex + 1];
        Array.Copy(_reversedDigits, startIndex, halfDigits, 0, endIndex - startIndex + 1);
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
    
    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    public static BigInteger operator *(BigInteger a, BigInteger b) => a.Multiply(b);
}

class Program
{
    static void Main(string[] args)
    {
        var x = new BigInteger("1313234242425");
        Console.WriteLine(x);
        
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
    }
}
