using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    sealed public class Rectangle : FillableShape
    {

        /// <summary>
        /// Initializes a new instance of the Rectangle class
        /// </summary>
        public Rectangle()
            : base()
        {

        }



        /// <summary>
        /// Draws this Rectangle
        /// </summary>
        /// <param name="graphics">Current Graphics context</param>
        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(ForeColor, BorderWidth))
            {
                pen.DashStyle = BorderStyle;
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                
                graphics.DrawRectangle(pen, 
                    Math.Min(StartX, EndX), 
                    Math.Min(StartY, EndY), 
                    Math.Abs(EndX - StartX), 
                    Math.Abs(EndY - StartY));

                graphics.FillRectangle(new SolidBrush(FillColor),
                    Math.Min(StartX + BorderWidth, EndX - BorderWidth),
                    Math.Min(StartY + BorderWidth, EndY - BorderWidth),
                    Math.Abs((EndX - BorderWidth) - (StartX + BorderWidth)),
                    Math.Abs((EndY - BorderWidth) - (StartY + BorderWidth)));

            }   //End the using() statement

        }   //End the Draw() method
            


    }   //End the Rectangle class
}   //End the PictureEditDrawing namespace
