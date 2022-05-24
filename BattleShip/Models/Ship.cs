using System.Collections.Generic;
using System.Drawing;

namespace BattleShip
{
    public class Ship
    {
        public Point startPoint;
        public int decksCount;
        public int Orientation;
        public bool isChoosed;
        public List<Point> points;
        private bool isDestroyed;
        private int wreckedDeskCount;
        private static Point destroyedCell = new Point(-1, -1);

        public Ship(int decksCount, int orientation, Point startPoint)
        {
            this.decksCount = decksCount;
            Orientation = orientation;
            this.startPoint = startPoint;
            isChoosed = false;
            points = new List<Point>();
            wreckedDeskCount = 0;
        }

        public bool IsDestroyed() => isDestroyed;

        public static int FindShip(List<Ship> ships, Point shoot)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                for (int j = 0; j < ships[i].points.Count; j++)
                {
                    if (ships[i].points[j] == shoot) return i;
                }
            }
            return -1;
        }

        public static int FindShipPoint(Ship ship, Point shoot)
        {
            for (int i = 0; i < ship.points.Count; i++)
            {
                if (ship.points[i] == shoot) return i;
            }
            return -1;
        }

        public static void DestroyShip(Ship ship) =>
            ship.isDestroyed = true ? (ship.wreckedDeskCount >= ship.decksCount) : false;

        public void DestroyDesk()
        {
            wreckedDeskCount++;
        }



        public void DefShipCoord(Point start)
        {
            points.Clear();
            //start = startPoint;
            if (Orientation == 0)
            {
                for (int i = 0; i < decksCount; i++)
                {
                    Point point = new Point(start.X + i, start.Y);
                    points.Add(point);
                }
            }
            else if (Orientation == 1)
            {
                for (int i = 0; i < decksCount; i++)
                {
                    Point point = new Point(start.X, start.Y + i);
                    points.Add(point);
                }
            }
        }

        public static bool AllowPutShip(Ship insertedShip, int index, int[,] field)
        {
            int X = insertedShip.points[index].X;
            int Y = insertedShip.points[index].Y;
            bool PutResult;

            if ((X > 0 && Y > 0) && (X < 9 && Y < 9))
            {
                if (field[Y - 1, X - 1] != MainForm.SHIP_CELL && field[Y + 1, X - 1] != MainForm.SHIP_CELL && field[Y - 1, X + 1] != MainForm.SHIP_CELL && field[Y + 1, X + 1] != MainForm.SHIP_CELL &&
                    field[Y - 1, X] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL && field[Y + 1, X] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 0 && Y == 0)
            {
                if (field[Y + 1, X + 1] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL && field[Y + 1, X] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 9 && Y == 9)
            {
                if (field[Y - 1, X - 1] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL && field[Y - 1, X] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 0 && Y == 9)
            {
                if (field[Y - 1, X + 1] != MainForm.SHIP_CELL && field[Y - 1, X] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 9 && Y == 0)
            {
                if (field[Y + 1, X - 1] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL && field[Y + 1, X] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 0 && Y > 0 && Y < 9)
            {
                if (field[Y - 1, X + 1] != MainForm.SHIP_CELL && field[Y + 1, X + 1] != MainForm.SHIP_CELL &&
                    field[Y - 1, X] != MainForm.SHIP_CELL && field[Y + 1, X] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (X == 9 && Y > 0 && Y < 9)
            {
                if (field[Y - 1, X - 1] != MainForm.SHIP_CELL && field[Y + 1, X - 1] != MainForm.SHIP_CELL &&
                    field[Y - 1, X] != MainForm.SHIP_CELL && field[Y + 1, X] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (Y == 0 && X > 0 && X < 9)
            {
                if (field[Y + 1, X + 1] != MainForm.SHIP_CELL && field[Y + 1, X - 1] != MainForm.SHIP_CELL &&
                    field[Y + 1, X] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else if (Y == 9 && X > 0 && X < 9)
            {
                if (field[Y - 1, X + 1] != MainForm.SHIP_CELL && field[Y - 1, X - 1] != MainForm.SHIP_CELL &&
                    field[Y - 1, X] != MainForm.SHIP_CELL && field[Y, X - 1] != MainForm.SHIP_CELL && field[Y, X + 1] != MainForm.SHIP_CELL)
                    PutResult = true;
                else
                    PutResult = false;
            }
            else PutResult = false;
            return PutResult;
        }

        public static void ShipTranslation(Ship ship, int mode, int[,] field)
        {
            for (int i = 0; i < ship.points.Count; i++)
            {
                if (mode == 1) field[ship.points[i].Y, ship.points[i].X] = MainForm.SHIP_CELL;
                else field[ship.points[i].Y, ship.points[i].X] = MainForm.EMPTY_CELL;
            }
        }
    }
}
