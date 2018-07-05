namespace IEEE
{
    using System;
    using System.Diagnostics.PerformanceData;
    using System.Net.NetworkInformation;
    using System.Text;

    public static class IEEE
    {
        public static string DoubleToStringBits(this double value)
        {
            if (value.IsSpecialValue(out string result))
            {
                return result;
            }

            char sign = value.IsNegative() ? '1' : '0';

            (string exponent, string mantiss) absoluteValue = value.ToNormalizedBinary();

            string answer = JoinPartsToString(sign, absoluteValue.exponent, absoluteValue.mantiss);

            return answer;
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

        private static (string exponent, string mantiss) ToNormalizedBinary(this double value)
        {
            int exponentDecimalBase = value.GetExponent();
            double mantissDecimalBase = value.GetMantiss();

            string exponentBinaryString = ExponentToBinaryString(exponentDecimalBase);
            string mantissBinaryString = MantissToBinaryString(mantissDecimalBase);

            return (exponentBinaryString, mantissBinaryString);
        }

        private static int GetExponent(this double value)
        {
            const int SHIFT = 1024;
            return (int)Math.Log(value, 2) + SHIFT;
        }

        private static double GetMantiss(this double value)
        {
            int integerPart = (int)value;
            double fractionalPart = value - integerPart;
        }

        private static string ExponentToBinaryString(int value)
        {
            const int BASE = 2;
            const int LENGTH = 11;
            var binary = new char[LENGTH];
            int i;
            for (i = LENGTH - 1; i >= 0; i--)
            {
                binary[i] = (value % BASE).ToChar();
                value >>= 1;

                if (value == 0)
                {
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

        private static string JoinPartsToString(char sign, string exponent, string mantiss)
        {
            string singStr = sign.ToString();

            return string.Join(string.Empty, singStr, exponent, mantiss);
        }

        private static char ToChar(this int value)
        {
            const int OFFSET = 48;
            return (char)(value + OFFSET);
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
