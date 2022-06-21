using VideoOS.Platform.Admin;

namespace camerasearch.Admin
{
    public partial class camerasearchToolsOptionDialogUserControl : ToolsOptionsDialogUserControl
    {
        public camerasearchToolsOptionDialogUserControl()
        {
            InitializeComponent();
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }

        public string MyPropValue
        {
            set { textBoxPropValue.Text = value ?? ""; }
            get { return textBoxPropValue.Text; }
        }
    }
}
