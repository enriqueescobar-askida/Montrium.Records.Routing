// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordCentreManager.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   Defines the RecordCentreManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    /// <summary>
    /// Manages The Record Center
    /// </summary>
    public class RecordCentreManager : IDisposable, ITraceable
    {
        #region fields
        /// <summary>is Disposed.</summary>
        private bool isDisposed = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordCentreManager"/> class.
        /// </summary>
        /// <param name="dropOffListItem">The SP List Item corresponding the one in the Drop Off Library.</param>
        /// <param name="url">The URL.</param>
        public RecordCentreManager(SPListItem dropOffListItem, string url)
        {
            if (dropOffListItem == null) throw new ArgumentNullException("dropOffListItem");
            if (String.IsNullOrEmpty(url)) throw new ArgumentException("url");

            UnifiedLoggingServer.LogMedium("--- " + this.GetType().Name + " 2@ ---");
            UnifiedLoggingServer.LogMedium("-@1:" + dropOffListItem.Name);
            UnifiedLoggingServer.LogMedium("-@2:" + url);

            using (RecordLibraryManager rlm = new RecordLibraryManager(url))
            using (RoutingRulesManager rrm = new RoutingRulesManager(url))
            {
                rlm.TraceLog("TraceLog_RLM_");
                rrm.TraceLog("TraceLog_RRM_");
                
                this.EnabledLibraries = rlm.EnabledLibraries;

                this.DropOffLibrary = rlm.DropOffLibrary;
                this.RoutingRules = rrm.RoutingRules;
                
                using (RecordDocumentManager rdm =
                    new RecordDocumentManager(
                    dropOffListItem, rlm.EnabledLibraries, rrm.RoutingRules))
                {
                    this.DropOffRecordDocument = rdm.RecordDocument;

                    rdm.ScanFields(rlm.DropOffLibrary, rlm.DropOffLibUrl);
                    /* trying to find a better ways:
                     * adding rules to rule list
                     * adding custom router implementing IRouter
                     * instead of forcing the copy
                    rdm.MoveFileToLibrary();*/
                    rdm.TraceLog("TraceLog_RDM_");
                    this.DropOffRecordDocument.TraceLog("TraceLog_RD_");

                    using (CustomRouterManager crm = new CustomRouterManager(url, this.DropOffRecordDocument))
                    {
                        crm.CreateRouter(
                        this.DropOffRecordDocument.Author,
                        this.DropOffRecordDocument.File.OpenBinaryStream(SPOpenBinaryOptions.Unprotected));
                        crm.TraceLog("TraceLog_CRM_");
                    }
                }
            }
        }
        #endregion

        #region DestructorDisposable
        /// <summary>
        /// Finalizes an instance of the <see cref="RecordCentreManager"/> class. 
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="RecordCentreManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~RecordCentreManager()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            this.Dispose(false);
        }
        #endregion

        #region AttibutesOrProperties
        /// <summary>Gets the drop off library.</summary>
        public SPList DropOffLibrary { get; internal set; }

        /// <summary>Gets the enabled libraries.</summary>
        public List<SPList> EnabledLibraries { get; internal set; }

        /// <summary>Gets the routing rules.</summary>
        public SPListItemCollection RoutingRules { get; internal set; }

        /// <summary>Gets the drop off record document.</summary>
        public RecordDocument DropOffRecordDocument { get; internal set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="header">The tracelog header.</param>
        public void TraceLog(string header)
        {
            UnifiedLoggingServer.LogMedium(header + "DOLTitle_:" + this.DropOffLibrary.Title);
            UnifiedLoggingServer.LogMedium(header + "LibCount_:" + this.EnabledLibraries.Count);
            UnifiedLoggingServer.LogMedium(header + "RulCount_:" + this.RoutingRules.Count);
            UnifiedLoggingServer.LogMedium(header + "DORDTitl_:" + this.DropOffRecordDocument.Title);
        }
        #endregion

        #region PublicOverride
        #endregion

        #region PublicDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region PrivateMethods
        #endregion

        #region PrivateDisposable
        /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// <param name="isDisposing">The is disposing.</param>
        private void Dispose(bool isDisposing)
        {
            // Check if Dispose has been called
            if (!this.isDisposed)
            {
                // dispose managed and unmanaged resources
                if (isDisposing)
                {
                    // managed resources clean
                    this.DropOffLibrary = null;
                    this.EnabledLibraries = null;
                    this.RoutingRules = null;
                    this.DropOffRecordDocument = null;
                }

                // unmanaged resources clean

                // confirm cleaning
                this.isDisposed = true;
            }
        }
        #endregion
    }
}