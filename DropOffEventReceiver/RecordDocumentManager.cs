// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordDocumentManager.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   The record document manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    /// <summary>
    /// Manages the records
    /// </summary>
    public class RecordDocumentManager : IDisposable, ITraceable
    {
        #region fields
        /// <summary>is Disposed.</summary>
        private bool isDisposed = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordDocumentManager"/> class.
        /// </summary>
        /// <param name="dropOffListItem">The drop off library record.</param>
        /// <param name="enabledLibraries">The SP web lists.</param>
        /// <param name="routingRules">The routing rules.</param>
        public RecordDocumentManager(
            SPListItem dropOffListItem, List<SPList> enabledLibraries, SPListItemCollection routingRules)
        {
            if (dropOffListItem == null) throw new ArgumentNullException("dropOffListItem");
            if (enabledLibraries == null) throw new ArgumentException("enabledLibraries");
            if (routingRules == null) throw new ArgumentNullException("routingRules");

            UnifiedLoggingServer.LogMedium("--- " + this.GetType().Name + " 3@ ---");
            UnifiedLoggingServer.LogMedium("-@1:" + dropOffListItem.Name);
            UnifiedLoggingServer.LogMedium("-@2:" + enabledLibraries.Count);
            UnifiedLoggingServer.LogMedium("-@3:" + routingRules.Count);
            this.RecordDocument = new RecordDocument(enabledLibraries, dropOffListItem, routingRules);
        }

        #endregion

        #region DestructorDisposable
        /// <summary>
        /// Finalizes an instance of the <see cref="RecordDocumentManager"/> class. 
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="RecordDocumentManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~RecordDocumentManager()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            this.Dispose(false);
        }
        #endregion

        #region AttributesOrProperties
        /// <summary>Gets the record document.</summary>
        public RecordDocument RecordDocument { get; internal set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Scans the fields.
        /// </summary>
        /// <param name="dropOffLibrary">The drop off library.</param>
        /// <param name="dropOffLibraryUrl">The drop off library URL.</param>
        public void ScanFields(SPList dropOffLibrary, string dropOffLibraryUrl)
        {
            UnifiedLoggingServer.LogMedium("--- " + this.GetType().Name + ".ScanFields 2@ ---");
            UnifiedLoggingServer.LogMedium("-@1:" + dropOffLibrary.Title);
            UnifiedLoggingServer.LogMedium("-@2:" + dropOffLibraryUrl);

            // rd.SpListItem, rlm.DropOffLibrary, rlm.DropOffLibUrl, rd.XmlProperties
            RecordFieldManager rfm = new RecordFieldManager(
                this.RecordDocument.ListItem, dropOffLibrary, dropOffLibraryUrl, this.RecordDocument.XmlProperties, false);
            rfm.TraceLog("TraceLog_RFM_");
        }

        /// <summary>
        /// Moves the file to library.
        /// </summary>
        public void MoveFileToLibrary()
        {
            string newUrl;
            SPListItem routingRule;
            SPList newLib;

            if (this.RecordDocument.HasLibrary)
            {
                // child level library
                routingRule = this.RecordDocument.RoutingRule;
                newUrl = routingRule.Web.Url + "/";
                newLib = this.RecordDocument.CandidateLibrary;

                if (this.RecordDocument.HasRoutingRule)
                {
                    // child level library with rule
                    if (routingRule["Target Folder"] == null)
                    {
                        // child level library with rule without folder
                        newUrl += routingRule["Target Library"] + "/";
                        this.RecordDocument.MoveToLibraryWithoutFolder(newUrl);
                    }
                    else
                    {
                        // child level library with rule with folder
                        SPFolder newSpFolder = newLib.Folders[0].Folder;
                        newUrl = newSpFolder.Url;
                        this.RecordDocument.MoveToLibraryWithFolder(newSpFolder);
                    }
                }
                else
                {
                    // parent level library only - without rule
                    newUrl = this.RecordDocument.CandidateLibrary.ParentWebUrl + "/"
                                + this.RecordDocument.CandidateLibrary.Title;
                }
            }
            else if (this.RecordDocument.HasParentLibrary)
            {
                // parent level library
                routingRule = this.RecordDocument.ParentRoutingRule;
                newUrl = routingRule.Web.Url + "/";
                newLib = this.RecordDocument.ParentCandidateLibrary;

                if (this.RecordDocument.HasParentRoutingRule)
                {
                    // parent level library with rule
                    if (routingRule["Target Folder"] == null)
                    {
                        // parent level library with rule without folder
                        newUrl += routingRule["Target Library"] + "/";
                        this.RecordDocument.MoveToLibraryWithoutFolder(newUrl);
                    }
                    else
                    {
                        // parent level library with rule with folder
                        SPFolder newSpFolder = newLib.Folders[0].Folder;
                        newUrl = newSpFolder.Url;
                        this.RecordDocument.MoveToLibraryWithFolder(newSpFolder);
                    }
                }
                else
                {
                    // parent level library only - without rule
                    newUrl = this.RecordDocument.ParentCandidateLibrary.ParentWebUrl + "/"
                                + this.RecordDocument.ParentCandidateLibrary.Title;
                }
            }
                
            // unknown level library continue;
            // if (newLib != null) newLib.Update();
        }

        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="header">The trace log header.</param>
        public void TraceLog(string header)
        {
            UnifiedLoggingServer.LogMedium(header + "Name_:" + this.RecordDocument.File.Name);
            UnifiedLoggingServer.LogMedium(header + "Titl_:" + this.RecordDocument.Title);
            UnifiedLoggingServer.LogMedium(header + "DNam_:" + this.RecordDocument.ListItem.DisplayName);
        }
        #endregion

        #region PublicOverride
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "FileTitle:\t\t" + this.RecordDocument.Title + "\n\n" + this.RecordDocument;
        }
        #endregion

        #region PublicDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region PrivateMethods
        #endregion

        #region PrivateDisposable
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool isDisposing)
        {
            // Check if Dispose has been called
            if (!this.isDisposed)
            {
                // dispose managed and unmanaged resources
                if (isDisposing)
                {
                    // managed resources clean
                    this.RecordDocument = null;
                }

                // unmanaged resources clean

                // confirm cleaning
                this.isDisposed = true;
            }
        }
        #endregion
    }
}