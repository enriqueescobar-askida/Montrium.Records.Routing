// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordFieldManager.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   The record field manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Microsoft.SharePoint;

    /// <summary>Record Field Manager.</summary>
    public class RecordFieldManager : IDisposable, ITraceable
    {
        #region fields
        /// <summary>If this is Disposed.</summary>
        private bool isDisposed = false;

        /// <summary>The logger.</summary>
        private string logger = String.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordFieldManager"/> class.
        /// </summary>
        /// <param name="fileSpListItem">The file sp list item.</param>
        /// <param name="contextLibraryList">The contextual library list.</param>
        /// <param name="url">The URL.</param>
        /// <param name="xmlLookup">The XML lookup.</param>
        /// <param name="changeVersion">if set to <c>true</c> [change version].</param>
        public RecordFieldManager(
            SPListItem fileSpListItem, SPList contextLibraryList, string url, string xmlLookup, bool changeVersion)
        {
            if (fileSpListItem == null) throw new ArgumentNullException("fileSpListItem");
            if (contextLibraryList == null) throw new ArgumentNullException("contextLibraryList");
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (new SPSite(url) == null) throw new SPException(url + " Invalid Url");
            if (String.IsNullOrEmpty(xmlLookup)) throw new ArgumentNullException("xmlLookup");

            UnifiedLoggingServer.LogMedium("--- " + this.GetType().Name + " 5@ ---");
            UnifiedLoggingServer.LogMedium("-@1:" + fileSpListItem.Name);
            UnifiedLoggingServer.LogMedium("-@2:" + contextLibraryList.Title);
            UnifiedLoggingServer.LogMedium("-@3:" + url);
            UnifiedLoggingServer.LogMedium("-@4:" + xmlLookup.Length);
            UnifiedLoggingServer.LogMedium("-@5:" + changeVersion);
            this.FileListItem = fileSpListItem;
            this.ContextUrl = url;
            this.ContextListName = url.Split('/')[url.Split('/').Length - 2];
            this.ContextSite = new SPSite(url);
            this.ContextWeb = this.ContextSite.OpenWeb();
            this.ContextFields = contextLibraryList.Fields;
            this.ChangeVersion = changeVersion;
            
            // disable the security validation (temporarily)
            this.ContextSite.RootWeb.AllowUnsafeUpdates = true;

            // Screen source item
            this.ScreenXMLLookup(xmlLookup);

            // Screen for new field from XML Lookup
            // this.AddUnmatchedLookups(fileSpListItem);
        }
        #endregion

        #region DestructorDisposable
        /// <summary>
        /// Finalizes an instance of the <see cref="RecordFieldManager"/> class.
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="RecordFieldManager"/> is reclaimed by garbage collection.
        /// This destructor will run only if the Dispose method does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~RecordFieldManager()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            this.Dispose(false);
        }
        #endregion

        #region AttributesOrProperties
        /// <summary>Gets the context URL.</summary>
        public string ContextUrl { get; internal set; }

        /// <summary>Gets the name of the context list.</summary>
        public string ContextListName { get; internal set; }

        /// <summary>Gets the SRC list item.</summary>
        public SPListItem FileListItem { get; internal set; }

        /// <summary>Gets the context fields.</summary>
        public SPFieldCollection ContextFields { get; internal set; }

        /// <summary>Gets the context site.</summary>
        public SPSite ContextSite { get; internal set; }

        /// <summary>Gets the context web.</summary>
        public SPWeb ContextWeb { get; internal set; }

        /// <summary>Gets a value indicating whether ChangeVersion.</summary>
        public bool ChangeVersion { get; internal set; }

        /// <summary>Gets the new lookup nodes.</summary>
        public List<XmlLookupNode> NewLookupNodes { get; internal set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="boo">if set to <c>true</c> [boo].</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(bool boo)
        {
            return this + "\n" + this.logger;
        }

        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="header">The header.</param>
        public void TraceLog(string header)
        {
            UnifiedLoggingServer.LogMedium(header + "Flds_" + this.ToString(true));
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
            string s = "ContextUrl:\t\t\t" + this.ContextUrl + "\nContextListName:\t" + this.ContextListName + "\n";
            s += "ContextSPSite.Url:\t" + this.ContextSite.Url + "\nContextSPWeb.Url:\t" + this.ContextWeb.Url + "\n"
             + "FileSPListItem:\t\t" + this.FileListItem.Title + "\nContextFieldsCount:\t" + this.ContextFields.Count;
            return s + "\n";
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
        /// Adds the unmatched lookups.
        /// </summary>
        /// <param name="fileSpListItem">The file sp list item.</param>
        private void AddUnmatchedLookups(SPListItem fileSpListItem)
        {
            SPFieldCollection newFields = fileSpListItem.Fields;
            foreach (XmlLookupNode newLookupNode in this.NewLookupNodes)
            {
                string internalName = newFields.Add(newLookupNode.CamelCaseName, newLookupNode.GetSpFieldType(), false);
            }

            fileSpListItem.UpdateOverwriteVersion();
        }

        /// <summary>Determines whether [is SP field title valid] [the specified title].</summary>
        /// <param name="title">The title.</param>
        /// <returns>
        ///   <c>true</c> if [is SP field title valid] [the specified title]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSPFieldTitleValid(string title)
        {
            return !title.Equals("Signatures Status") && !title.Equals("Name");
        }

        /// <summary>Determines whether [is internal name valid] [the specified sp field internal name].</summary>
        /// <param name="spFieldInternalName">Name of the sp field internal.</param>
        /// <returns>
        ///   <c>true</c> if [is SP field internal name valid] [the specified sp field internal name]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSPFieldInternalNameValid(string spFieldInternalName)
        {
            return spFieldInternalName != "TaxCatchAll" && spFieldInternalName != "TaxCatchAllLabel";
        }

        /// <summary>Determines whether [is SP field id valid] [the specified GUID].</summary>
        /// <param name="guid">The GUID.</param>
        /// <returns>
        ///   <c>true</c> if [is SP field id valid] [the specified GUID]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSPFieldIdValid(Guid guid)
        {
            return guid != SPBuiltInFieldId.DocIcon // "Type"
                            && guid != SPBuiltInFieldId.ContentType // "ContentType"
                            && guid != SPBuiltInFieldId.ContentTypeId // "ContentTypeId"
                            && guid != SPBuiltInFieldId.TemplateUrl // "Template Link"
                            && guid != SPBuiltInFieldId.xd_ProgID // "Html File Link"
                            && guid != SPBuiltInFieldId.xd_Signature // "Is Signed"
                            && guid != SPBuiltInFieldId.MetaInfo; // "Property Bag"
        }

        /// <summary>Determines whether [is SP field type valid] [the specified sp field type].</summary>
        /// <param name="spFieldType">The sp field type.</param>
        /// <returns>
        ///   <c>true</c> if [is SP field type valid] [the specified sp field type]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSPFieldTypeValid(SPFieldType spFieldType)
        {
            return spFieldType != SPFieldType.Attachments &&
                                spFieldType != SPFieldType.File &&
                                spFieldType != SPFieldType.Computed;
        }

        /// <summary>Determines whether [is SP field read internal of file] [the specified sp field].</summary>
        /// <param name="spField">The sp field.</param>
        /// <returns>
        ///   <c>true</c> if [is SP field read internal of file] [the specified sp field]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSPFieldReadInternalOfFile(SPField spField)
        {
            return !spField.ReadOnlyField && this.IsSPFieldTitleValid(spField.Title) &&
                    this.IsSPFieldInternalNameValid(spField.InternalName) &&
                    this.IsSPFieldIdValid(spField.Id) &&
                    this.IsSPFieldTypeValid(spField.Type);
        }

        /// <summary>Determines whether [is field copy target] [the specified field].</summary>
        /// <param name="spField">The field.</param>
        /// <param name="validSPField">The source field.</param>
        /// <returns>
        ///   <c>true</c> if [is field copy target] [the specified field]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFieldCopyTarget(SPField spField, ref SPField validSPField)
        {
            // do not copy internal, read-only fields, or the file name
            if (this.IsSPFieldReadInternalOfFile(spField))
            {
                validSPField = this.FileListItem.Fields.Cast<SPField>().FirstOrDefault(f => f.Title == spField.Title) ?? null;

                // source list-item is missing the metadata field
                if (validSPField == null) return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Screens the XML lookup.
        /// </summary>
        /// <param name="xmlLookup">The XML lookup.</param>
        private void ScreenXMLLookup(string xmlLookup)
        {
            XmlLookupManager xmlLookupReader = new XmlLookupManager(xmlLookup);
            List<XmlLookupNode> xmlLookupNodeList = new List<XmlLookupNode>();

            for (int i = 0; i < xmlLookupReader.LookupNodeList.Count; i++)
            {
                XmlLookupNode lookupNode = xmlLookupReader.LookupNodeList[i];
                this.logger += "[" + i + "]".PadRight(5 - i.ToString(CultureInfo.InvariantCulture).Length, '_')
                                + "<" + lookupNode.CamelCaseName + "|" + lookupNode.Type + "|" + lookupNode.Value + ">\n";
                SPField xmlSPField = null;
                SPField validSPField = null;

                // finds the field unsing the current lookupnode in this.FileSPListItem
                bool isFound = this.IsFoundOnFileSPListItemFields(lookupNode, ref xmlSPField);
                bool isCopyTarget = false;
                this.logger += "\tisFound?" + isFound + "\n";

                if (isFound)
                {
                    // fileSPField = this.GetSPFieldOnSPFields(lookupNode);
                    isCopyTarget = this.IsFieldCopyTarget(xmlSPField, ref validSPField);
                    this.logger += "\tisCopyTarget?" + isCopyTarget + "\n";

                    if (!isCopyTarget)
                        this.logger += "\t\t(N)Continue\n";
                    else
                    {
                        // validSPField
                        this.logger += "\t\t(Y)Update<" + xmlSPField.Title + "|" + xmlSPField.Type + ">\twith SPFieldUpdater\n";
                        RecordField spFieldUpdater = new RecordField(xmlSPField, validSPField);
                        this.logger += spFieldUpdater.ToString();
                        this.logger += spFieldUpdater.FixSPField(
                            this.ContextWeb, this.ContextSite, this.FileListItem, lookupNode.Value) + "\n";
                    }
                }
                else
                {
                    // if not found
                    this.logger += "\tIfNotFound Add new (" + lookupNode.CamelCaseName + "|" + lookupNode.Value + "|" + lookupNode.Type + ")";
                    xmlLookupNodeList.Add(lookupNode);
                    this.logger += "\n";
                }
            }

            this.NewLookupNodes = xmlLookupNodeList;
            this.logger += "\n";
        }

        /// <summary>
        /// Determines whether [is found on file SP list item fields] [the specified lookup node].
        /// </summary>
        /// <param name="lookupNode">The lookup node.</param>
        /// <param name="xmlSPField">The file SP field.</param>
        /// <returns>
        ///   <c>true</c> if [is found on SP fields] [the specified lookup node]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFoundOnFileSPListItemFields(XmlLookupNode lookupNode, ref SPField xmlSPField)
        {
            foreach (SPField fileSPFieldItemField in this.FileListItem.Fields)
                if (lookupNode.CamelCaseName.Contains(fileSPFieldItemField.Title))
                {
                    xmlSPField = fileSPFieldItemField;

                    // source list-item is missing the metadata field
                    if (xmlSPField == null) return false;
                    else return true;
                }

            return false;
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
                    this.ContextUrl = this.ContextListName = String.Empty;
                    this.ContextSite = null;
                    this.ContextWeb = null;
                    this.FileListItem = null;
                    this.ContextFields = null;
                }

                // unmanaged resources clean
                this.ChangeVersion = false;

                // confirm cleaning
                this.isDisposed = true;
            }
        }
        #endregion
    }
}