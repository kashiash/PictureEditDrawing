using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEditDrawing
{
    sealed public class TextStamp : IDrawShapes
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the Fore Color of this Text Stamp
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Fill Color of this Text Stamp
        /// </summary>
        public Color FillColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Border Width of this Text Stamp
        /// </summary>
        public float BorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starting X co-ordinate for the Text Stamp
        /// </summary>
        public int StartX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starting Y co-ordinate for the Text Stamp
        /// </summary>
        public int StartY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Text string of this Text Stamp
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Text Font to be used for this Text Stamp
        /// </summary>
        public Font TextFont
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the TextStamp class
        /// </summary>
        public TextStamp()
        {

        }



        /// <summary>
        /// Starts this Text Stamp by registering the start location coordinates
        /// </summary>
        /// <param name="Location">Start location</param>
        public void StartDrawing(Point Location)
        {
            StartX = Location.X;
            StartY = Location.Y;

        }   //End the StartDrawing() method



        /// <summary>
        /// Ends this Text Stamp by registering the end location coordinates
        /// </summary>
        /// <param name="Location">End location</param>
        public void EndDrawing(Point Location)
        {
            StartX = Location.X;
            StartY = Location.Y;

        }   //End the EndDrawing() method



        /// <summary>
        /// Draws this Text Stamp
        /// </summary>
        /// <param name="graphics">Current Graphics context</param>
        public void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(ForeColor))
            {
                pen.Alignment = PenAlignment.Inset;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddString(Text, TextFont.FontFamily, (int)TextFont.Style, TextFont.Size, new Point(StartX, StartY), StringFormat.GenericDefault);
                    graphics.DrawPath(pen, path);

                    using (SolidBrush brush = new SolidBrush(FillColor))
                        graphics.FillPath(brush, path);

                }   //End the using() statement

            }   //End the using() statement

        }   //End the Draw() method



        /// <summary>
        /// Returns True if the provided text can fit on within the given image width, based on the text settings
        /// </summary>
        /// <param name="ImageWidth">Width of the Image to be test the text against</param>
        /// <param name="TextString">Text string to check</param>
        /// <param name="TextFont">Text font settings</param>
        /// <param name="graphics">Current Graphics context</param>
        /// <returns>True if the text will fit within the provided Image width</returns>
        static public bool CheckTextSizing(int ImageWidth, string TextString, Font TextFont, Graphics graphics)
        {
            int TextWidth = 0;

            TextWidth = Convert.ToInt32(graphics.MeasureString(TextString, TextFont).Width);

            return (TextWidth < ImageWidth);

        }   //End the CheckTextSizing() method


    }   //End the TextStamp class
}   //End the PictureEditDrawing namespace
