using System;
using System.Windows.Controls;
using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    public class camerasearchSettingsPanelPlugin : SettingsPanelPlugin
    {
        private UserControl _userControl;

        /// <summary>
        /// Should return unique id identifying this plug-in.
        /// </summary>
        public override Guid Id
        {
            get { return camerasearchDefinition.camerasearchSettingsPanel; }
        }

        /// <summary>
        /// Should return the title of this plug-in. It will be displayed in the Settings panel navigation list.
        /// </summary>
        public override string Title { get { return "camerasearch"; } }

        /// <summary>
        /// This method is called just before the Settings panel is closed.
        /// </summary>
        public override void Close()
        { }

        /// <summary>
        /// Should close the user control and clean up any resources or event registrations. <br>
        /// It's called when a user navigates away from this plug-in - either when selecting different component in the navigation list or when closing the Settings panel.
        /// </summary>
        public override void CloseUserControl()
        {
            _userControl = null;
        }

        /// <summary>
        /// Should create a Windows Presentation Foundation (WPF) user control which represents the user interface (UI) of this plug-in.
        /// </summary>
        public override UserControl GenerateUserControl()
        {
            _userControl = new camerasearchSettingsPanelControl(this);
            return _userControl;
        }

        /// <summary>
        /// This method is called when the Settings panel is loaded. <br>
        /// It should be used for accessing configuration items if needed.
        /// </summary>
        public override void Init()
        {
            LoadProperties(false);
        }

        /// <summary>
		/// Should save any changes made in the user control. If the save operation fails, use errorMessage to provide a string describing the error. <br>
		/// This method should be overridden by the plug-in to validate the entries and possibly call <see cref="SaveProperties(bool)"/> to have the properties stored on the server. 
		/// (If the values have been set using <see cref="PropertyClass.SetProperty(string, string)"/>)
        /// </summary>
        /// <param name="errorMessage">The error description if saving failed. An empty string if successfully saved.</param>
        /// <returns>True if settings were successfully saved, otherwise false.</returns>
        public override bool TrySaveChanges(out string errorMessage)
        {
            SaveProperties(false);
            errorMessage = string.Empty;
            return true;
        }
    }
}
