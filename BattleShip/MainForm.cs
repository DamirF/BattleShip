using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class MainForm : Form
    {
        public const int EMPTY_CELL = 0;
        public const int SHIP_CELL = 1;
        public const int MISS_CELL = 2;
        public const int HIT_CELL = 3;

        private Bitmap field;
        private Pen fieldBorder;
        private Pen GamePen;
        private Graphics graphics;
        private Dictionary<char, int> keys;
        private static List<Ship> playerShips;
        private static bool startGameAllow, shipsPlaced;

        private static int scale;
        private static int[,] playerField;
        private static int[,] enemyField;

        public static List<Ship> GetShips => playerShips;

        public static void GetPlayerField(int[,] insertedField)
        {
            playerField = insertedField;
            shipsPlaced = true;
        }

        public static void GetEnemyField(int[,] field)
        {
            enemyField = field;
        }

        public static int[,] SendField()
        {
            return playerField;
        }

        public MainForm()
        {
            InitializeComponent();
            field = new Bitmap(BattleField.Width, BattleField.Height);
            fieldBorder = new Pen(Color.Black, 2f);
            GamePen = new Pen(Color.Red, 2f);
            playerField = new int[10, 10];
            enemyField = new int[10, 10];
            startGameAllow = false;
            keys = new Dictionary<char, int>();
            scale = BattleField.Width / 10;
            StartGameBut.Enabled = false;
            DictionaryStuff();
            FieldInitialize();
        }

        private void FieldInitialize()
        {
            graphics = Graphics.FromImage(field);
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    field.SetPixel(i, j, Color.White);
                }
            }
            for (int k = 0; k < field.Width / 10; k++)
            {
                graphics.DrawLine(fieldBorder, new Point(field.Width * k / 10, 0), new Point(field.Width * k / 10, field.Height));
                graphics.DrawLine(fieldBorder, new Point(0, field.Height * k / 10), new Point(field.Width, field.Height * k / 10));
            }
            BattleField.Image = field;
        }

        public void DrawField()
        {
            int indent = scale / 2;
            graphics = Graphics.FromImage(field);
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch(playerField[i, j])
                    {
                        case EMPTY_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    field.SetPixel(k, l, Color.White);
                                }
                            }
                            break;
                        case SHIP_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    field.SetPixel(k, l, Color.DarkGray);
                                }
                            }
                            break;
                        case MISS_CELL:
                            GamePen.Color = Color.Black;
                            graphics.DrawEllipse(GamePen, new Rectangle(
                                j * scale + 1 + indent / 2, i * scale + 1 + indent / 2,
                                indent, indent));
                            break;
                        case HIT_CELL:
                            GamePen.Color = Color.Red;
                            graphics.DrawLine(GamePen, new Point(j * scale - 1, i * scale - 1), new Point((j + 1) * scale - 1, (i + 1) * scale - 1));
                            graphics.DrawLine(GamePen, new Point((j + 1) * scale - 1, i * scale - 1), new Point((j) * scale - 1, (i + 1) * scale - 1));
                            break;
                    }
                }
            }
            BattleField.Image = field;
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
            if (!startGameAllow) return;
            Point step;

            if(((Button)sender).BackColor.ToArgb() == Color.White.ToArgb())
            {
                step = PlayerStep(((Button)sender).Name);
                if (playerField[step.Y, step.X] == SHIP_CELL)
                {
                    playerField[step.Y, step.X] = HIT_CELL;
                    ((Button)sender).BackColor = Color.LightGreen;
                    if (CheckField(playerField, HIT_CELL) == 20) MessageBox.Show("Win!");
                }
                else
                {
                    playerField[step.Y, step.X] = MISS_CELL;
                    ((Button)sender).BackColor = Color.DarkGray;
                }
            }
            DrawField();
        }

        private void StartGameBut_Click(object sender, EventArgs e)
        {
            shipPlace.Enabled = false;
            startGameAllow = true;
            ResetGame.Enabled = false;
            StartGameBut.Enabled = false;
        }

        private void StopGame_Click(object sender, EventArgs e)
        {
            shipPlace.Enabled = true;
            startGameAllow = false;
            ResetGame.Enabled = true;
            if(shipsPlaced)StartGameBut.Enabled = true;
        }

        private void Reset()
        {
            FieldInitialize();
            startGameAllow = false;
            shipsPlaced = false;
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

        private void shipPlace_Click(object sender, EventArgs e)
        {
            FieldInitialize();

            AddShips.AddShipsForm startGame = new AddShips.AddShipsForm();
            startGame.ShowDialog();

            if (shipsPlaced) 
            {
                StartGameBut.Enabled = true;
                BOT_Field.FieldAutoInit("BOT");
                while (CheckField(enemyField, SHIP_CELL) != 20) BOT_Field.FieldAutoInit("BOT");
                DrawField();
            }            
        }
    }
}
