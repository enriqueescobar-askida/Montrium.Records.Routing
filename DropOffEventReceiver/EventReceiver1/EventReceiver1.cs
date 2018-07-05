// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventReceiver1.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   List Item Events
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DropOffEventReceiver.EventReceiver1
{
    using System;

    using Microsoft.SharePoint;

    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1 : SPItemEventReceiver
    {
        #region fields
        /// <summary>
        /// The call number.
        /// </summary>
        private static int callNumber = 1;
        #endregion

        #region PublicOverrideMethods
        /// <summary>
        /// The item updated.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            // base.ItemUpdated(properties);
            UnifiedLoggingServer.LogMedium("---- ItemUpdated ----");

            if ((callNumber % 2) == 0)
            {
                UnifiedLoggingServer.LogMedium("---- UseProperties Granted " +
                    (callNumber % 2) + " " + this.EventFiringEnabled);
                this.EventFiringEnabled = false;
                this.UseProperties(properties);
                this.EventFiringEnabled = true;
            }

            callNumber++;
        }

        #endregion

        #region PrivateMethods
        /// <summary>
        /// The use properties.
        /// </summary>
        /// <param name="properties">
        /// The properties.
        /// </param>
        private void UseProperties(SPItemEventProperties properties)
        {
            UnifiedLoggingServer.LogMedium("---- UseProperties ----");

            if (properties.ListTitle.Contains("Drop Off Library"))
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate
                    {
                        SPListItem dropOffListItem = properties.ListItem;

                        using (RecordCentreManager rcm = new RecordCentreManager(dropOffListItem, properties.WebUrl))
                        {
                            UnifiedLoggingServer.LogMedium(rcm.ToString());
                            rcm.TraceLog("TraceLog_RCM_");
                        }
                    });
                }
                catch (Exception e)
                {
                    throw new SPException("An error occured while processing the list Feature/ UseProperties.\n" + e.Message, e);
                }
            }
        }
        #endregion
    }
}
