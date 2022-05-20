using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BattleShip.bot
{
    /*
    посмотришь как я в классе BOT_Field сделал
    основная функция которая будет отвечать за ход бота public static
    остальные вспомогательные private static
     */
    public class BOT
    {
        private static Timer stepTime;
        private static int time;

        private int[,] field;
        private int missSteps, searchShip;
        private Random rnd;
        private List<Point> steps;
        private List<Point> availableSteps;
        private bool isHit, wreckedShipIsExist;
        private List<Ship> playerShips;
        private static List<Point> wreckedShipPoints = new List<Point>();
        private static int IndexOfShip;
        private static Point destroyCell = new Point(-1, -1);

        public BOT(List<Ship> ships)
        {
            field = new int[10, 10];
            missSteps = searchShip = 0;
            rnd = new Random();
            steps = new List<Point>();
            availableSteps = new List<Point> ();
            playerShips = ships;
            for(int i = 0; i < playerShips.Count; i++)
            {
                Ship.ShipTranslation(playerShips[i], 1, field);
            }
            isHit = wreckedShipIsExist = false;

            time = 0;
            stepTime = new Timer();
            stepTime.Interval = 1000;
            stepTime.Tick += StepTime_Tick;
        }

        private void StepTime_Tick(object sender, EventArgs e)
        {
            time++;
            if(time >= 3)
            {

            }
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
                            points.Add(new Point(step.Y, step.X + 1));
                            points.Add(new Point(step.Y + 1, step.X));
                            break;
                        case 9:
                            points.Add(new Point(step.Y, step.X + 1));
                            points.Add(new Point(step.Y - 1, step.X));
                            break;
                        default:
                            points.Add(new Point(step.Y, step.X + 1));
                            points.Add(new Point(step.Y + 1, step.X));
                            points.Add(new Point(step.Y - 1, step.X));
                            break;
                    }
                    break;
                case 9:
                    switch (step.Y)
                    {
                        case 0:
                            points.Add(new Point(step.Y, step.X - 1));
                            points.Add(new Point(step.Y + 1, step.X));
                            break;
                        case 9:
                            points.Add(new Point(step.Y, step.X - 1));
                            points.Add(new Point(step.Y - 1, step.X));
                            break;
                        default:
                            points.Add(new Point(step.Y, step.X - 1));
                            points.Add(new Point(step.Y + 1, step.X));
                            points.Add(new Point(step.Y - 1, step.X));
                            points.Add(new Point(step.Y, step.X + 1));
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
            } while (field[step.Y, step.X] != MainForm.EMPTY_CELL);
            return step;
        }

        private string CheckRotation(ref List<Point> points)
        {
            if (points.Count < 2) return "";
            else
            {
                if (points[points.Count - 1].X == points[points.Count - 2].X)
                    return "vertical";
                else return "horizontal";
            }
        }

        public Point Step()
        {
            Point step = new Point();
            Ship wreckedShip; // подбитый корабль
            Point lastStep; // последний ход
            if (wreckedShipIsExist || isHit)
            {
                if(isHit)
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

                if(isHit)
                {
                    wreckedShip.points[Ship.FindShipPoint(wreckedShip, lastStep)] = destroyCell;
                    Ship.DestroyShip(wreckedShip);
                }  
                
                if(wreckedShip.IsDestroyed())
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
                    if(wreckedShipPoints.Count == 1)
                    {
                        availableSteps = CheckStep(lastStep);
                        step = availableSteps[missSteps];
                        missSteps++;
                    }
                    else
                    {
                        switch(CheckRotation(ref wreckedShipPoints))
                        {
                            case "vertical":
                                if((isHit && lastStep.Y < 9) && 
                                    lastStep.Y - wreckedShipPoints[wreckedShipPoints.Count - 1 - searchShip].Y >= 1)
                                {
                                    step = new Point(lastStep.Y + 1, lastStep.X);
                                }
                                else
                                {
                                    lastStep = wreckedShipPoints[wreckedShipPoints.Count - 1 - searchShip];
                                    step = new Point(lastStep.Y - 1, lastStep.X);
                                }
                                break;
                            case "horizontal":
                                if (isHit && lastStep.X < 9 &&
                                    lastStep.X - wreckedShipPoints[wreckedShipPoints.Count - 1 - searchShip].X >= 1)
                                {
                                    step = new Point(lastStep.Y, lastStep.X + 1);
                                }
                                else
                                {
                                    lastStep = wreckedShipPoints[wreckedShipPoints.Count - 1 - searchShip];
                                    step = new Point(lastStep.Y, lastStep.X - 1);
                                }
                                break;
                        }
                        searchShip++;
                    }
                }
            }
            else
            {
                step = ChooseStep();
            }

            steps.Add(step);
            isHit = StepResult(step);
            return step;
        }
    }


}
