
static double Power(double baseNumber, int exponent)
{
    if (exponent == 0)
    
        return 1;
    

   return baseNumber * Power(baseNumber, exponent - 1); // 32
}

Console.WriteLine(Power(2, 5));