using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace BattleShip.GameModes
{
    public class Game
    {
        private int[,] firstPlayerField;
        private int[,] secondPlayerField;
        private bool FirstPlayerStep, SecondPlayerStep;
        private static Random rnd = new Random();
        private static UdpClient udpClient, udpClient1;
        private Point ReceivePoint;

       public Game(ref int[,]  firstPlayerField, ref int[,] secondPlayerField)
       {
            this.firstPlayerField = firstPlayerField;
            this.secondPlayerField = secondPlayerField;
            if(rnd.Next(0,2) == 0)
            {
                FirstPlayerStep = true;
                SecondPlayerStep = false;
            }
            else
            {
                FirstPlayerStep = false;
                SecondPlayerStep = true;
            }
       }

        public static void StartGame()
        {

        }

        public bool PlayerOneStep(Point playerOneStep, Point playerTwoStep)
        {
            sendPoint(playerOneStep);
            Thread receiveThread = new Thread(new ThreadStart(receivePoint));
            receiveThread.Start();
            playerTwoStep = ReceivePoint;
            if (secondPlayerField[playerTwoStep.Y, playerTwoStep.X] == MainForm.SHIP_CELL)
            {
                FirstPlayerStep=true;
                secondPlayerField[playerTwoStep.Y, playerTwoStep.X] = MainForm.HIT_CELL;
            }
            else
            {
                FirstPlayerStep=false;
            }
            return FirstPlayerStep;
        }

        //public bool PlayerTwoStep(Point playerOneStep, Point playerTwoStep)
        //{
        //    sendPoint(playerTwoStep);
        //    playerOneStep = receivePoint();
        //    if (firstPlayerField[playerOneStep.Y, playerOneStep.X] == MainForm.SHIP_CELL)
        //    {
        //        SecondPlayerStep = true;
        //        secondPlayerField[playerOneStep.Y, playerOneStep.X] = MainForm.HIT_CELL;
        //    }
        //    else
        //    {
        //        SecondPlayerStep = false;
        //    }
        //    return SecondPlayerStep;
        //}

        private void sendPoint(Point sendPoint)
        {
            udpClient = new UdpClient();
            try
            {
                while (true)
                {
                    string message = sendPoint.ToString();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    udpClient.Send(data, data.Length, "127.0.0.1", 8801); // отправка
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }

        private void receivePoint()
        {
            udpClient1 = new UdpClient(8800); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = udpClient1.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    string newmessage = "";
                    for (int i = 0; i < message.Length; i++)
                    {
                        if (message[i] >= 48 && message[i] <= 57)
                        {
                            newmessage += message[i];
                        }
                    }

                    ReceivePoint = new Point(Convert.ToInt32(Char.GetNumericValue(newmessage[0])), Convert.ToInt32(Char.GetNumericValue(newmessage[1])));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                udpClient1.Close();
            }
        }
    }
}
