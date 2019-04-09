using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    sealed public class Freehand : IDrawShapes
    {

        #region Public accessors

        /// <summary>
        /// Gets or sets the Fore Color of this Freehand drawing
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Border Width of this Freehand drawing
        /// </summary>
        public float BorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of Points that comprise this Freehand drawing
        /// </summary>
        public IList<Point> Points
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the Freehand class
        /// </summary>
        public Freehand()
        {
            ForeColor = Color.Black;
            BorderWidth = 1.0f;
        }



        /// <summary>
        /// Starts this drawing by registering the start location coordinates
        /// </summary>
        /// <param name="Location">Start location</param>
        public void StartDrawing(Point Location)
        {
            Points.Add(Location);

        }   //End the StartDrawing() method



        /// <summary>
        /// Ends this drawing by registering the end location coordinates
        /// </summary>
        /// <param name="Location">Start location</param>
        public void EndDrawing(Point Location)
        {
            Points.Add(Location);
                
        }   //End the EndDrawing() method



        /// <summary>
        /// Draws this Freehand drawing
        /// </summary>
        /// <param name="graphics">Current Graphic context</param>
        public void Draw(Graphics graphics)
        {
            if (Points == null || Points.Count < 2)
                return;
            
            for (int i = 1; i < Points.Count; i++)
            {
                using (Pen pen = new Pen(ForeColor, BorderWidth))
                {
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.DrawLine(pen, Points[i - 1], Points[i]);

                }   //End the using() statement
                    
            }   //End the for() loop

        }   //End the Draw() method



    }   //End the Freehand class
}   //End the PictureEditDrawing namespace
