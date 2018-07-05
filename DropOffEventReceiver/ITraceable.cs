// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITraceable.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   Interface Traceable
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    /// <summary>
    /// Interface Traceable
    /// </summary>
    public interface ITraceable
    {
        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="header">The header.</param>
        void TraceLog(string header);
    }
}