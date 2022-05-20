using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BattleShip.GameModes
{
    internal class Game
    {
        private int[,] firstPlayerField;
        private int[,] secondPlayerField;
        private bool FirstPlayerStep, SecondPlayerStep;
        private static Random rnd;

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

        public bool PlayerOneStep(Point step)
        {
            if (secondPlayerField[step.Y, step.X] == MainForm.SHIP_CELL)
            {
                FirstPlayerStep=true;
                secondPlayerField[step.Y, step.X] = MainForm.HIT_CELL;
            }
            else
            {
                FirstPlayerStep=false;
            }
            return FirstPlayerStep;
        }

        public bool PlayerTwoStep(Point step)
        {
            if (firstPlayerField[step.Y, step.X] == MainForm.SHIP_CELL)
            {
                SecondPlayerStep = true;
                secondPlayerField[step.Y, step.X] = MainForm.HIT_CELL;
            }
            else
            {
                SecondPlayerStep = false;
            }
            return SecondPlayerStep;
        }
    }
}
