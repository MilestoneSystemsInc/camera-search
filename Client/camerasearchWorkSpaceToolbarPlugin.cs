using System;
using System.Collections.Generic;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    internal class camerasearchWorkSpaceToolbarPluginInstance : WorkSpaceToolbarPluginInstance
    {
        private Item _window;

        public camerasearchWorkSpaceToolbarPluginInstance()
        {
        }

        public override void Init(Item window)
        {
            _window = window;

            Title = "camerasearch";
        }

        public override void Activate()
        {
            // Here you should put whatever action that should be executed when the toolbar button is pressed
        }

        public override void Close()
        {
        }

    }

    internal class camerasearchWorkSpaceToolbarPlugin : WorkSpaceToolbarPlugin
    {
        public camerasearchWorkSpaceToolbarPlugin()
        {
        }

        public override Guid Id
        {
            get { return camerasearchDefinition.camerasearchWorkSpaceToolbarPluginId; }
        }

        public override string Name
        {
            get { return "camerasearch"; }
        }

        public override void Init()
        {
            // TODO: remove below check when camerasearchDefinition.camerasearchWorkSpaceToolbarPluginId has been replaced with proper GUID
            if (Id == new Guid("22222222-2222-2222-2222-222222222222"))
            {
                System.Windows.MessageBox.Show("Default GUID has not been replaced for camerasearchWorkSpaceToolbarPluginId!");
            }

            WorkSpaceToolbarPlaceDefinition.WorkSpaceIds = new List<Guid>() { ClientControl.LiveBuildInWorkSpaceId, ClientControl.PlaybackBuildInWorkSpaceId, camerasearchDefinition.camerasearchWorkSpacePluginId };
            WorkSpaceToolbarPlaceDefinition.WorkSpaceStates = new List<WorkSpaceState>() { WorkSpaceState.Normal };
        }

        public override void Close()
        {
        }

        public override WorkSpaceToolbarPluginInstance GenerateWorkSpaceToolbarPluginInstance()
        {
            return new camerasearchWorkSpaceToolbarPluginInstance();
        }
    }
}
