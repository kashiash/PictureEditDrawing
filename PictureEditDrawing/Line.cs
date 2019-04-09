using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    sealed public class Line : Shape
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the start cap style for this Line
        /// </summary>
        public LineCap StartCap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end cap style for this Line
        /// </summary>
        public LineCap EndCap
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the Line class
        /// </summary>
        public Line()
            : base()
        {
            EndCap = LineCap.Flat;
            StartCap = LineCap.Flat;
        }



        /// <summary>
        /// Draws this Line
        /// </summary>
        /// <param name="graphics">Current Graphics context</param>
        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(ForeColor, BorderWidth))
            {
                pen.EndCap = EndCap;
                pen.StartCap = StartCap;
                pen.DashStyle = BorderStyle;

                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawLine(pen, StartX, StartY, EndX, EndY);

            }   //End the using() statement

        }   //End the Draw() method




    }   //End the Line class
}   //End the PictureEditDrawing namespace
