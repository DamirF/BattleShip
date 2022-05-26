using BattleShip.bot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShip.GameModes;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;

namespace BattleShip
{
    public partial class MainForm : Form
    {
        public const int EMPTY_CELL = 0;
        public const int SHIP_CELL = 1;
        public const int MISS_CELL = 2;
        public const int HIT_CELL = 3;
        public const int FLAG = 4;

        public BOT bot;
        private Bitmap field;
        private Dictionary<char, int> keys;
        private static List<Ship> playerShips;
        private bool startGameAllow, isHit;
        private static int scale;
        private static int[,] playerField;
        private static int[,] enemyField;
        private static UdpClient udpClient, udpClient1;
        private Point ReceivePoint, StepPoint;
        private int stepCondition, receiveStepCondition;

        private int GameMode = 0;


        public MainForm()
        {
            InitializeComponent();
            field = new Bitmap(BattleField.Width, BattleField.Height);
            BattleField.Image = field;
            isHit = true;
            playerField = new int[10, 10];
            enemyField = new int[10, 10];
            startGameAllow = false;
            keys = new Dictionary<char, int>();
            scale = BattleField.Width / 10;
            StartGameBut.Enabled = false;
            DictionaryStuff();
            DrawController.FieldInitialize(ref BattleField, 10);
        }

        public static List<Ship> GetShips => playerShips;
        public static void SendShips(List<Ship> ships) => playerShips = ships;

        public static void GetPlayerField(int[,] insertedField)
        {
            playerField = insertedField;
        }

        public static void GetEnemyField(int[,] field)
        {
            enemyField = field;
        }

        public void GetSecondPlayerField(int[,] field)
        {
            enemyField = field;
        }

        public int[,] SendField()
        {
            return playerField;
        }

        private void DictionaryStuff()
        {
            int key = 0;
            for (int i = 65; i <= 74; i++)
            {
                keys.Add((char)i, key++);
            }
        }

        public static int CheckField(int[,] field, int CELL)
        {
            int check = 0;
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(field[i,j] == CELL) check++;
                }
            }
            return check;
        }

        private Point PlayerStep(string ButName) =>
         new Point
            (
                keys[Convert.ToChar(ButName.Substring(9, 1))],
                Convert.ToInt32(ButName.Substring(10, ButName.Length - 10)) - 1
             );
        

        private void GameButClick(object sender, EventArgs e)
        {
            if (!startGameAllow || !isHit) return;
            Point step, receiveStep;
            int receiveConditionStep;

            if(((Button)sender).BackColor.ToArgb() == Color.White.ToArgb())
            {
                step = PlayerStep(((Button)sender).Name);
                StepPoint = PlayerStep(((Button)sender).Name);
                switch (GameMode)
                {
                    case 1:
                        try
                        {
                            GameButs.Enabled = true;
                            Thread receiveThread = new Thread(new ThreadStart(receivePoint));
                            Thread receiveThread1 = new Thread(new ThreadStart(receiveCondition));
                            //Thread sendPoint1 = new Thread(new ThreadStart(sendPoint));
                            //sendPoint1.Start();
                            receiveThread.Start();
                            receiveThread1.Start();
                            sendPoint();
                            receiveStep = ReceivePoint;
                            receiveConditionStep = receiveStepCondition;
                            if (playerField[receiveStep.Y, receiveStep.X] == MainForm.SHIP_CELL)
                            {
                                playerField[receiveStep.Y, receiveStep.X] = MainForm.HIT_CELL;
                                stepCondition = 1;
                                string stepConditionString = stepCondition.ToString();
                                byte[] bytes = Encoding.UTF8.GetBytes(stepConditionString);
                                udpClient.Send(bytes, bytes.Length);
                            }
                            else
                            {
                                playerField[receiveStep.Y, receiveStep.X] = MainForm.MISS_CELL;
                                stepCondition = 0;
                                string stepConditionString = stepCondition.ToString();
                                byte[] bytes = Encoding.UTF8.GetBytes(stepConditionString);
                                udpClient.Send(bytes, bytes.Length);
                            }
                            if (receiveConditionStep == 0)
                            {
                                GameButs.Enabled = false;
                            }
                            else
                            {
                                GameButs.Enabled = true;
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    case 2:
                        if (enemyField[step.Y, step.X] == SHIP_CELL)
                        {
                            enemyField[step.Y, step.X] = HIT_CELL;
                            ((Button)sender).BackColor = Color.LightGreen;
                            if (CheckField(enemyField, HIT_CELL) == 20) MessageBox.Show("Win!");
                        }
                        else
                        {
                            enemyField[step.Y, step.X] = MISS_CELL;
                            ((Button)sender).BackColor = Color.DarkGray;
                            if (bot != null) BotStep();
                        }
                        break;
                }
            }
            DrawController.DrawField(BattleField, field, playerField);
        }

        private void BotStep()
        {
            Point enemyStep;
            enemyStep = bot.Step();
            switch (playerField[enemyStep.Y, enemyStep.X])
            {
                case SHIP_CELL:
                    playerField[enemyStep.Y, enemyStep.X] = HIT_CELL;
                    Task.Delay(10000);
                    DrawController.DrawField(BattleField, field, playerField); 
                    BotStep();
                    break;
                case EMPTY_CELL:
                    playerField[enemyStep.Y, enemyStep.X] = MISS_CELL;
                    break;
            }

        }

        private void StartGameBut_Click(object sender, EventArgs e)
        {
            if (GameMode == 0)
            {
                MessageBox.Show("Choose game mode!");
                return;
            }

            shipPlace.Enabled = false;
            startGameAllow = true;
            ResetGame.Enabled = false;
            StartGameBut.Enabled = false;

            switch (GameMode)
            {
                case 1:
                    bot = null;
                    udpClient = new UdpClient();
                    udpClient1 = new UdpClient(Convert.ToInt32(portListener.Text));
                    break;
                case 2:
                    bot = new BOT(playerShips);
                    while (CheckField(enemyField, SHIP_CELL) != 20)
                        BOT_Field.FieldAutoInit("BOT");
                    break;
            }
        }

        private void StopGame_Click(object sender, EventArgs e)
        {
            shipPlace.Enabled = true;
            startGameAllow = false;
            ResetGame.Enabled = true;
        }

        private void Reset()
        {
            DrawController.FieldInitialize(ref BattleField, 10);
            startGameAllow = false;
            shipPlace.Enabled = true;
            StartGameBut.Enabled = false;
            playerField = new int[10, 10];
            enemyField = new int[10, 10];
            for(int i = 0; i < GameButs.Controls.Count; i++)
            {
                GameButs.Controls[i].BackColor = Color.White;
            }
        }

        private void ResetGame_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void GM_Player_Click(object sender, EventArgs e)
        {
            GameMode = 1;
            GM.Text = "PLAYER";
        }

        private void GM_BOT_Click(object sender, EventArgs e)
        {
            GameMode = 2;
            GM.Text = "BOT";
        }

        private void shipPlace_Click(object sender, EventArgs e)
        {
            DrawController.FieldInitialize(ref BattleField, 10);

            AddShips.AddShipsForm startGame = new AddShips.AddShipsForm();
            startGame.ShowDialog();
            StartGameBut.Enabled = true;

            //while (CheckField(playerField, SHIP_CELL) != 20)
            //    BOT_Field.FieldAutoInit("BOT");

            playerShips = startGame.SendShipsList();
            for(int i =0; i < playerShips.Count; i++)
            {
                for(int j = 0; j < playerShips[i].points.Count; j++)
                {
                    playerField[playerShips[i].points[j].Y, playerShips[i].points[j].X] = 1;
                }
            }
            startGameAllow = true;
            DrawController.DrawField(BattleField, field, playerField);
        }

        public void PlayerOneStep(Point playerOneStep, Point playerTwoStep)
        {
            sendPoint();
            Thread receiveThread = new Thread(new ThreadStart(receivePoint));
            receiveThread.Start();
            playerTwoStep = ReceivePoint;
            if (playerField[playerTwoStep.Y, playerTwoStep.X] == MainForm.SHIP_CELL)
            {
                playerField[playerTwoStep.Y, playerTwoStep.X] = MainForm.HIT_CELL;
            }
        }

        private void sendPoint()
        {
            try
            {
                string message = StepPoint.ToString();
                byte[] data = Encoding.Unicode.GetBytes(message);
                udpClient.Send(data, data.Length, Adress.Text, Convert.ToInt32(portConnection.Text)); // отправка
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
            udpClient1 = new UdpClient(Convert.ToInt32(portListener.Text)); // UdpClient для получения данных
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

        private void receiveCondition()
        {
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = udpClient1.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    receiveStepCondition = Convert.ToInt32(message);
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
