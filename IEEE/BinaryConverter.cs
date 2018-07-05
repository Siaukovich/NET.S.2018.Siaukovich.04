using System;

namespace IEEE
{
    /// <summary>
    /// Class that converts values to its binary representation.
    /// </summary>
    public static class BinaryConverter
    {
        /// <summary>
        /// Converts given int value to corresponding binary value.
        /// </summary>
        /// <param name="value">
        /// Value that needs to be converted.
        /// </param>
        /// <param name="length">
        /// Length of result string. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Binary representation of passed int.
        /// </returns>
        public static string ToBinaryString(int value, int length)
        {
            const int BASE = 2;
            var binary = new char[length];
            int i;
            for (i = length - 1; i >= 0; i--)
            {
                binary[i] = Math.Abs(value % BASE).ToChar();
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

        /// <summary>
        /// Converts given double value to corresponding binary value.
        /// </summary>
        /// <param name="value">
        /// Value that needs to be converted.
        /// </param>
        /// <param name="length">
        /// Length of result string. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Binary representation of passed double.
        /// </returns>
        public static string ToBinaryString(double value, int length)
        {
            const int BASE = 2;
            var binary = new char[length];

            // TODO check for zero integer part.
            int integerPartLength = (int) Math.Log(value, 2) + 1;
            if (integerPartLength < 0)
            {
                integerPartLength = 0;
            }
            if (integerPartLength > length)
            {
                integerPartLength = length;
            }

            string integerPartBinary = IntegerPartToBinaryString(value, integerPartLength);

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
                    fractionalPart--;
                }
                
                i++;
            }

            while (i < length)
            {
                binary[i] = '0';
                i++;
            }

            return new string(binary);
        }

        /// <summary>
        /// Converts integer part of given double value to corresponding binary value.
        /// </summary>
        /// <param name="value">
        /// Value that needs to be converted.
        /// </param>
        /// <param name="length">
        /// Length of result string. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Binary representation of the integer part of passed double.
        /// </returns>
        public static string IntegerPartToBinaryString(double value, int length)
        {
            const int BASE = 2;
            value = Math.Floor(value);
            var binary = new char[length];
            int i;
            for (i = length - 1; i >= 0; i--)
            {
                binary[i] = Math.Abs((int)Math.IEEERemainder(value, BASE)).ToChar();
                value /= 2;
                value = Math.Floor(value);

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

        private static double GetFractionalPart(this double value) => value - Math.Floor(value);

        private static char ToChar(this int value)
        {
            const int OFFSET = 48;
            return (char)(value + OFFSET);
        }
    }
}