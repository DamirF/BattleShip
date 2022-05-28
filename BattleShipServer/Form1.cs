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
        Random random = new Random();
        private int selectClient;

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
                 if (Clients.Items.Count > 2)
                 {
                     server.DisconnectClient(e.IpPort);
                     Clients.Items.RemoveAt(2);
                 }
                 if (Clients.Items.Count == 2)
                 {
                     selectClient = random.Next(0, 2);
                     server.Send(Clients.Items[selectClient].ToString(), "+");
                     if(selectClient == 0)
                     {
                         selectClient++;
                         server.Send(Clients.Items[selectClient].ToString(), "-");
                     }
                     else if(selectClient == 1)
                     {
                         selectClient--;
                         server.Send(Clients.Items[selectClient].ToString(), "-");
                     }
                 }
             });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if(Clients.Items.Count == 2)
                {
                    if (e.IpPort == Clients.Items[0].ToString())
                    {
                        server.Send(Clients.Items[1].ToString(), e.Data);
                    }
                    else if (e.IpPort == Clients.Items[1].ToString())
                    {
                        server.Send(Clients.Items[0].ToString(), e.Data);
                    }
                }
            });
        }
    }
}
