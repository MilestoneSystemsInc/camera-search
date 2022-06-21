using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using System.ComponentModel;
using VideoOS.Platform.Admin;
using VideoOS.Platform.UI;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Resources;
using VideoOS.Platform.Util;
using VideoOS.Platform.ConfigurationItems;



namespace camerasearch.Admin
{
    /// <summary>
    /// This UserControl only contains a configuration of the Name for the Item.
    /// The methods and properties are used by the ItemManager, and can be changed as you see fit.
    /// </summary
    /// >
    /// 
    
    
    public partial class camerasearchUserControl : UserControl
    {
        internal event EventHandler ConfigurationChangedByUser;
        DataTable dt = new DataTable();
       

        public camerasearchUserControl()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerAsync(2000);
        }

        internal String DisplayName
        {
            get { return ""; }
            set {  }
        }

        /// <summary>
        /// Ensure that all user entries will call this method to enable the Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnUserChange(object sender, EventArgs e)
        {
            search();

        }


        internal void FillContent(Item item)
        {
            
        }

        internal void UpdateItem(Item item)
        {
            item.Name = DisplayName;
            // Fill in any propertuies that should be saved:
            //item.Properties["AKey"] = "some value";
        }

        internal void ClearContent()
        {
            textBoxSearch.Text = "";
        }

        private void camerasearchUserControl_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            textBoxSearch.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            backgroundWorker1.RunWorkerAsync(2000);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
        private void search()
        {

            string Sfilter = "";
            string searchtxt = textBoxSearch.Text;
            if (dt != null)
            {
                if (checkBoxDisabledDevices.Checked)
                {
                    Sfilter = "ALL Like '%" + searchtxt + "%'";
                }
                else
                {
                    Sfilter = "ALL Like '%" + searchtxt + "%' and Enabled=true";
                }

                
                if (listBox1.SelectedIndices.Count>0)
                {
                    Sfilter = Sfilter + " and (";
                    foreach (int i in listBox1.SelectedIndices)
                    {
                        
                        Sfilter = Sfilter + " devicetype='" + listBox1.Items[i].ToString() + "'";

                        Sfilter = Sfilter + " or "; 
                    }
                    Sfilter = Sfilter.Remove(Sfilter.Length-4,4)+ ")";
                }

            }

            if (dt.Rows.Count != 0)
            {
                dt.DefaultView.RowFilter = Sfilter;

            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            search();


        }
        public static void SafeInvoke(System.Windows.Forms.Control control, System.Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(new System.Windows.Forms.MethodInvoker(() => { action(); }));
            else
                action();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            BackgroundWorker helperBW = sender as BackgroundWorker;
            e.Result = BackgroundProcessLogicMethod(helperBW);
            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                Log_Message("Assigning Values to UI",false);
                SafeInvoke(button1, () => { button1.Enabled = true; });
                SafeInvoke(dataGridView1, () => { dataGridView1.DataSource = dt; });
                SafeInvoke(dataGridView1, () => { dataGridView1.Columns["ALL"].Visible = false; });
                SafeInvoke(dataGridView1, () => { dataGridView1.ResumeLayout(); });
                SafeInvoke(button2, () => { button2.Enabled = true; });
                SafeInvoke(textBoxSearch, () => { textBoxSearch.Enabled = true; });
                SafeInvoke(progressBar1, () => { progressBar1.Visible = false; });
                SafeInvoke(hidelabel, () => { hidelabel.Visible = false; });
                SafeInvoke(hidelabel, () => { hidelabel.Visible = false; });
                SafeInvoke(listBox1, () => { for (int intCount = 0; intCount < dt.Rows.Count; intCount++)
                    {
                        var val = dt.Rows[intCount]["DeviceType"].ToString();

                        //check if it already exists
                        if (!listBox1.Items.Contains(val))
                        {
                            listBox1.Sorted = false;
                            listBox1.Items.Add(val);
                            listBox1.SetSelected(listBox1.Items.Count-1, true);
                            listBox1.Sorted=true;
                        }
                    }

                });
                Log_Message("Assigned Values to UI",false);
            }
           

        }
 

        private void Log_Message(string mymessage,Boolean error)
            {
            EnvironmentManager.Instance.Log(false, "4Js - Camera Search", mymessage); ; 
        }

        private int BackgroundProcessLogicMethod(BackgroundWorker bw)
        {
            int counter = 0;
            int Total =0;
            
            Log_Message("Search began",false);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            bw.ReportProgress(0);
            Configuration.Instance.RefreshConfiguration(Kind.Server);
            List<Item> sites = Configuration.Instance.GetItemsByKind(Kind.Server, ItemHierarchy.SystemDefined);
            Log_Message("Initial Config pulled", false); 
            bw.ReportProgress(50);
            dt.Clear();
            if (dt.Columns.Count == 0)
            {

                dt.Columns.Add("SiteName");
                dt.Columns.Add("RecordingServer");
                dt.Columns.Add("HardwareName");
                dt.Columns.Add("Address");
                dt.Columns.Add("MAC");
                dt.Columns.Add("Model");
                dt.Columns.Add("DeviceName");
                dt.Columns.Add("DeviceType");
                dt.Columns.Add("Enabled");
                dt.Columns.Add("ALL");
                
            }

            Log_Message("Columns Rendered", false);
            Log_Message("Pulling Config", false);
            foreach (Item site in sites)
            {
                ManagementServer managementServer = new ManagementServer(site.FQID);

                ICollection<RecordingServer> servers = managementServer.RecordingServerFolder.RecordingServers;
                Log_Message("recording servers Pulled:" + servers.Count, false);
                foreach (RecordingServer server in servers)
                {
                    Total = sites.Count * servers.Count;
                    counter = +1;

                    bw.ReportProgress(50 + (50 * (counter / Total)));
                    ICollection<Hardware> hardwares = server.HardwareFolder.Hardwares;

                    foreach (Hardware hardware in hardwares)
                    {

                        
                        ICollection<Camera> cameras = hardware.CameraFolder.Cameras;
                        ICollection<Metadata> metadatas = hardware.MetadataFolder.Metadatas;
                        ICollection<Microphone> microphones = hardware.MicrophoneFolder.Microphones;
                        ICollection<Speaker> speakers = hardware.SpeakerFolder.Speakers;
                        ICollection <InputEvent> inputs = hardware.InputEventFolder.InputEvents;
                        ICollection<Output> outputs = hardware.OutputFolder.Outputs;
                        string strMACAddress=hardware.HardwareDriverSettingsFolder.HardwareDriverSettings.FirstOrDefault().HardwareDriverSettingsChildItems.FirstOrDefault().Properties.GetValue("MacAddress");
                        

                        foreach (Camera camera in cameras)
                        {
                            
                            
                            DataRow _ravi = dt.NewRow();

                            if (hardware.Enabled) { 
                            _ravi["Enabled"] = camera.Enabled;
                            }
                            else {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = camera.Name;
                            _ravi["DeviceType"] = "Camera";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress; 
                            _ravi["ALL"] = "Camera" + camera.Name + server.Name + site.Name + hardware.Name + hardware.Address + strMACAddress+hardware.Model+"";
                            dt.Rows.Add(_ravi);

                        }
                        foreach (Metadata Metadata in metadatas)
                        {

                            DataRow _ravi = dt.NewRow();
                            if (hardware.Enabled)
                            {
                                _ravi["Enabled"] = Metadata.Enabled;
                            }
                            else
                            {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = Metadata.Name;
                            _ravi["DeviceType"] = "Metadata";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress;
                            _ravi["ALL"] = "Metadata" + Metadata.Name + server.Name + site.Name + hardware.Name + hardware.Address + strMACAddress+hardware.Model+"";
                            dt.Rows.Add(_ravi);
                         

                        }
                        foreach (Microphone Microphone in microphones)
                        {

                            DataRow _ravi = dt.NewRow();
                            if (hardware.Enabled)
                            {
                                _ravi["Enabled"] = Microphone.Enabled;
                            }
                            else
                            {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = Microphone.Name;
                            _ravi["DeviceType"] = "Microphone";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress;
                            _ravi["ALL"] = "Microphone" + Microphone.Name + server.Name + site.Name + hardware.Name + hardware.Address+ strMACAddress+hardware.Model+"";
                            dt.Rows.Add(_ravi);

                        }
                        foreach (Speaker Speaker in speakers)
                        {

                            DataRow _ravi = dt.NewRow();
                            if (hardware.Enabled)
                            {
                                _ravi["Enabled"] = Speaker.Enabled;
                            }
                            else
                            {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = Speaker.Name;
                            _ravi["DeviceType"] = "Speaker";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress;
                            _ravi["ALL"] = "Speaker" + Speaker.Name + server.Name + site.Name + hardware.Name + hardware.Address + strMACAddress+hardware.Model+"";
                            dt.Rows.Add(_ravi);

                        }
                        foreach (InputEvent Input in inputs)
                        {

                            DataRow _ravi = dt.NewRow();
                            if (hardware.Enabled)
                            {
                                _ravi["Enabled"] = Input.Enabled;
                            }
                            else
                            {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = Input.Name;
                            _ravi["DeviceType"] = "Input";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress;
                            _ravi["ALL"] = "Input" + Input.Name + server.Name + site.Name + hardware.Name + hardware.Address+ strMACAddress+hardware.Model+""; 
                            dt.Rows.Add(_ravi);

                        }

                        foreach (Output output in outputs)
                        {

                           
                            DataRow _ravi = dt.NewRow();
                            if (hardware.Enabled)
                            {
                                _ravi["Enabled"] = output.Enabled;
                            }
                            else
                            {
                                _ravi["Enabled"] = hardware.Enabled;
                            }
                            _ravi["DeviceName"] = output.Name;
                            _ravi["DeviceType"] = "Output";
                            _ravi["RecordingServer"] = server.Name;
                            _ravi["SiteName"] = site.Name;
                            _ravi["Model"] = hardware.Model;
                            _ravi["HardwareName"] = hardware.Name;
                            _ravi["Address"] = hardware.Address;
                            _ravi["MAC"] = strMACAddress ;
                            _ravi["ALL"] = "Input" + output.Name + server.Name + site.Name + hardware.Name + hardware.Address + strMACAddress + hardware.Model + "";
                            dt.Rows.Add(_ravi);

                        }
                    }

                }
            }
            Log_Message("Config Pulled and stored : Total devices : " + dt.Rows.Count +  ": took :" + stopwatch.ElapsedMilliseconds,false);
            stopwatch.Stop();
            return 0;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            search();

        }
    }
}
