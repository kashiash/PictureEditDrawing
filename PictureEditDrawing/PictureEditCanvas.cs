using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureEditDrawing
{
    /// <summary>
    /// Extends the PictureEdit control to provide a simple drawing canvas
    /// </summary>
    [ToolboxItem(true)]
    [Description("Extends the PictureEdit control to provide a simple drawing canvas")]
    sealed public class PictureEditCanvas : PictureEdit
    {

        #region Private members

        /// <summary>
        /// PictureEdit popup menu
        /// </summary>
        private PictureMenu dxPopupMenu = null;

        /// <summary>
        /// Drawing mode flag
        /// </summary>
        private bool _IsDrawingMode;

        /// <summary>
        /// Current Shape to be drawn onto this PictureEditCanvas
        /// </summary>
        private IDrawShapes CurrentShape = null;

        /// <summary>
        /// Collection of Shapes to be drawn onto this PictureEditCanvas
        /// </summary>
        private IList<IDrawShapes> Shapes = new List<IDrawShapes>();

        #endregion

        #region Public accessors

        /// <summary>
        /// Gets the Picture Menu for this PictureEditCanvas control
        /// </summary>
        protected override PictureMenu Menu
        {
            get { return dxPopupMenu; }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the PictureEditCanvas class
        /// </summary>
        public PictureEditCanvas()
            : base()
        {

        }



        /// <summary>
        /// On create control event handler
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            dxPopupMenu = CreatePictureMenu();

            //Add in our custom DXMenuItems
            DXMenuItem drawingTools = new DXMenuItem("Drawing Tools", new EventHandler(OnDrawingToolsClick));
            drawingTools.BeginGroup = true;
            drawingTools.Image = PictureEditDrawing.Properties.Resources.pictureshapeoutlinecolor_16x16;
            dxPopupMenu.Items.Add(drawingTools);

            DXMenuItem saveImage = new DXMenuItem("Save Image", new EventHandler(OnSaveImageClick));
            saveImage.Image = Menu.Items[6].Image;
            dxPopupMenu.Items.Add(saveImage);

        }   //End the OnCreateControl() method



        /// <summary>
        /// Item click event handler for the Drawing Tools DXMenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDrawingToolsClick(object sender, EventArgs e)
        {
            using (frmDrawingTool showDrawingTools = new frmDrawingTool(Image))
            {
                if (showDrawingTools.ShowDialog(Parent) == DialogResult.OK)
                {
                    //Change us into Drawing Mode
                    _IsDrawingMode = true;
                    Cursor = Cursors.Cross;
                    CurrentShape = showDrawingTools.SelectedShape;
                }

            }   //End the using() statement

        }   //End the OnDrawingToolsClick() method



        /// <summary>
        /// Item click event handler for the Save Image DXMenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveImageClick(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.DefaultExt = ".jpg";
                fileDialog.Filter = "JPEG Files|*.jpg";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                    SaveModifiedImage(fileDialog.FileName);

            }   //End the using() statement

        }   //End the OnSaveImageClick() method



        /// <summary>
        /// Saves the modified PictureEditCanvas image
        /// </summary>
        /// <param name="FileName">File name for the saved image</param>
        private void SaveModifiedImage(string FileName)
        {
            using (Bitmap imageBMP = new Bitmap(Width, Height))
            {
                DrawToBitmap(imageBMP, Bounds);
                imageBMP.Save(FileName);

            }   //End the using() statement

        }   //End the SaveModifiedImage() method



        /// <summary>
        /// On key down event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //See if we should cancel out of Drawing mode
            if (e.KeyCode == Keys.Escape && _IsDrawingMode == true)
            {
                _IsDrawingMode = false;
                CurrentShape = null;
                Cursor = Cursors.Default;
                Invalidate();
            }

        }   //End the OnKeyDown() method



        /// <summary>
        /// On mouse down event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_IsDrawingMode == false)
                return;

            if (IsDesignMode == false && Control.MouseButtons == MouseButtons.Left)
                CurrentShape.StartDrawing(e.Location);

        }   //End the OnMouseDown() method



        /// <summary>
        /// On mouse move event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_IsDrawingMode == false)
                return;

            if (IsDesignMode == false)
            {
                //Make sure the user is holding the button for Shapes/Drawings or is placing text
                if ((Control.MouseButtons == MouseButtons.Left || CurrentShape is TextStamp) && CurrentShape != null)
                {
                    CurrentShape.EndDrawing(e.Location);

                    //Force a re-draw of the Control
                    Invalidate();
                }
            }

        }   //End the OnMouseMove() method



        /// <summary>
        /// On mouse up event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_IsDrawingMode == false)
                return;

            if (IsDesignMode == false && CurrentShape != null)
            {
                //Add this Shape to our collection
                Shapes.Add(CurrentShape);
                CurrentShape = null;

                //Turn off Drawing Mode
                _IsDrawingMode = false;
                Cursor = Cursors.Default;
            }

        }   //End the OnMouseUp() method



        /// <summary>
        /// On paint event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (IsDesignMode == false)
            {
                //Draw the current Shape being placed by the user
                if (_IsDrawingMode == true && CurrentShape != null)
                    CurrentShape.Draw(e.Graphics);

                //Draw all the other Shapes
                foreach (IDrawShapes shape in Shapes)
                    shape.Draw(e.Graphics);
            }

        }   //End the OnPaint() method





    }   //End the PictureEditCanvas class
}   //End the PictureEditDrawing namespace
