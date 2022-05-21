using BattleShip.AddShips;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BattleShip
{
    internal class BOT_Field
    {
        private static int[,] botField;
        private static Random rnd;
        private static List<Ship> BOTships_NotIns = new List<Ship>();
        private static List<Ship> BOTships_Ins = new List<Ship>();
        private static Point NULLpt = new Point();

        public BOT_Field() { }

        public static void FieldAutoInit(string mode)
        {
            int column;
            bool allowInsert = true;
            int index;
            rnd = new Random();

            ClearField();
            InitShipsList();
            while (BOTships_NotIns.Count != 0)
            {
                if (BOTships_NotIns.Count <= 1) index = 0;
                else index = rnd.Next(BOTships_NotIns.Count - 1);

                Ship ship = BOTships_NotIns[index];

                for (int row = 0; row < 10; row++)
                {
                    column = rnd.Next(0, 10);
                    ship.startPoint = new Point(row, column);
                    ship.DefShipCoord(ship.startPoint);
                    for (int j = 0; j < ship.points.Count; j++)
                    {
                        allowInsert = Ship.AllowPutShip(ship, j, botField);
                        if (!allowInsert) break;
                    }
                    if (allowInsert)
                    {
                        BOTships_Ins.Add(ship);
                        BOTships_NotIns.RemoveAt(index);
                        Ship.ShipTranslation(ship, MainForm.SHIP_CELL, botField);
                        break;
                    }
                }
            }
            switch (mode)
            {
                case "BOT":
                    MainForm.GetEnemyField(botField);
                    break;
                case "PLAYER":
                    AddShipsForm.GetField(botField);
                    //MainForm.GetPlayerField(botField);
                    AddShipsForm.GetShipsList(BOTships_Ins);
                    break;
            }
        }

        private static void ClearField()
        {
            botField = new int[10, 10];
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    botField[i, j] = 0;
                }
            }
        }

        private static void InitShipsList()
        {
            BOTships_NotIns.Clear();
            BOTships_Ins.Clear();
            for(int i = 0; i < 4; i++)
            {
                BOTships_NotIns.Add(new Ship(1, 0, NULLpt));
            }
            for (int i = 0; i < 3; i++)
            {
                BOTships_NotIns.Add(new Ship(2, rnd.Next(0, 2), NULLpt));
            }
            for (int i = 0; i < 2; i++)
            {
                BOTships_NotIns.Add(new Ship(3, rnd.Next(0, 2), NULLpt));
            }
            BOTships_NotIns.Add(new Ship(4, rnd.Next(0, 2), NULLpt));
        }
    }
}
