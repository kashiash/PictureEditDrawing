using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    sealed public class Ellipse : FillableShape
    {

        /// <summary>
        /// Initializes a new instance of the Ellipse class
        /// </summary>
        public Ellipse()
            : base()
        {

        }



        /// <summary>
        /// Draws this Ellipse
        /// </summary>
        /// <param name="graphics">Current Graphics context</param>
        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(ForeColor, BorderWidth))
            {
                pen.DashStyle = BorderStyle;
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                //Anti-alias the ellipse for the best look
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                graphics.DrawEllipse(pen, StartX / 2, StartY / 2, EndX, EndY);
                                
                graphics.FillEllipse(new SolidBrush(FillColor), StartX / 2, StartY / 2, EndX, EndY);

            }   //End the using() statement

        }   //End the Draw() method



    }   //End the Shape class
}   //End the PictureEditDrawing namespace
