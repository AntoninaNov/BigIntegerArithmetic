public class BigInteger
{
    private int[] _digitArray;
    private bool _isNegativeNumber;

    public BigInteger(string numberAsString)
    {
        // Convert here string representation to inner int array IN REVERSED ORDER
    }

    public BigInteger Add(BigInteger otherNumber)
    {
        // return new BigInteger, result of current + another
    }

    public BigInteger Subtract(BigInteger otherNumber)
    {
        // return new BigInteger, result of current - another
    }

    public BigInteger Multiply(BigInteger otherNumber)
    {
        // Karatsuba's multiplication algorithm
    }
    
    public override string ToString()
    {
        // Convert array back to string and return it
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Read user input, perform arithmetic operation, output result
    }
}
