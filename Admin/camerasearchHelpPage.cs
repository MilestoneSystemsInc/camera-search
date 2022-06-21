using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Admin;

namespace camerasearch.Admin
{
    public partial class HelpPage : ItemNodeUserControl
    {
        /// <summary>
        /// User control to display help page
        /// </summary>	
        public HelpPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display information from or about the Item selected.
        /// </summary>
        /// <param name="item"></param>
        public override void Init(Item item)
        {
  }

        /// <summary>
        /// Close any session and release any resources used.
        /// </summary>
        public override void Close()
        {

        }

        private void HelpPage_Load(object sender, EventArgs e)
        {

        }
    }
}
