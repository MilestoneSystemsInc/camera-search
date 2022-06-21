using System;
using System.Drawing;
using VideoOS.Platform;
using VideoOS.Platform.Admin;

namespace camerasearch.Admin
{
    public class camerasearchTabPlugin : TabPlugin
    {
        private Item _associatedItem = null;

        /// <summary>
        /// This plugin tab is visible when a camera is selected. Change to any other device type guid.
        /// </summary>
        public override Guid AssociatedKind
        {
            get { return Kind.Camera; }
        }

        public override Guid Id
        {
            get { return camerasearchDefinition.camerasearchTabPluginId; }
        }

        public override Image Icon
        {
            get { return camerasearchDefinition.TreeNodeImage; }
        }

        public override string Name
        {
            get { return "camerasearch"; }
        }

        /// <summary>
        /// This method is called when the user has logged in and configuration is accessible.<br/>
        /// </summary>
        public override void Init()
        {
        }

        public override void Close()
        {
        }

        public override TabUserControl GenerateUserControl(Item item)
        {
            _associatedItem = item;
            return new camerasearchTabUserControl(item);
        }

        /// <summary>
        /// Check to see if this plugin tab should be visible.
        /// </summary>
        /// <param name="associatedItem">The currently selected device</param>
        /// <returns></returns>
        public override bool IsVisible(Item associatedItem)
        {
            return true;
        }
    }
}
