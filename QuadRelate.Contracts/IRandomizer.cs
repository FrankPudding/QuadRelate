namespace QuadRelate.Contracts
{
    public interface IRandomizer
    {
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue</returns>
        int Next(int maxValue);
    }
}