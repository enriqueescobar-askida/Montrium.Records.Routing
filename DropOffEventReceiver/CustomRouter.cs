// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomRouter.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   Defines the RecordRouter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    using System;
    using System.Collections;
    using System.IO;

    using Microsoft.Office.RecordsManagement.RecordsRepository;
    using Microsoft.SharePoint;

    using RecordsRepositoryProperty = Microsoft.SharePoint.RecordsRepositoryProperty;

    /// <summary>
    /// The record router.
    /// </summary>
    public class CustomRouter : ICustomRouter, IDisposable
    {
        #region fields
        /// <summary>the log.</summary>
        private string log = String.Empty;

        /// <summary>is Disposed.</summary>
        private bool isDisposed = false;
        #endregion

        #region DestructorDisposable
        /// <summary>
        /// Finalizes an instance of the <see cref="CustomRouter"/> class. 
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CustomRouter"/> is reclaimed by garbage collection.
        /// </summary>
        ~CustomRouter()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            this.Dispose(false);
        }

        #endregion

        #region AttributesOrProperties
        /// <summary>Gets the name of the assembly.</summary>
        public string AssemblyName { get; internal set; }

        /// <summary>Gets the name.</summary>
        public string Name { get; internal set; }

        /// <summary>Gets the name of the file.</summary>
        public string FileName { get; internal set; }

        /// <summary>Gets the file path.</summary>
        public string FilePath { get; internal set; }

        /// <summary>Gets the type of the content.</summary>
        public string ContentType { get; internal set; }

        /// <summary>Gets the sp user.</summary>
        public SPUser SpUser { get; internal set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Initializes the specified absolute site URL.
        /// </summary>
        /// <param name="absoluteSiteUrl">The absolute site URL.</param>
        /// <param name="recordFile">The record file.</param>
        /// <param name="contentTypeName">Name of the content type.</param>
        public void Initialize(string absoluteSiteUrl, SPFile recordFile, string contentTypeName)
        {
            if (String.IsNullOrEmpty(absoluteSiteUrl)) throw new ArgumentNullException("absoluteSiteUrl");
            if (recordFile == null) throw new ArgumentNullException("recordFile");
            if (String.IsNullOrEmpty(contentTypeName)) throw new ArgumentNullException("contentTypeName");

            string s = "--- " + this.GetType().Name + " 3@ ---";
            this.log += s + "\n";
            UnifiedLoggingServer.LogMedium(s);
            s = "-@1:" + recordFile.Name;
            this.log += s + "\n";
            UnifiedLoggingServer.LogMedium(s);
            s = "-@2:" + absoluteSiteUrl;
            this.log += s + "\n";
            UnifiedLoggingServer.LogMedium(s);
            s = "-@3:" + contentTypeName;
            this.log += s + "\n";
            UnifiedLoggingServer.LogMedium(s);

            this.AssemblyName = this.GetType().Assembly.FullName;
            this.log += "--- CR.Init:" + this.AssemblyName + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.AssemblyName);
            this.Name = this.GetType().Name;
            this.log += "--- CR.Init:" + this.Name + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.Name);
            this.FileName = recordFile.Name;
            this.log += "--- CR.Init:" + this.FileName + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.FileName);
            this.FilePath = absoluteSiteUrl + "/" + recordFile.Url;
            this.log += "--- CR.Init:" + this.FilePath + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.FilePath);
            this.ContentType = contentTypeName;
            this.log += "--- CR.Init:" + this.ContentType + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.ContentType);
            this.SpUser = new SPSite(absoluteSiteUrl).OpenWeb().CurrentUser;
            this.log += "--- CR.Init:" + this.SpUser.Name + "\n";
            UnifiedLoggingServer.LogMedium("--- CR.Init:" + this.SpUser.Name);
        }

        // CustomRouterResult ICustomRouter.OnSubmitFile(
        // public CustomRouterResult OnSubmitFile(
        /// <summary>
        /// The on submit file.
        /// </summary>
        /// <param name="contentOrganizerWeb">The content organizer web.</param>
        /// <param name="recordSeries">The record series.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="fileContent">The file content.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="finalFolder">The final folder.</param>
        /// <param name="resultDetails">The result details.</param>
        /// <returns>
        /// The Microsoft.Office.RecordsManagement.RecordsRepository.CustomRouterResult.
        /// </returns>
        public CustomRouterResult OnSubmitFile(
            EcmDocumentRoutingWeb contentOrganizerWeb,
            string recordSeries,
            string userName,
            Stream fileContent,
            RecordsRepositoryProperty[] properties,
            SPFolder finalFolder,
            ref string resultDetails)
        {
            if (contentOrganizerWeb == null) throw new ArgumentNullException("contentOrganizerWeb");

            // We should have a Content Organizer enabled web           
            if (!contentOrganizerWeb.IsRoutingEnabled) throw new ArgumentException("Invalid content organizer.");
            if (String.IsNullOrEmpty(recordSeries)) throw new ArgumentNullException("recordSeries");
            if (String.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName");
            if (fileContent.Length == 0) throw new ArgumentNullException("fileContent");
            if (properties == null) throw new ArgumentNullException("properties");
            if (!finalFolder.Exists) throw new ArgumentNullException("finalFolder");

            UnifiedLoggingServer.LogMedium("---- CR.OnSubmitFile() 7@");
            UnifiedLoggingServer.LogMedium("-@1:" + contentOrganizerWeb.GetType());
            UnifiedLoggingServer.LogMedium("-@2:" + recordSeries);
            UnifiedLoggingServer.LogMedium("-@3:" + userName);
            UnifiedLoggingServer.LogMedium("-@4:" + fileContent.Length);
            UnifiedLoggingServer.LogMedium("-@5:" + properties.Length);
            UnifiedLoggingServer.LogMedium("-@6:" + finalFolder.Name);
            UnifiedLoggingServer.LogMedium("-@7:" + resultDetails);

            try
            {
                foreach (RecordsRepositoryProperty recordsRepositoryProperty in properties)
                {
                    string s = "---- CR.Props [" + recordsRepositoryProperty.Name + "|"
                               + recordsRepositoryProperty.Value + "]";
                    this.log += s + "\n";
                    UnifiedLoggingServer.LogMedium(s);
                }

                // Create a Hashtable of properties which forms the metadata for the file
                Hashtable fileProperties = EcmDocumentRouter.GetHashtableForRecordsRepositoryProperties(properties, recordSeries);
                UnifiedLoggingServer.LogMedium("---- CR.OnSubmitFile().GetHashtableForRecordsRepositoryProperties:" + fileProperties.Count);
                resultDetails += this.log;
                UnifiedLoggingServer.LogMedium("-@FilNm_:" + this.FileName);
                UnifiedLoggingServer.LogMedium("-@FilPt_:" + this.FilePath);
                UnifiedLoggingServer.LogMedium("-@SPusr_:" + this.SpUser);

                // Save it to disk
                EcmDocumentRouter.SaveFileToFinalLocation(
                    contentOrganizerWeb,
                    finalFolder,
                    fileContent,
                    this.FileName,
                    this.FilePath,
                    fileProperties, 
                    this.SpUser,
                    true,
                    "CustomRouter CheckInComment");
            }
            catch (Exception e)
            {
                UnifiedLoggingServer.LogHigh("---- CR:TryCatch savefiletofinallocation" + e.Message);
                return CustomRouterResult.SuccessCancelFurtherProcessing;
            }

            return CustomRouterResult.SuccessContinueProcessing;
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
                    this.AssemblyName = this.Name = this.FileName = this.FilePath = this.ContentType = String.Empty;
                    this.SpUser = null;
                }

                // unmanaged resources clean

                // confirm cleaning
                this.isDisposed = true;
            }
        }
        #endregion
    }
}