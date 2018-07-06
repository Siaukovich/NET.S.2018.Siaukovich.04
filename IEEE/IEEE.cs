namespace IEEE
{
    using System;
    using System.Runtime.InteropServices;

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
            var union = new Union { doubleValue = value };

            return ToIeeeString(union);
        }

        /// <summary>
        /// Converts double part if passed union to string if bits.
        /// </summary>
        /// <param name="union">
        /// The union.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Binary representation of passed union's double field.
        /// </returns>
        private static string ToIeeeString(Union union)
        {
            const int DOUBLE_LENGTH = 64; // 8 bytes = 64 bits.
            var binary = new char[DOUBLE_LENGTH];
            for (int i = 0; i < DOUBLE_LENGTH; i++)
            {
                long mask = 1L << i;

                binary[binary.Length - 1 - i] = (union.longValue & mask) != 0 ? '1' : '0';
            }

            return new string(binary);
        }

        #endregion

        #region Private

        /// <summary>
        /// Holds double and long fields in the same memory chunks,
        /// which allows to use bitwise operation on double-type field.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            [FieldOffset(0)]
            public double doubleValue;

            [FieldOffset(0)]
            public long longValue;
        }

        #endregion

    }

}
