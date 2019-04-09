using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    /// <summary>
    /// Serves as a base class for all Shape types. This class cannot be instantiated.
    /// </summary>
    abstract public class Shape : IDrawShapes
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the starting X co-ordinate for the Shape
        /// </summary>
        public int StartX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starting Y co-ordinate for the Shape
        /// </summary>
        public int StartY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ending X co-ordinate for the Shape
        /// </summary>
        public int EndX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ending Y co-ordinate for the Shape
        /// </summary>
        public int EndY
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the ForeColor of this Shape
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Border Width of this Shape
        /// </summary>
        public float BorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Border Style of this Shape
        /// </summary>
        public DashStyle BorderStyle
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the Shape class
        /// </summary>
        public Shape()
        {
            BorderWidth = 1.0f;
            ForeColor = Color.Black;            
        }


        /// <summary>
        /// Draws this Shape
        /// </summary>
        /// <param name="graphics">Current Graphics context</param>
        public abstract void Draw(Graphics graphics);



        /// <summary>
        /// Starts this drawing by registering the start location coordinates
        /// </summary>
        /// <param name="Location">Start location</param>
        public void StartDrawing(Point Location)
        {
            StartX = Location.X;
            StartY = Location.Y;

        }   //End the StartDrawing() method



        /// <summary>
        /// Ends this drawing by registering the end location coordinates
        /// </summary>
        /// <param name="Location">End location</param>
        public void EndDrawing(Point Location)
        {
            EndX = Location.X;
            EndY = Location.Y;

        }   //End the EndDrawing() method



    }   //End the Shape class
}   //End the PictureEditDrawing namespace
