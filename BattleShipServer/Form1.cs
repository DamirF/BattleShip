using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer(HostTextBox.Text);
            try
            {
                server.Start();
            }
            catch
            {
                MessageBox.Show("Сервер уже запущен");
            }
            server.Events.DataReceived += Events_DataReceived;
            server.Events.ClientConnected += Events_ClientConnected;
        }

        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
             this.Invoke((MethodInvoker)delegate
             {
                 Clients.Items.Add(e.IpPort);
             });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if(e.IpPort == Clients.Items[0].ToString())
                {
                    server.Send(Clients.Items[1].ToString(), e.Data);
                }   
                else if(e.IpPort == Clients.Items[1].ToString())
                {
                    server.Send(Clients.Items[0].ToString(), e.Data);
                }
            });
        }
    }
}
