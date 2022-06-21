using System;
using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    /// <summary>
    /// Interaction logic for camerasearchWorkSpaceViewItemWpfUserControl.xaml
    /// </summary>
    public partial class camerasearchWorkSpaceViewItemWpfUserControl : ViewItemWpfUserControl
    {
        public camerasearchWorkSpaceViewItemWpfUserControl()
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
        /// Do not show the sliding toolbar!
        /// </summary>
        public override bool ShowToolbar
        {
            get { return false; }
        }

        private void ViewItemWpfUserControl_ClickEvent(object sender, EventArgs e)
        {
            FireClickEvent();
        }

        private void ViewItemWpfUserControl_DoubleClickEvent(object sender, EventArgs e)
        {
            FireDoubleClickEvent();
        }
    }
}
