using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using DevExpress.XtraLayout.Utils;

namespace PictureEditDrawing
{
    public partial class frmDrawingTool : DevExpress.XtraEditors.XtraForm
    {

        #region Private members

        /// <summary>
        /// User-selected Shape
        /// </summary>
        private IDrawShapes _SelectedShape;

        /// <summary>
        /// Current PictureEditCanvas Image
        /// </summary>
        private Image _ParentCanvasImage;

        #endregion

        #region Public accessors

        /// <summary>
        /// Gets the user-selected Shape
        /// </summary>
        public IDrawShapes SelectedShape
        {
            get { return _SelectedShape; }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the frmDrawingTool class
        /// </summary>
        /// <param name="ParentCanvasImage"></param>
        public frmDrawingTool(Image ParentCanvasImage)
        {
            InitializeComponent();

            _ParentCanvasImage = ParentCanvasImage;
            
            //Populate our style ImageComboBoxEdit controls
            cboEndCap.Properties.Items.AddEnum<LineCap>();
            cboStartCap.Properties.Items.AddEnum<LineCap>();
            cboEndCap.SelectedIndex = cboStartCap.SelectedIndex = 0;

            cboBorderStyle.Properties.Items.AddEnum<DashStyle>();
            cboBorderStyle.EditValue = DashStyle.Solid;
        }



        /// <summary>
        /// Button checked event handler for the Tools button panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlTools_ButtonChecked(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            //Toggle Shape options based on the selected Shape
            if (pnlTools.Buttons["Line"].IsChecked == true)
            {
                ctrlBorderStyle.ContentVisible = true;
                ctrlFillColor.Visibility = ctrlText.Visibility = LayoutVisibility.Never;
                ctrlEndCap.Visibility = ctrlStartCap.Visibility = LayoutVisibility.Always;
                ctrlFont.Visibility = ctrlFontSize.Visibility = LayoutVisibility.Never;
                ctrlBold.Visibility = ctrlItalic.Visibility = ctrlUnderline.Visibility = LayoutVisibility.Never;
            }
            else if (pnlTools.Buttons["Ellipse"].IsChecked == true || pnlTools.Buttons["Rectangle"].IsChecked == true)
            {
                ctrlBorderStyle.ContentVisible = true;
                ctrlFillColor.Visibility = LayoutVisibility.Always;
                ctrlText.Visibility = LayoutVisibility.Never;
                ctrlEndCap.Visibility = ctrlStartCap.Visibility = LayoutVisibility.Never;
                ctrlFont.Visibility = ctrlFontSize.Visibility = LayoutVisibility.Never;
                ctrlBold.Visibility = ctrlItalic.Visibility = ctrlUnderline.Visibility = LayoutVisibility.Never;
            }
            else if (pnlTools.Buttons["Freehand"].IsChecked == true)
            {
                ctrlFillColor.Visibility = ctrlText.Visibility = LayoutVisibility.Never;
                ctrlBorderStyle.ContentVisible = false;
                ctrlEndCap.Visibility = ctrlStartCap.Visibility = LayoutVisibility.Never;
                ctrlFont.Visibility = ctrlFontSize.Visibility = LayoutVisibility.Never;
                ctrlBold.Visibility = ctrlItalic.Visibility = ctrlUnderline.Visibility = LayoutVisibility.Never;
            }
            else if (pnlTools.Buttons["Text"].IsChecked == true)
            {
                ctrlText.Visibility = LayoutVisibility.Always;
                ctrlFont.Visibility = ctrlFontSize.Visibility = LayoutVisibility.Always;
                ctrlBold.Visibility = ctrlItalic.Visibility = ctrlUnderline.Visibility = LayoutVisibility.Always;
                ctrlFillColor.Visibility = LayoutVisibility.Always;
                cboFillColor.Color = Color.Black;   //Default text to filled
                ctrlBorderStyle.ContentVisible = false;
                ctrlEndCap.Visibility = ctrlStartCap.Visibility = LayoutVisibility.Never;
            }            

        }   //End the pnlTools_ButtonChecked() method



        /// <summary>
        /// Checks to see if the Text entered will fit on the image given the selected parameters (font, size)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckTextSize_Click(object sender, EventArgs e)
        {
            using (Graphics graphics = CreateGraphics())
            {
                FontStyle style = FontStyle.Regular;

                if (btnBold.Checked)
                    style = FontStyle.Bold;
                if (btnItalic.Checked)
                    style = style | FontStyle.Italic;
                if (btnUnderline.Checked)
                    style = style | FontStyle.Underline;
                

                using (Font selectedFont = new Font(txtFont.Text, Convert.ToSingle(txtFontSize.Value), style))
                {
                    if (TextStamp.CheckTextSizing(Convert.ToInt32(_ParentCanvasImage.Width), txtText.Text, selectedFont, graphics) == false)
                    {
                        XtraMessageBox.Show("Warning: Your text will not fit on the current image with the current settings",
                            "Text Length Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }

                }   //End the using() statement

            }   //End the using() statement

        }   //End the btnCheckTextSize_Click() method



        /// <summary>
        /// Button click event handler for the Ok button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (pnlTools.Buttons["Line"].IsChecked.Value == true)
            {
                _SelectedShape = new Line();
                (_SelectedShape as Line).EndCap = (LineCap)cboEndCap.EditValue;
                (_SelectedShape as Line).StartCap = (LineCap)cboStartCap.EditValue;
                
            }
            else if (pnlTools.Buttons["Rectangle"].IsChecked.Value == true)
            {
                _SelectedShape = new Rectangle();
                (_SelectedShape as FillableShape).FillColor = cboFillColor.Color;
            }
            else if (pnlTools.Buttons["Ellipse"].IsChecked.Value == true)
            {
                _SelectedShape = new Ellipse();
                (_SelectedShape as FillableShape).FillColor = cboFillColor.Color;
            }
            else if (pnlTools.Buttons["Freehand"].IsChecked.Value == true)
            {
                _SelectedShape = new Freehand();
                (_SelectedShape as Freehand).Points = new List<Point>(50);
            }
            else if (pnlTools.Buttons["Text"].IsChecked.Value == true)
            {
                FontStyle style = FontStyle.Regular;

                if (btnBold.Checked)
                    style = FontStyle.Bold;
                if (btnItalic.Checked)
                    style = style | FontStyle.Italic;
                if (btnUnderline.Checked)
                    style = style | FontStyle.Underline;

                _SelectedShape = new TextStamp();
                (_SelectedShape as TextStamp).FillColor = cboFillColor.Color;
                (_SelectedShape as TextStamp).Text = txtText.Text;
                (_SelectedShape as TextStamp).TextFont = new Font(txtFont.Text, Convert.ToSingle(txtFontSize.Value), style);
            }

            _SelectedShape.BorderWidth = Convert.ToSingle(txtBorderWidth.Value);
            _SelectedShape.ForeColor = cboForeColor.Color;

            DialogResult = DialogResult.OK;
            Close();

        }   //End the btnOk_Click() method



        void spoo() { } 



    }   //End the frmDrawingTool class
}   //End the PictureEditDrawing namespace