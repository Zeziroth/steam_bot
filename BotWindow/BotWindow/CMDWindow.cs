using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotWindow
{
    public partial class CMDWindow : Form
    {
        private string _user;
        private string _pass;
        private string _games;
        private string _secret;
        private int gameCount = 0;

        private CMDHandler cmdBoost = null;
        private int procID = -1;
        private Stopwatch sw = new Stopwatch();
        private List<Thread> threads = new List<Thread>();
        private bool globalStop = false;

        public CMDWindow(string user, string pass, string games, string secret)
        {
            _user = user;
            _pass = pass;
            _games = games;
            _secret = secret;
            gameCount = _games.Split('-').Length;
            cmdBoost = new CMDHandler(@"C:\Windows\System32\cmd.exe");
            InitializeComponent();
        }

        private void TrackTime()
        {
            while (!globalStop)
            {
                Invoker.SetLabelText(label_Elapsed, "Running: " + (sw.ElapsedMilliseconds / 1000) + " seconds");
                Invoker.SetLabelText(label_Boosted, "Boosted: " + (sw.ElapsedMilliseconds / 1000) * gameCount + " seconds");
                Thread.Sleep(50);
            }
            sw.Stop();
            globalStop = false;
        }
        private void CmdBoost_StandartTextReceived(object sender, string e)
        {
            try
            {
                if (e.Contains(_pass))
                {
                    return;
                }

                if (!e.StartsWith(Environment.NewLine))
                {
                    richTextBox1.AppendText(Environment.NewLine);
                }
                richTextBox1.AppendText(e);
            }
            catch { }
        }

        private void CMDWindow_Load(object sender, EventArgs e)
        {
            sw.Start();
            label_Customer.Text = "Customer: " + _user;
            label_Gamecount.Text = "Games boosting: " + gameCount;
            string query = String.Format("{0} {1} {2} {3} {4} {5}", "node", "afk.js", _user, _pass, _games, _secret);
            cmdBoost.StandartTextReceived += CmdBoost_StandartTextReceived;
            cmdBoost.ExecuteAsync();

            cmdBoost.WriteLine(query);

            threads.Add(Core.RunThread(TrackTime));
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.KeyCode == Keys.Enter)
            {
                cmdBoost.Write(textBox.Text);
                textBox.Clear();
            }
        }

        private void CMDWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process p in cmdBoost.process.GetChildProcesses())
            {
                p.Kill();
            }

            while (threads.Count > 0)
            {
                threads[0].Abort();
                threads.RemoveAt(0);
            }
        }
    }
}
