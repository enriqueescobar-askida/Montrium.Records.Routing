// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomRouterManager.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   The custom router manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Office.RecordsManagement.RecordsRepository;
    using Microsoft.SharePoint;

    using RecordsRepositoryProperty = Microsoft.SharePoint.RecordsRepositoryProperty;

    /// <summary>
    /// The custom router manager.
    /// </summary>
    public class CustomRouterManager : IDisposable, ITraceable
    {
        #region fields
        /// <summary>is Disposed.</summary>
        private bool isDisposed = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRouterManager"/> class.
        /// The custom router manager.
        /// </summary>
        /// <param name="absoluteSiteUrl">The absolute site URL.</param>
        /// <param name="rd">The rd.</param>
        public CustomRouterManager(string absoluteSiteUrl, RecordDocument rd)
        {
            if (String.IsNullOrEmpty(absoluteSiteUrl)) throw new ArgumentNullException("absoluteSiteUrl");
            if (rd == null) throw new ArgumentNullException("rd");

            UnifiedLoggingServer.LogMedium("--- " + this.GetType().Name + " 2@ ---");
            UnifiedLoggingServer.LogMedium("-@1:" + absoluteSiteUrl);
            UnifiedLoggingServer.LogMedium("-@2:" + rd.File.Name);
            this.AbsoluteSiteUrl = absoluteSiteUrl;
            this.ContentTypeName = rd.ContentType.Name;
            this.RouterName = rd.ContentType.Name + "CustomRouter";
            this.RecordsRepositoryProperties = this.FetchProperties(rd.FieldCollection, rd.ListItem);
            this.RouterRule = this.FetchRule(rd);
            this.RouterPath = this.RouterRule.Web.Url + "/";
            this.RouterLibrary = this.FetchLibrary(rd);
            this.HasRouter = this.CanFindRouter(rd);
            this.HasTargetFolder = this.RouterRule["Target Folder"] != null;
            this.RouterFolder = this.FetchFolder();
            this.RouterPath = this.FetchPath(this.RouterPath);
            this.CustomRouter = new CustomRouter();
            this.CustomRouter.Initialize(absoluteSiteUrl, rd.File, rd.ContentType.Name);
            this.AssemblyName = this.CustomRouter.AssemblyName;
            this.ClassName = this.GetType().Namespace + "." + this.CustomRouter.Name;
        }
        #endregion

        #region DestructorDisposable
        /// <summary>
        /// Finalizes an instance of the <see cref="CustomRouterManager"/> class.
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CustomRouterManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~CustomRouterManager()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            this.Dispose(false);
        }
        #endregion

        #region AttibutesOrProperties
        /// <summary>Gets the absolute site URL.</summary>
        public string AbsoluteSiteUrl { get; internal set; }

        /// <summary>Gets the name of the router.</summary>
        public string RouterName { get; internal set; }

        /// <summary>Gets the name of the content type.</summary>
        public string ContentTypeName { get; internal set; }

        /// <summary>Gets the name of the assembly.</summary>
        public string AssemblyName { get; internal set; }

        /// <summary>Gets the name of the class.</summary>
        public string ClassName { get; internal set; }

        /// <summary>Gets the router path.</summary>
        public string RouterPath { get; internal set; }

        /// <summary>Gets the records repository properties.</summary>
        public RecordsRepositoryProperty[] RecordsRepositoryProperties { get; internal set; }

        /// <summary>Gets the router rule.</summary>
        public SPListItem RouterRule { get; internal set; }

        /// <summary>Gets the router library.</summary>
        public SPList RouterLibrary { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has router.
        /// </summary>
        /// <value><c>true</c> if this instance has router; otherwise, <c>false</c>.</value>
        public bool HasRouter { get; internal set; }

        /// <summary>Gets a value indicating whether this instance has target folder.</summary>
        /// <value><c>true</c> if this instance has target folder; otherwise, <c>false</c>.</value>
        public bool HasTargetFolder { get; internal set; }

        /// <summary>Gets the router folder.</summary>
        public SPFolder RouterFolder { get; internal set; }

        /// <summary>Gets the custom router.</summary>
        public CustomRouter CustomRouter { get; internal set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Creates the router.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <param name="openBinaryStream">The open binary stream.</param>
        public void CreateRouter(string author, Stream openBinaryStream)
        {
            using (SPSite spSite = new SPSite(this.AbsoluteSiteUrl))
            using (SPWeb spWeb = spSite.OpenWeb())
            {
                EcmDocumentRoutingWeb ecmDocumentRoutingWeb = new EcmDocumentRoutingWeb(spWeb);
                try
                {
                    ecmDocumentRoutingWeb.RemoveCustomRouter(this.RouterName);
                }
                catch (Exception e)
                {
                    UnifiedLoggingServer.LogHigh("EcmDocumentRoutingWeb<" + this.RouterName + "> DEL_ERR:" + e.Message);
                }

                UnifiedLoggingServer.LogMedium("---- CRM:ADD_ROUTER");
                ecmDocumentRoutingWeb.AddCustomRouter(this.RouterName, this.AssemblyName, this.ClassName);

                // fetch corresponding folder into corresponding library
                UnifiedLoggingServer.LogMedium("---- CRM_DestPath=" + this.RouterPath);
                if (this.HasRouter)
                {
                    string log = "---- CRM.OnSubmiting\n";
                    UnifiedLoggingServer.LogMedium("---- CRM.OnSubmiting_DestPath=" + this.RouterPath);

                    this.CustomRouter.OnSubmitFile(
                        ecmDocumentRoutingWeb,
                        this.ContentTypeName,
                        author,
                        openBinaryStream,
                        this.RecordsRepositoryProperties,
                        this.RouterFolder,
                        ref log);
                    UnifiedLoggingServer.LogMedium("---- CRM.OnSubmitted_DestPath=" + this.RouterPath);
                    UnifiedLoggingServer.LogMedium(log);
                }

                UnifiedLoggingServer.LogMedium("---- CRM_END_DestPath=" + this.RouterPath);
            }
        }

        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="header">The tracelog header.</param>
        public void TraceLog(string header)
        {
            UnifiedLoggingServer.LogMedium(header + "AbsUrl_:" + this.AbsoluteSiteUrl);
            UnifiedLoggingServer.LogMedium(header + "RteNam_:" + this.RouterName);
            UnifiedLoggingServer.LogMedium(header + "PropSz_:" + this.RecordsRepositoryProperties.Length);
            UnifiedLoggingServer.LogMedium(header + "RleNam_:" + this.RouterRule.Name);
            UnifiedLoggingServer.LogMedium(header + "DesPat_:" + this.RouterPath);
            UnifiedLoggingServer.LogMedium(header + "LibTit_:" + this.RouterLibrary.Title);
            UnifiedLoggingServer.LogMedium(header + "HasRte_:" + this.HasRouter);
            UnifiedLoggingServer.LogMedium(header + "HasTgF_:" + this.HasTargetFolder);
            UnifiedLoggingServer.LogMedium(header + "AssNam_:" + this.AssemblyName);
            UnifiedLoggingServer.LogMedium(header + "ClasNm_:" + this.ClassName);
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
            return "---- CustomRouterManager ----\n" + 
                    "---- CustomRouterManager:" + this.AbsoluteSiteUrl + "\n" +
                    "---- CustomRouterManager:" + this.RouterName + "\n" +
                    "---- CustomRouterManager:" + this.AssemblyName + "\n" +
                    "---- CustomRouterManager:" + this.ClassName;
        }
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
        /// <summary>
        /// Determines whether this instance [can find router] the specified rd.
        /// </summary>
        /// <param name="rd">The rd.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can find router] the specified rd; otherwise, <c>false</c>.
        /// </returns>
        private bool CanFindRouter(RecordDocument rd)
        {
            // starts orphan
            bool boo = false;

            // check if in the family
            boo = rd.HasRoutingRule ? rd.HasRoutingRule : rd.HasParentRoutingRule;

            return boo;
        }

        /// <summary>
        /// Fetches the library.
        /// </summary>
        /// <param name="rd">The rd.</param>
        /// <returns>
        /// The Microsoft.SharePoint.SPList.
        /// </returns>
        private SPList FetchLibrary(RecordDocument rd)
        {
            // starts orphan
            SPList lib = null;

            // check if in the family
            lib = rd.HasLibrary ? rd.CandidateLibrary : rd.ParentCandidateLibrary;

            return lib;
        }

        /// <summary>
        /// The fetch rule.
        /// </summary>
        /// <param name="rd">The rd.</param>
        /// <returns>
        /// The Microsoft.SharePoint.SPListItem.
        /// </returns>
        private SPListItem FetchRule(RecordDocument rd)
        {
            // starts orphan
            SPListItem rule = null;
            
            // check if in the family
            rule = rd.HasLibrary ? rd.RoutingRule : rd.ParentRoutingRule;

            return rule;
        }

        /// <summary>
        /// Fetches the properties.
        /// </summary>
        /// <param name="spFieldCollection">The SP field collection.</param>
        /// <param name="listItem">The list item.</param>
        /// <returns>
        /// The Microsoft.SharePoint.RecordsRepositoryProperty[].
        /// </returns>
        private RecordsRepositoryProperty[] FetchProperties(SPFieldCollection spFieldCollection, SPListItem listItem)
        {
            // create a property list/ array for each metadata field
            List<RecordsRepositoryProperty> propertyList = new List<RecordsRepositoryProperty>();
            foreach (SPField spField in spFieldCollection)
            {
                try
                {
                    string value = Convert.ToString(spField.GetFieldValue(Convert.ToString(listItem[spField.Title])));
                    UnifiedLoggingServer.LogMedium("---- CRR_Props[" + spField.Title + "_|_" + spField.TypeAsString + "_|_" + value + "]");
                    RecordsRepositoryProperty property =
                        new RecordsRepositoryProperty
                        {
                            Name = spField.Title,
                            Type = spField.TypeAsString,
                            Value = value
                        };

                    propertyList.Add(property);
                }
                catch (Exception e)
                {
                }
            }

            return propertyList.ToArray();
        }

        /// <summary>
        /// Fetches the folder.
        /// </summary>
        /// <returns>
        /// The Microsoft.SharePoint.SPFolder.
        /// </returns>
        private SPFolder FetchFolder()
        {
            SPFolder spFolder = null;

            // check if in library
            spFolder = this.HasTargetFolder ? this.RouterLibrary.Folders[0].Folder : this.RouterLibrary.RootFolder;

            return spFolder;
        }

        /// <summary>
        /// Fetches the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The System.String.
        /// </returns>
        private string FetchPath(string path)
        {
            string s = path;

            if (!this.HasRouter) s = this.RouterLibrary.ParentWebUrl + "/" + this.RouterLibrary.Title;
            else
            {
                s = this.HasTargetFolder ? s + this.RouterLibrary.Folders[0].Folder.Url : s + this.RouterRule["Target Library"] + "/";
            }

            return s;
        }
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
                    this.AbsoluteSiteUrl = this.RouterName = this.ContentTypeName =
                        this.AssemblyName = this.ClassName = this.RouterPath = String.Empty;
                    this.RecordsRepositoryProperties = null;
                    this.RouterRule = null;
                    this.RouterLibrary = null;
                    this.RouterFolder = null;
                    this.CustomRouter = null;
                }

                // unmanaged resources clean
                this.HasRouter = this.HasTargetFolder = false;

                // confirm cleaning
                this.isDisposed = true;
            }
        }
        #endregion
    }
}