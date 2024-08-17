namespace JitterGang
{
    /// <summary>
    /// Defines the interface for handling controller input.
    /// </summary>
    public interface IControllerHandler : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the designated button is currently pressed.
        /// </summary>
        bool IsButtonPressed { get; }

        /// <summary>
        /// Starts polling the controller for input.
        /// </summary>
        void StartPolling();

        /// <summary>
        /// Stops polling the controller for input.
        /// </summary>
        void StopPolling();
    }
}
