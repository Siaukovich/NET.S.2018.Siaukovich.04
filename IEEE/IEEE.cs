namespace IEEE
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

            // TODO: Almost everything.
            return null;
        }

        private static bool IsSpecialValue(this double value, out string result)
        {
            switch (value)
            {
                case double.NaN:
                    result = "1111111111111000000000000000000000000000000000000000000000000000";
                    return true;
                    
                case double.PositiveInfinity:
                    result = "0111111111110000000000000000000000000000000000000000000000000000";
                    return true;

                case double.NegativeInfinity:
                    result = "1111111111110000000000000000000000000000000000000000000000000000";
                    return true;
            }

            if (value != 0)
            {
                result = null;
                return false;
            }

            if (IsNegative(value))
            {
                result = "1000000000000000000000000000000000000000000000000000000000000000";
                return true;
            }

            // If positive zero.
            result = "0000000000000000000000000000000000000000000000000000000000000000";
            return true;
        }

        /// Checks if value is negative.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsNegative(double value) => 1 / value < 0;
    }
}
