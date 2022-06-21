using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    /// <summary>
    /// This UserControl contains the visible part of the Property panel during Setup mode. <br/>
    /// If no properties is required by this ViewItemPlugin, the GeneratePropertiesUserControl() method on the ViewItemManager can return a value of null.
    /// <br/>
    /// When changing properties the ViewItemManager should continuously be updated with the changes to ensure correct saving of the changes.
    /// <br/>
    /// As the user click on different ViewItem, the displayed property UserControl will be disposed, and a new one created for the newly selected ViewItem.
    /// </summary>
    public partial class camerasearchPropertiesWpfUserControl : PropertiesWpfUserControl
    {
        #region private fields

        private camerasearchViewItemManager _viewItemManager;

        #endregion

        #region Initialization & Dispose

        /// <summary>
        /// This class is created by the ViewItemManager.  
        /// </summary>
        /// <param name="viewItemManager"></param>
        public camerasearchPropertiesWpfUserControl(camerasearchViewItemManager viewItemManager)
        {
            _viewItemManager = viewItemManager;
            InitializeComponent();
        }

        /// <summary>
        /// Setup events and message receivers and load stored configuration.
        /// </summary>
        public override void Init()
        {
            if (_viewItemManager.ConfigItems != null)
            {
                FillContent(_viewItemManager.ConfigItems, _viewItemManager.SomeId);
            }
        }

        /// <summary>
        /// Perform any cleanup stuff and event -=
        /// </summary>
        public override void Close()
        {
        }

        /// <summary>
        /// We have some configuration from the server, that the user can choose from.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="selectedId"></param>
        internal void FillContent(List<Item> config, Guid selectedId)
        {
            comboBoxID.Items.Clear();
            ComboBoxNode selectedComboBoxNode = null;

            foreach (Item item in config)
            {
                ComboBoxNode comboBoxNode = new ComboBoxNode(item);
                comboBoxID.Items.Add(comboBoxNode);
                if (comboBoxNode.Item.FQID.ObjectId == selectedId)
                    selectedComboBoxNode = comboBoxNode;
            }

            if (selectedComboBoxNode != null)
                comboBoxID.SelectedItem = selectedComboBoxNode;
        }

        #endregion

        #region Event handling

        private void comboBoxID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewItemManager.SomeId = ((ComboBoxNode)comboBoxID.SelectedItem).Item.FQID.ObjectId;
        }

        #endregion
    }

    internal class ComboBoxNode
    {
        internal Item Item { get; private set; }
        internal ComboBoxNode(Item item)
        {
            Item = item;
        }

        public override string ToString()
        {
            return Item.Name;
        }
    }
}
