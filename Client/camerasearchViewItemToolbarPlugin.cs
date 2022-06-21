using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    internal class camerasearchViewItemToolbarPluginInstance : ViewItemToolbarPluginInstance
    {
        private Item _viewItemInstance;
        private Item _window;

        public override void Init(Item viewItemInstance, Item window)
        {
            _viewItemInstance = viewItemInstance;
            _window = window;

            Title = "camerasearch";
            Tooltip = "camerasearch tooltip";
        }

        public override void Activate()
        {
            // Here you should put whatever action that should be executed when the toolbar button is pressed
        }

        public override void Close()
        {
        }
    }

    internal class camerasearchViewItemToolbarPlugin : ViewItemToolbarPlugin
    {
        public override Guid Id
        {
            get { return camerasearchDefinition.camerasearchViewItemToolbarPluginId; }
        }

        public override string Name
        {
            get { return "camerasearch"; }
        }

        public override ToolbarPluginOverflowMode ToolbarPluginOverflowMode
        {
            get { return ToolbarPluginOverflowMode.AsNeeded; }
        }

        public override void Init()
        {
            // TODO: remove below check when camerasearchDefinition.camerasearchViewItemToolbarPluginId has been replaced with proper GUID
            if (Id == new Guid("33333333-3333-3333-3333-333333333333"))
            {
                System.Windows.MessageBox.Show("Default GUID has not been replaced for camerasearchViewItemToolbarPluginId!");
            }

            ViewItemToolbarPlaceDefinition.ViewItemIds = new List<Guid>() { ViewAndLayoutItem.CameraBuiltinId };
            ViewItemToolbarPlaceDefinition.WorkSpaceIds = new List<Guid>() { ClientControl.LiveBuildInWorkSpaceId, ClientControl.PlaybackBuildInWorkSpaceId, camerasearchDefinition.camerasearchWorkSpacePluginId };
            ViewItemToolbarPlaceDefinition.WorkSpaceStates = new List<WorkSpaceState>() { WorkSpaceState.Normal };
        }

        public override void Close()
        {
        }

        public override ViewItemToolbarPluginInstance GenerateViewItemToolbarPluginInstance()
        {
            return new camerasearchViewItemToolbarPluginInstance();
        }
    }
}
