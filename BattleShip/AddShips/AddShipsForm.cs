using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShip.AddShips
{
    public partial class AddShipsForm : Form
    {
        private Bitmap field, addField;
        private Ship ship = null;
        private List<Ship> ships;
        private Timer autoPlace;
        private bool auto;

        private int scale, _scale;
        private int autoPlaceTime;
        private static int[,] shipsField;

        int[] decksCountCheck;
        readonly int[] maxDecksCount = new int[4] {4, 3, 2, 1};

        public static void GetField(int[,] _field)
        {
            shipsField = _field;
        }

        public List<Ship> SendShipsList()
        {
            return ships;
        }

        public AddShipsForm()
        {
            InitializeComponent();

            field = new Bitmap(Field.Width, Field.Height);
            Field.Image = field;
            addField = new Bitmap(AddShipPB.Width, AddShipPB.Height);
            AddShipPB.Image = addField;

            scale = Field.Width / 10;
            _scale = AddShipPB.Width / 5;

            ships = new List<Ship>();
            
            auto = false;

            autoPlace = new Timer();
            autoPlace.Interval = 100;
            autoPlace.Tick += AutoPlace_Tick;
            autoPlaceTime = 0;

            shipsField = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    shipsField[i, j] = 0;
                }
            }

            decksCountCheck = new int[4] { 0, 0, 0, 0 };

            DrawController.FieldInitialize(ref Field, 10);
            DrawController.FieldInitialize(ref AddShipPB, 5);

            shipOrientation.SelectedIndex = 0;
            shipOrientation.Text = shipOrientation.Items[0].ToString();
        }

        private void AutoPlace_Tick(object sender, EventArgs e)
        {
            autoPlaceTime += 100;
            if (autoPlaceTime > 1200)
            {
                Save();
                Close();
            }
        }

        private void Clear()
        {
            DrawController.FieldInitialize(ref Field, 10);
            DrawController.FieldInitialize(ref AddShipPB, 5);
            auto = false;
            ship = null;
            ships.Clear();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    shipsField[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++) decksCountCheck[i] = 0;
        }

        private void ClearFieldBut_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void AddShip_Click(object sender, EventArgs e)
        {
            
            if(auto) Clear();
            if(decksCountCheck[Convert.ToInt32(decksCount.Value) - 1] == maxDecksCount[Convert.ToInt32(decksCount.Value) - 1])
            {
                MessageBox.Show("Достигнуто максимально количество кораблей с данным количеством палуб!");
                return;
            }
            else
            {
                DrawController.FieldInitialize(ref AddShipPB, 5);
                Point p = new Point(1, 1);
                ship = new Ship(Convert.ToInt32(decksCount.Value), shipOrientation.SelectedIndex, p);
                ship.DefShipCoord(p);
                DrawShip(ship, addField, _scale, Color.DarkGray);
                AddShipPB.Image = addField;

                if (CheckedShipIsExist())
                {
                    int j = 0;
                    for (; j < ships.Count; j++)
                    {
                        if (ships[j].isChoosed) break;
                    }
                    Ship.ShipTranslation(ships[j], 1, shipsField);
                    
                }
                for (int i = 0; i < ships.Count; i++)
                {
                    ships[i].isChoosed = false;
                    DrawShip(ships[i], field, scale, Color.DarkGray);
                }
                Field.Image = field;
            }
        }

        private void DrawShip(Ship ship, Bitmap map, int Tscale, Color color)
        {
            for (int i = 0; i < ship.points.Count; i++)
            {
                for (int k = ship.points[i].X * Tscale + 1; k < (ship.points[i].X + 1) * Tscale - 1; k++)
                {
                    for (int l = ship.points[i].Y * Tscale + 1; l < (ship.points[i].Y + 1) * Tscale - 1; l++)
                    {
                        map.SetPixel(k, l, color);
                    }
                }
            }
        }

        private void shipOrientation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void DeleteShip_Click(object sender, EventArgs e)
        {
            if (ship != null)
            {
                ship = null;
                DrawController.FieldInitialize(ref AddShipPB, 5);
            }
            else if (ship == null && CheckedShipIsExist())
            {
                int i = 0;
                for (; i < ships.Count; i++)
                {
                    if (ships[i].isChoosed) break;
                }
                decksCountCheck[ships[i].decksCount - 1]--;
                DrawShip(ships[i], field, scale, Color.White);
                Ship.ShipTranslation(ships[i], 0, shipsField);
                ships.RemoveAt(i);
                Field.Image = field;
            }
        }

        private void Field_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Ship tempShip;
            int i = 0;
            bool allowesPutShip = true;
            Point temp = new Point(Convert.ToInt32(e.X / scale), Convert.ToInt32(e.Y / scale));

            if (field.GetPixel(e.X, e.Y).ToArgb() == Color.White.ToArgb())
            {
                if (ship != null && ship.isChoosed)
                {
                    tempShip = ship;
                    tempShip.startPoint = new Point(Convert.ToInt32(e.X / scale), Convert.ToInt32(e.Y / scale));
                    tempShip.DefShipCoord(tempShip.startPoint);
                    for(int k = 0; k < tempShip.points.Count; k++)
                    {
                        allowesPutShip = Ship.AllowPutShip(tempShip, k, shipsField);
                        if (allowesPutShip == false) break;
                    }
                    if (allowesPutShip)
                    {
                        ship.startPoint = new Point(Convert.ToInt32(e.X / scale), Convert.ToInt32(e.Y / scale));
                        ship.DefShipCoord(ship.startPoint);
                        ship.isChoosed = false;
                        decksCountCheck[ship.decksCount - 1]++;
                        ships.Add(ship);
                        Ship.ShipTranslation(ships[ships.Count - 1], MainForm.SHIP_CELL, shipsField);
                        ship = null;
                        DrawShip(ships[ships.Count - 1], field, scale, Color.DarkGray);
                        DrawController.FieldInitialize(ref AddShipPB, 5);
                    }
                    else MessageBox.Show("Error!");

                }
                if (ship == null && CheckedShipIsExist())
                {
                    for (; i < ships.Count; i++)
                    {
                        if (ships[i].isChoosed) break;
                    }
                    Ship.ShipTranslation(ships[i], 0, shipsField);
                    tempShip = new Ship(ships[i].decksCount, ships[i].Orientation, new Point(Convert.ToInt32(e.X / scale), Convert.ToInt32(e.Y / scale)));
                    tempShip.DefShipCoord(tempShip.startPoint);
                    for (int k = 0; k < tempShip.points.Count; k++)
                    {
                        allowesPutShip = Ship.AllowPutShip(tempShip, k, shipsField);
                        if (allowesPutShip == false) break;
                    }
                    if (allowesPutShip)
                    {
                        DrawShip(ships[i], field, scale, Color.White);
                        Ship.ShipTranslation(ships[i], MainForm.EMPTY_CELL, shipsField);
                        ships[i].startPoint = temp;
                        ships[i].DefShipCoord(ships[i].startPoint);
                        Ship.ShipTranslation(ships[i], MainForm.SHIP_CELL, shipsField);
                        DrawShip(ships[i], field, scale, Color.DarkGray);
                    }
                    else MessageBox.Show("Error!");
                }
            }
            else if (field.GetPixel(e.X, e.Y).ToArgb() == Color.DarkGray.ToArgb())
            {
                for (; i < ships.Count; i++)
                {
                    if (ShipCheckField(ships[i], temp))
                    {
                        ships[i].isChoosed = true;
                        DrawShip(ships[i], field, scale, Color.LightGreen);
                    }
                    else
                    {
                        ships[i].isChoosed = false;
                        DrawShip(ships[i], field, scale, Color.DarkGray);
                    }
                }
                ship = null;
                DrawController.FieldInitialize(ref AddShipPB, 5);
            }
            else if (field.GetPixel(e.X, e.Y).ToArgb() == Color.LightGreen.ToArgb())
            {
                for (; i < ships.Count; i++)
                {
                    if (ShipCheckField(ships[i], temp))
                    {
                        ships[i].isChoosed = false;
                        break;
                    }
                }
                DrawShip(ships[i], field, scale, Color.DarkGray);
            }
            Field.Image = field;
        }

        private void AddShipPB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (addField.GetPixel(e.X, e.Y).ToArgb() == Color.DarkGray.ToArgb() || addField.GetPixel(e.X, e.Y).ToArgb() == Color.LightGreen.ToArgb())
            {
                for (int i = 0; i < ships.Count; i++)
                {
                    ships[i].isChoosed = false;
                    DrawShip(ships[i], field, scale, Color.DarkGray);
                }
                Field.Image = field;

                ship.isChoosed = !ship.isChoosed;
                if (ship.isChoosed)
                    DrawShip(ship, addField, _scale, Color.LightGreen);
                else
                    DrawShip(ship, addField, _scale, Color.DarkGray);
            }
            AddShipPB.Image = addField;
        }

        private bool ShipCheckField(Ship ship, Point p)
        {
            bool res = false;
            for (int i = 0; i < ship.points.Count; i++)
            {
                if (ship.points[i] == p) res = true;
            }
            return res;
        }



        private void Auto_Click(object sender, EventArgs e)
        {
            {
                AddShip.Enabled = false;
                DeleteShip.Enabled = false;
                Auto.Enabled = false;
                ClearBut.Enabled = false;
                CompleteBut.Enabled = false;
            }
            autoPlaceTime = 0;
            auto = true;
            autoPlace.Enabled = true;
            BOT_Field.FieldAutoInit("PLAYER");
            while(MainForm.CheckField(shipsField, MainForm.SHIP_CELL) != 20) BOT_Field.FieldAutoInit("PLAYER");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (shipsField[i, j])
                    {
                        case MainForm.EMPTY_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    field.SetPixel(k, l, Color.White);
                                }
                            }
                            break;
                        case MainForm.SHIP_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    field.SetPixel(k, l, Color.DarkGray);
                                }
                            }
                            break;
                    }
                }
            }
            Field.Image = field;
            
        }
        private void Save()
        {
            if (auto)
            {
                MainForm.GetPlayerField(shipsField);
                Close();
            }
            else
            {
                if (ships.Count < 10) MessageBox.Show("Расставте все корабли!");
                else
                {
                    MainForm.GetPlayerField(shipsField);
                    Close();
                }
            }
        }

        private void CompleteBut_Click(object sender, EventArgs e)
        {
            Save();
        }

        private bool CheckedShipIsExist()
        {
            bool res = false;
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].isChoosed)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }
}
