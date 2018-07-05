namespace IEEE
{
    using System;

    /// <summary>
    /// Class that converts values to its binary representation by IEEE 754 standard.
    /// </summary>
    public static class IEEE
    {
        #region Public

        /// <summary>
        /// Converts double to corresponding binary representation.
        /// </summary>
        /// <param name="value">
        /// Value that will be converted.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Binary representation.
        /// </returns>
        public static string DoubleToStringBits(this double value)
        {
            if (value.IsSpecialValue(out string result))
            {
                return result;
            }

            string sign = value.IsNegative() ? "1" : "0";

            value = Math.Abs(value);

            string absoluteValue = value.ToNormalizedBinary();

            return sign + absoluteValue;
        }

        #endregion

        #region Private

        /// <summary>
        /// Checks if the value is special value, 
        /// such as NaN, positive or negative infinity or zero.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="result">
        /// Stores special value binary representation.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// Return true if value is special.
        /// </returns>
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

        /// <summary>
        /// Calculates value exponent and mantiss.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ToNormalizedBinary(this double value)
        {
            int exponentDecimalBase = value.GetExponent();

            const int EXPONENT_LENGTH = 11;
            string exponentBinaryString = BinaryConverter.ToBinaryString(exponentDecimalBase, EXPONENT_LENGTH);

            const int MANTISS_LENGTH = 52;
            string mantissBinaryString = BinaryConverter.ToBinaryString(value, MANTISS_LENGTH + 1).Substring(1); // Omit first one.

            return string.Join(string.Empty, exponentBinaryString, mantissBinaryString);
        }

        /// <summary>
        /// Calculates the exponent by the ieee 754 rules.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// Shifted exponent.
        /// </returns>
        private static int GetExponent(this double value)
        {
            const int SHIFT = 1023;
            return (int)Math.Floor(Math.Log(value, 2)) + SHIFT;
        }

        /// <summary>
        /// Checks if value is negative.
        /// </summary>
        /// <param name="value">
        /// Value that needs to be checked.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// True if negative.
        /// </returns>
        private static bool IsNegative(this double value) => value < 0;
    }

    #endregion
}
