namespace QuadRelate.Contracts
{
    public interface IMessageWriter
    {
        /// <summary>
        /// Output message text plus a newline.
        /// </summary>
        /// <param name="message">Text to output</param>
        void WriteMessage(string message);

        /// <summary>
        /// Output message text (with no newline).
        /// </summary>
        /// <param name="message">Text to output</param>
        void Write(string message);
    }
}