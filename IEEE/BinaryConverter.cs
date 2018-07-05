using System;

namespace IEEE
{
    public static class BinaryConverter
    {
        public static string ToBinaryString(int value, int length)
        {
            const int BASE = 2;
            var binary = new char[length];
            int i;
            for (i = length - 1; i >= 0; i--)
            {
                binary[i] = (value % BASE).ToChar();
                value >>= 1;

                if (value == 0)
                {
                    i--;
                    break;
                }
            }

            while (i >= 0)
            {
                binary[i] = '0';
                i--;
            }

            return new string(binary);
        }
        
        public static string ToBinaryString(double value, int length)
        {
            const int BASE = 2;
            var binary = new char[length];

            // TODO check for zero integer part.
            int integerPartLength = (int) Math.Log(value, 2) + 1;
            string integerPartBinary = ToBinaryString((int) value, integerPartLength);

            double fractionalPart = value.GetFractionalPart();

            int i = integerPartLength;
            integerPartBinary.CopyTo(0, binary, 0, integerPartLength);

            while (i < length && fractionalPart != 0)
            {
                fractionalPart *= BASE;
                
                int integerPart = (int) fractionalPart;
                binary[i] = integerPart.ToChar();
                
                if (integerPart == 1)
                {
                    fractionalPart -= 1;
                }
                
                i++;
            }

            i++;
            while (i < length)
            {
                binary[i] = '0';
                i++;
            }

            return new string(binary);
        }

        private static double GetFractionalPart(this double value) => value - (int) value;

        private static char ToChar(this int value)
        {
            const int OFFSET = 48;
            return (char)(value + OFFSET);
        }
    }
}