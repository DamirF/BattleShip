using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BattleShip.bot
{
    public class BOT
    {
        private static Timer stepTime;
        private static int time;

        private int[,] field;
        private int missSteps, searchShip;
        private Random rnd;
        private List<Point> steps;
        private List<Point> availableSteps;
        private bool isHit, wreckedShipIsExist, changeRotation;
        private List<Ship> playerShips;
        private List<Point> wreckedShipPoints = new List<Point>();
        private int IndexOfShip;
        private readonly Point destroyCell = new Point(-1, -1);

        public BOT(List<Ship> ships)
        {
            field = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = 0;
                }
            }
            missSteps = searchShip = 0;
            rnd = new Random();
            steps = new List<Point>();
            availableSteps = new List<Point>();
            playerShips = ships;

            for (int i = 0; i < playerShips.Count; i++)
            {
                for (int j = 0; j < playerShips[i].points.Count; j++)
                {
                    field[playerShips[i].points[j].Y, playerShips[i].points[j].X] = 1;
                }
            }

            isHit = wreckedShipIsExist = changeRotation = false;

            time = 0;
            stepTime = new Timer();
            stepTime.Interval = 1000;
        }

        private List<Point> CheckStep(Point step)
        {
            List<Point> points = new List<Point>();
            switch (step.X)
            {
                case 0:
                    switch (step.Y)
                    {
                        case 0:
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y + 1));
                            break;
                        case 9:
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y - 1));
                            break;
                        default:
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y - 1));
                            points.Add(new Point(step.X, step.Y + 1));
                            break;
                    }
                    break;
                case 9:
                    switch (step.Y)
                    {
                        case 0:
                            points.Add(new Point(step.X - 1, step.Y));
                            points.Add(new Point(step.X, step.Y + 1));
                            break;
                        case 9:
                            points.Add(new Point(step.X - 1, step.Y));
                            points.Add(new Point(step.X, step.Y - 1));
                            break;
                        default:
                            points.Add(new Point(step.X - 1, step.Y));
                            points.Add(new Point(step.X, step.Y + 1));
                            points.Add(new Point(step.X, step.Y - 1));
                            break;
                    }
                    break;
                default:
                    switch (step.Y)
                    {
                        case 0:
                            points.Add(new Point(step.X - 1, step.Y));
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y + 1));
                            break;
                        case 9:
                            points.Add(new Point(step.X - 1, step.Y));
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y - 1));
                            break;
                        default:
                            points.Add(new Point(step.X, step.Y - 1));
                            points.Add(new Point(step.X + 1, step.Y));
                            points.Add(new Point(step.X, step.Y + 1));
                            points.Add(new Point(step.X - 1, step.Y));
                            break;
                    }
                    break;
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (field[points[i].Y, points[i].X] == MainForm.MISS_CELL)
                    points.RemoveAt(i);
            }

            return points;
        }

        private bool StepResult(Point step)
        {
            bool result = false;
            if (field[step.Y, step.X] == 1) result = true;
            return result;
        }

        private Point ChooseStep()
        {
            Point step;
            do
            {
                step = new Point(rnd.Next(0, 10), rnd.Next(0, 10));
            } while (field[step.Y, step.X] == MainForm.HIT_CELL ||
            field[step.Y, step.X] == MainForm.MISS_CELL);
            return step;
        }

        public Point Step()
        {
            Point step = new Point();
            Ship wreckedShip; // подбитый корабль
            Point lastStep; // последний ход
            if (wreckedShipIsExist || isHit)
            {
                if (isHit)
                {
                    lastStep = steps[steps.Count - 1];
                    wreckedShipPoints.Add(lastStep);
                }
                else
                {
                    lastStep = wreckedShipPoints[wreckedShipPoints.Count - 1];
                }

                IndexOfShip = Ship.FindShip(playerShips, lastStep);
                wreckedShip = playerShips[IndexOfShip];

                if (isHit)
                {
                    wreckedShip.DestroyDesk();
                    Ship.DestroyShip(wreckedShip);
                }

                if (wreckedShip.IsDestroyed())
                {
                    wreckedShipIsExist = false;
                    missSteps = searchShip = 0;
                    wreckedShipPoints.Clear();
                    steps.Clear();
                    step = ChooseStep();
                }
                else
                {
                    wreckedShipIsExist = true;
                    if (wreckedShipPoints.Count == 1)
                    {
                        availableSteps = CheckStep(lastStep);
                        step = availableSteps[missSteps];
                        missSteps++;
                    }
                    else
                    {
                        Point firstPoint = wreckedShipPoints[0];
                        Point lastPoint = wreckedShipPoints[wreckedShipPoints.Count - 1];
                        switch (wreckedShip.Orientation)
                        {
                            case 0: // h
                                if (lastPoint.X - firstPoint.X >= 0 && lastPoint.X < 9 && isHit)
                                {
                                    step = new Point(lastPoint.X + 1, lastPoint.Y);
                                }
                                else if(lastPoint.X - firstPoint.X < 0 && isHit)
                                {
                                    step = new Point(lastPoint.X - 1, lastPoint.Y);
                                }
                                else if (!isHit)
                                {
                                    step = new Point(firstPoint.X - 1, firstPoint.Y);
                                }
                                break;
                            case 1: // v
                                if (lastPoint.Y - firstPoint.Y >= 0 && lastPoint.Y < 9 && isHit)
                                {
                                    step = new Point(lastPoint.X, lastPoint.Y + 1);
                                }
                                //else if (lastPoint.Y - firstPoint.Y == -1)
                                //{
                                //    step = new Point(firstPoint.X, firstPoint.Y - 1);
                                //}
                                else if(lastPoint.Y - firstPoint.Y < 0 && isHit)
                                {
                                    step = new Point(lastPoint.X, lastPoint.Y - 1);
                                }
                                else if (!isHit)
                                {
                                    step = new Point(firstPoint.X, firstPoint.Y + 1);
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                step = ChooseStep();
                steps.Clear();
            }

            steps.Add(step);
            isHit = StepResult(step);
            return step;
        }
    }
}
