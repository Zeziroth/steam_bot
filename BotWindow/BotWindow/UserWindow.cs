using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BotWindow
{
    public partial class UserWindow : Form
    {
        private MySQLController _mySQLConn = null;
        private Form1 _parent = null;

        private int _uid = -1;
        private Dictionary<string, string> data = new Dictionary<string, string>();


        public UserWindow(int uid, MySQLController mySQLConn, Form1 parent)
        {
            _uid = uid;
            _mySQLConn = mySQLConn;
            _parent = parent;

            if (_uid <= 0)
            {
                this.Close();
            }

            InitializeComponent();
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {
            SQLRecord record = _mySQLConn.Select("user", true, new Dictionary<string, string>() { { "uid", _uid.ToString() } });
            

            if (record.NumRows() == 1)
            {
                List<string> keys = record.GetColumns();
                foreach (string key in keys)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = key;
                    string val = record.GetValue(0, key);
                    data.Add(key, val);
                    if (key == "steampass")
                    {
                        string masked = "";
                        itm.Tag = val;
                        val.ToList().ForEach(c => masked += '*');
                        itm.SubItems.Add(masked);
                    }
                    else
                    {
                        itm.SubItems.Add(val);
                    }
                    listView_Details.Items.Add(itm);
                }

                SQLRecord purchaseRecord = _mySQLConn.Select("payment", true, new Dictionary<string, string>() { { "uid", _uid.ToString() } });
                for (int i = 0; i<purchaseRecord.NumRows(); i++)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = purchaseRecord.GetValue(i, "pid");

                    itm.SubItems.Add(purchaseRecord.GetValue(i, "amount") + " $");
                    itm.SubItems.Add(purchaseRecord.GetValue(i, "mail"));
                    itm.SubItems.Add(purchaseRecord.GetValue(i, "txid"));
                    itm.SubItems.Add(purchaseRecord.GetValue(i, "ppMessage"));
                    itm.SubItems.Add(Core.UnixToDate(long.Parse(purchaseRecord.GetValue(i, "paymentTimestamp"))).ToString("dd.MM.yyyy hh:mm:ss"));
                    
                    listView_Purchases.Items.Add(itm);
                }

                SQLRecord gameRecord = _mySQLConn.ReturnQuery("SELECT * FROM userapp LEFT JOIN app ON userapp.aid = app.appid WHERE uid = " + _uid.ToString());
                for (int i = 0; i<gameRecord.NumRows(); i++)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = gameRecord.GetValue(i, "appid");

                    itm.SubItems.Add(gameRecord.GetValue(i, "appname"));
                    itm.SubItems.Add(gameRecord.GetValue(i, "bot"));

                    listView_Games.Items.Add(itm);
                }
            }
        }

        private void UserWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Handle_ListViewUser(object sender)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItems.Count == 1)
            {
                ListViewItem itm = listView.SelectedItems[0];
                string keyValue = itm.Text;
                string oldValue = itm.SubItems[1].Text;
                if (keyValue == "steampass")
                {
                    oldValue = itm.Tag.ToString();
                }
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("New value for " + keyValue, "Edit setting", oldValue);

                if (newValue != itm.SubItems[1].Text)
                {
                    if (newValue.Trim() == "")
                    {
                        if (MessageBox.Show("Wollen Sie wirklich das Feld auf NULL setzen?", "Confirmation",  MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    _mySQLConn.Update("user", new Dictionary<string, string>() { { keyValue, newValue } }, true, new Dictionary<string, string>() { { "uid", _uid.ToString() } });
                    itm.SubItems[1].Text = newValue;
                }
            }
        }

        private void listView_Details_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Handle_ListViewUser(sender);
        }

        private void listView_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Handle_ListViewUser(sender);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/" + data["steamid"] + "/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> games = new List<string>();

            foreach (ListViewItem itm in listView_Games.SelectedItems)
            {
                games.Add(itm.Text);
            }

            CMDWindow boostWindow = new CMDWindow(data["steamuser"], data["steampass"], String.Join("-", games), data["steamsecret"]);
            boostWindow.Show();
        }
    }
}
