using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    /// <summary>
    /// Represents a Shape that can be filled. This class cannot be instantiated.
    /// </summary>
    abstract public class FillableShape : Shape
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the fill color for this Shape
        /// </summary>
        public Color FillColor
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the FillableShape class
        /// </summary>
        public FillableShape()
            : base()
        {

        }

    }   //End the FillableShape class
}   //End the PictureEditDrawing namespace
