// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnifiedLoggingServer.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   The unified logger service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Montrium.RecordsRouter
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint.Administration;

    /// <summary>
    /// The unified logger service.
    /// </summary>
    [CLSCompliant(false), System.Runtime.InteropServices.GuidAttribute("F81575E1-E59B-4C28-95CC-433C71482920")]
    public class UnifiedLoggingServer : SPDiagnosticsServiceBase
    {
        #region AttributesOrProperties
        /// <summary>The service name.</summary>
        private static string ServiceName = "Montrium Logging Service";

        /// <summary>The area name.</summary>
        private static string AreaName = "Montrium Solutions";

        /// <summary>The category.</summary>
        private static string Category = "Montrium RUBi_Methods";

        /// <summary>The event id.</summary>
        private static int EventId = 9191;

        /// <summary>Gets the local.</summary>
        public static UnifiedLoggingServer Local
        {
            get
            {
                return SPFarm.Local.Services.GetValue<UnifiedLoggingServer>(ServiceName); // .GetValue(ServiceName);
            }
        }

        /// <summary>Gets The current.</summary>
        private static UnifiedLoggingServer current;

        /// <summary>Gets the current.</summary>
        public static UnifiedLoggingServer Current
        {
            get
            {
                return current ?? (current = new UnifiedLoggingServer());
            }
        }
        #endregion

        #region Constructors
        /// <summary>Prevents a default instance of the <see cref="UnifiedLoggingServer"/> class from being created.</summary>
        private UnifiedLoggingServer()
            : base(ServiceName, SPFarm.Local)
        {
        }
        #endregion

        #region Destructor
        #endregion

        #region PublicMethods
        /// <summary>Writes a High level log message to the SharePoint ULS only.</summary>
        /// <param name="message">The message.</param>
        public static void LogHigh(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            WriteTraceLog(TraceSeverity.High, message);
            
        }

        /// <summary>Writes a Medium level log message to the SharePoint ULS only.</summary>
        /// <param name="message">The message.</param>
        public static void LogMedium(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            WriteTraceLog(TraceSeverity.Medium, message);
            WriteEventLog(Category, EventSeverity.Information, message);
        }

        /// <summary>Writes a Low level log message to the SharePoint ULS only (i.e. Most Verbose, or most detailed).</summary>
        /// <param name="message">The message.</param>
        public static void LogLow(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            WriteTraceLog(TraceSeverity.Verbose, message);
        }

        /// <summary>
        /// Unexpecteds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Unexpected(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            WriteTraceLog(TraceSeverity.Unexpected, message);
        }
        #endregion

        #region ProtectedOverrideMethods
        /// <summary>
        /// The provide areas.
        /// </summary>
        /// <returns>
        /// The System.Collections.Generic.IEnumerable`1[T -&gt; Microsoft.SharePoint.Administration.SPDiagnosticsArea].
        /// </returns>
        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            // provide the category with default severities 
            List<SPDiagnosticsCategory> categories = new List<SPDiagnosticsCategory>
                {
                    new SPDiagnosticsCategory(Category, TraceSeverity.Medium, EventSeverity.Information) 
                };

            yield return new SPDiagnosticsArea(AreaName, 0, 0, false, categories);
        }

        #endregion

        #region PrivateMethods
        /// <summary>
        /// The write trace log.
        /// </summary>
        /// <param name="traceSeverity">
        /// The trace severity.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void WriteTraceLog(TraceSeverity traceSeverity, string message)
        {
            if (traceSeverity != TraceSeverity.None)
            {
                try
                {
                    SPDiagnosticsCategory spDiagnosticsCategory =
                        UnifiedLoggingServer.Current.Areas[AreaName].Categories[Category];
                    UnifiedLoggingServer.Current.WriteTrace((uint)EventId, spDiagnosticsCategory, traceSeverity, message);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// The write event log.
        /// </summary>
        /// <param name="categoryName">
        /// The category name.
        /// </param>
        /// <param name="eventSeverity">
        /// The event severity.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void WriteEventLog(string categoryName, EventSeverity eventSeverity, string message)
        {
            try
            {
                UnifiedLoggingServer localService = Local;

                if (localService != null)
                {
                    SPDiagnosticsCategory category = localService.Areas[AreaName].Categories[categoryName];
                    localService.WriteEvent(1, category, eventSeverity, message);
                }
            }
            catch
            {   
            }
        }
        #endregion
    }
}