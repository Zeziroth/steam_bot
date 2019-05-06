using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BotWindow
{
    public partial class Form1 : Form
    {
        private MySQLController mySQLConn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mySQLConn = new MySQLController(Settings.SERVER, Settings.DATABASE, Settings.USER, Settings.PASS);

            if (mySQLConn.IsConnected())
            {
                button1.Enabled = false;
                button1.Visible = false;
                SetUp();
            }
        }

        private void SetUp()
        {
            Core.RunThread(LoadUserList);
        }

        public void LoadUserList()
        {
            while (true)
            {
                Invoker.UpdateList(listView_User);
                Invoker.ClearList(listView_User);

                SQLRecord recordSet = mySQLConn.Select("user");

                for (int i = 0; i < recordSet.NumRows(); i++)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = recordSet.GetValue(i, "uid");
                    itm.SubItems.Add(recordSet.GetValue(i, "steamid"));

                    itm.SubItems.Add(int.Parse(recordSet.GetValue(i, "gametime")) / 60 + " hours");
                    itm.SubItems.Add(Core.UnixToDate(long.Parse(recordSet.GetValue(i, "lastactive"))).ToString("dd.MM.yyyy hh:mm:ss"));
                    itm.SubItems.Add((recordSet.GetValue(i, "steamuser") != "" && recordSet.GetValue(i, "steampass") != "") ? "YES" : "NO");
                    itm.SubItems.Add(recordSet.GetValue(i, "balance") + " $");
                    itm.Tag = recordSet.GetValue(i, "uid");

                    Invoker.AddListItem(listView_User, itm);
                }
                Invoker.UpdateList(listView_User, false);
                Thread.Sleep(trackBar1.Value * 1000);
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_Sleep.Text = trackBar1.Value + " seconds";
        }
        private void Handle_ListViewUser(object sender)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItems.Count == 1)
            {
                ListViewItem itm = listView.SelectedItems[0];

                UserWindow userWindow = new UserWindow(int.Parse(itm.Text), mySQLConn, this);
                userWindow.Show();
            }
        }
        private void listView_User_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Handle_ListViewUser(sender);
        }
        private void listView_User_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Handle_ListViewUser(sender);
            }
        }
    }
}
