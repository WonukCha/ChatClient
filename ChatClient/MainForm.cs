using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace ChatClient
{
    public partial class MainForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        ChatClient chatClinet = null;

        public MainForm()
        {
            InitializeComponent();

            InitListview();
            chatClinet = new ChatClient();
            chatClinet.eOnConnect += OnConnect;
            chatClinet.eOnDisconnect += OnDisconnect;
            chatClinet.eOnReceive += OnReceive;
            chatClinet.eOnSend += OnSend;
            chatClinet.eReceiveChat += OnReceiveChat;

        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            chatClinet.Dispose();
            chatClinet = null;
            this.Close();
        }

        private void panelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string ip = textBoxIP.Text;
            int port = int.Parse(textBoxPort.Text);
            chatClinet.Connect(ip,port);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            chatClinet.Disonnect();

        }
        public void OnConnect(string ip, int port)
        {
            this.Invoke(new Action(delegate ()
            {
                labelNetworkStatus.Text = "Connect";
                labelNetworkStatus.ForeColor = Color.Green;
                AddListViewItem("OnConnect");
            }));
        }
        public void OnDisconnect()
        {
            this.Invoke(new Action(delegate ()
            {
                labelNetworkStatus.Text = "Disconnet";
                labelNetworkStatus.ForeColor = Color.Red;
                AddListViewItem("OnDisconnet");
            }));
        }
        public void OnReceive(byte[] bytes, int size)
        {
            this.Invoke(new Action(delegate ()
            {
                AddListViewItem("Receive");
                AddListViewItem(bytes);
            }));
        }
        public void OnSend(byte[] bytes, int size)
        {
            this.Invoke(new Action(delegate ()
            {
                AddListViewItem("Send");
                AddListViewItem(bytes);
            }));
        }
        public void OnReceiveChat(string id, string msg)
        {
            this.Invoke(new Action(delegate ()
            {
                InsertChatList(id, msg);
            }));
        }
        
        private void InitListview()
        {
            listViewNetwork.View = View.Details;
            listViewNetwork.Columns.Add("Time",100);
            listViewNetwork.Columns.Add("Size", 100);
            listViewNetwork.Columns.Add("Data", 400);

            listViewRoom.View = View.Details;
            listViewRoom.Columns.Add("RoomNumber", 300);

            listViewChat.View = View.Details;
            listViewChat.Columns.Add("Time", 100);
            listViewChat.Columns.Add("User", 100);
            listViewChat.Columns.Add("Msg", 400);
        }
        private void AddListViewItem(byte[] bytes)
        {
            string datetime = DateTime.Now.ToString("hh:mm:ss tt");
            string size = bytes.Length.ToString();
            string hex = BitConverter.ToString(bytes);

            ListViewItem item = new ListViewItem(new string[] {datetime,size,hex });
            listViewNetwork.Items.Add(item);
        }
        private void AddListViewItem(string str)
        {
            string datetime = DateTime.Now.ToString("hh:mm:ss tt");
            string size = str.Length.ToString();

            if (listViewNetwork.Items.Count > 2)
            {
                listViewNetwork.Items.RemoveAt(2);
            }
            ListViewItem item = new ListViewItem(new string[] { datetime, size, str });
            listViewNetwork.Items.Insert(0,item);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            chatClinet.Login(textBoxId.Text, textBoxPw.Text);
        }

        private void buttonRoomEnter_Click(object sender, EventArgs e)
        {
            chatClinet.EnterRoom(0);
        }

        private void buttonChatEnter_Click(object sender, EventArgs e)
        {
            chatClinet.SendChat(textBoxChat.Text);
        }
        private void InsertChatList(string user, string msg)
        {
            string datetime = DateTime.Now.ToString("hh:mm:ss tt");
            ListViewItem item = new ListViewItem(new string[] { datetime, user, msg });
            listViewChat.Items.Add(item);
        }
    }
}
