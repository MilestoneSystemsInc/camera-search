using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;

namespace camerasearch.Client
{
    public partial class camerasearchSidePanelWpfUserControl : SidePanelWpfUserControl
    {
        public camerasearchSidePanelWpfUserControl()
        {
            InitializeComponent();
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }

        /// <summary>
        /// Sample code to show how to maximize the current view item to fill the entire layout area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMax_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(
                new Message(MessageId.SmartClient.ViewItemControlCommand)
                { Data = ViewItemControlCommandData.MaximizeSelectedViewItem });
        }

        /// <summary>
        /// Sample code to show how to restore the view to normal mode, e.g. show all view items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMin_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(
                new Message(MessageId.SmartClient.ViewItemControlCommand)
                { Data = ViewItemControlCommandData.RestoreSelectedViewItem });
        }
    }
}
