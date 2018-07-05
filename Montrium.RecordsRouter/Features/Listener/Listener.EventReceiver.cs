// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Listener.EventReceiver.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Montrium.RecordsRouter.Features.Listener
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Security;

    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>
    [Guid("892b18dc-9e18-4d49-ba2d-e13ff95e0731")]
    public class ListenerEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        /// <summary>
        /// The feature activated.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (RecordCentreManager rcm = new RecordCentreManager("http://dev2010/sites/rc/"))
                {
                    UnifiedLoggingServer.LogMedium(rcm.ToString());
                }

                /*if ((properties.Feature.Parent as SPSite) != null)
                    using (SPSite spSite = properties.Feature.Parent as SPSite)
                    {
                        MessageBox.Show(spSite.Url);
                    using (SPWeb web2 = site.OpenWeb(properties.Web.ID))
                    {
                        SPListItem item = web2.Lists.GetList(properties.List.ID, false).GetItemById(properties.ListItemId);

                        Records.BypassLocks(item, delegate(SPListItem newItem)
                        {
                            newItem["Title"] = "Hello from code";
                            newItem.Audit.WriteAuditEvent(SPAuditEventType.Custom, SPAuditEventSource.ObjectModel.ToString(), "<Data>Audit text</Data>");
                            newItem.SystemUpdate(false);
                        });

                    }
                }*/
            });
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
