using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BattleShip
{
    public class DrawController
    {
        private static Graphics graphics;
        private static Pen fieldBorder = new Pen(Color.Black, 2f);
        private static Pen GamePen = new Pen(Color.Red, 2f);
        public static void FieldInitialize(ref PictureBox PB, int scale)
        {
            graphics = Graphics.FromImage(PB.Image);
            graphics.Clear(Color.White);
            for (int k = 0; k < PB.Width / 10; k++)
            {
                graphics.DrawLine(fieldBorder, new Point(PB.Width * k / scale, 0), new Point(PB.Width * k / scale, PB.Height));
                graphics.DrawLine(fieldBorder, new Point(0, PB.Height * k / scale), new Point(PB.Width, PB.Height * k / scale));
            }
            PB.Invalidate();
        }

        public static void DrawField(PictureBox PB, Bitmap map, int[,] playerField)
        {
            int scale = PB.Width / 10;
            int indent = scale / 2;
            graphics = Graphics.FromImage(PB.Image);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (playerField[i, j])
                    {
                        case MainForm.EMPTY_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    map.SetPixel(k, l, Color.White);
                                }
                            }
                            break;
                        case MainForm.SHIP_CELL:
                            for (int k = j * scale + 1; k < (j + 1) * scale - 1; k++)
                            {
                                for (int l = i * scale + 1; l < (i + 1) * scale - 1; l++)
                                {
                                    map.SetPixel(k, l, Color.DarkGray);
                                }
                            }
                            break;
                        case MainForm.MISS_CELL:
                            GamePen.Color = Color.Black;
                            graphics.DrawEllipse(GamePen, new Rectangle(
                                j * scale + 1 + indent / 2, i * scale + 1 + indent / 2,
                                indent, indent));
                            break;
                        case MainForm.HIT_CELL:
                            GamePen.Color = Color.Red;
                            graphics.DrawLine(GamePen, new Point(j * scale - 1, i * scale - 1), new Point((j + 1) * scale - 1, (i + 1) * scale - 1));
                            graphics.DrawLine(GamePen, new Point((j + 1) * scale - 1, i * scale - 1), new Point((j) * scale - 1, (i + 1) * scale - 1));
                            break;
                    }
                }
            }
            PB.Image = map;
        }
    }
}
