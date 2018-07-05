﻿namespace IEEE
{
    using System;

    public static class IEEE
    {
        public static string DoubleToStringBits(this double value)
        {
            if (value.IsSpecialValue(out string result))
            {
                return result;
            }

            string sign = value.IsNegative() ? "1" : "0";

            string absoluteValue = value.ToNormalizedBinary();

            return sign + absoluteValue;
        }

        private static bool IsSpecialValue(this double value, out string result)
        {
            if (double.IsNaN(value))
            {
                result = "1111111111111000000000000000000000000000000000000000000000000000";
                return true;
            }

            if (double.IsPositiveInfinity(value))
            {
                result = "0111111111110000000000000000000000000000000000000000000000000000";
                return true;
            }

            if (double.IsNegativeInfinity(value))
            {
                result = "1111111111110000000000000000000000000000000000000000000000000000";
                return true;
            }

            if (value != 0)
            {
                result = null;
                return false;
            }

            if (double.IsNegativeInfinity(1 / value))
            {
                result = "1000000000000000000000000000000000000000000000000000000000000000";
                return true;
            }

            // If positive zero.
            result = "0000000000000000000000000000000000000000000000000000000000000000";
            return true;
        }

        private static string ToNormalizedBinary(this double value)
        {
            int exponentDecimalBase = value.GetExponent();

            const int EXPONENT_LENGTH = 11;
            string exponentBinaryString = BinaryConverter.ToBinaryString(exponentDecimalBase, EXPONENT_LENGTH);

            const int MANTISS_LENGTH = 52;
            string mantissBinaryString = BinaryConverter.ToBinaryString(value, MANTISS_LENGTH);

            return string.Join(string.Empty, exponentBinaryString, mantissBinaryString);
        }

        private static int GetExponent(this double value)
        {
            const int SHIFT = 1023;
            return (int)Math.Log(value, 2) + SHIFT;
        }

        private static double GetMantiss(this double value)
        {
            int integerPart = (int)value;
            double fractionalPart = value - integerPart;

            
            // TODO
            return 0;
        }

        /// Checks if value is negative.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsNegative(this double value) => value < 0;
    }
}
