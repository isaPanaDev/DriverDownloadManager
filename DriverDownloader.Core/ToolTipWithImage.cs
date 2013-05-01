using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DriverDownloader.Core
{
    public class ToolTipWithImage : Control
    {
        private Image _img;
        private Control _ctl;
        private Timer _timer;
        string _imgfilename;
        string _tiptext;
        private const string message = "(A) Number: Prefixed by 'CF-'\n(B) Version: Sixth character ";
        
        #region Properties
        public String TipText { get { return _tiptext; } 
            set { 
                _tiptext = value;
                this.Size = new Size(200, 200); this.Font = new Font(FontFamily.GenericSerif, 9, FontStyle.Bold); 
            }
        }

        public String ImageFile
        {
            get { return _imgfilename; }
            set
            {
                if (_imgfilename == value) { }
                else
                {
                    _imgfilename = value;
                    try { _img = Image.FromFile(_imgfilename); this.Size = new Size(_img.Width, _img.Height+30); }
                    catch { _img = null; }
                }
            }
        }
        #endregion
       
        public ToolTipWithImage()
        {
            this.TipText = message;
           
            this.Location = new Point(0, 0);
            this.Visible = false;
            this.TabIndex = 0;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(ShowTipOff); // when timer elapsed, disable popup
        }

        public void SetToolTip(Control ctl)
        {
            _ctl = ctl;
            ctl.Parent.Controls.Add(this);
            ctl.Parent.Controls.SetChildIndex(this, 0);
            ctl.MouseMove += new MouseEventHandler(ShowTipOn);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_img != null)
            {
                Rectangle myTextRectangle = new Rectangle(40, 10, 7, 70);
                myTextRectangle.Location = new Point(0, 0);

                e.Graphics.DrawString(TipText, this.Font, Brushes.Blue, myTextRectangle.Width, myTextRectangle.Height); 
                e.Graphics.DrawImage(_img, 0, 0);
            }
        }

        public void ShowTipOn(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
            {
                _timer.Start();
                this.Left = _ctl.Left + e.X;
                this.Top = _ctl.Top + e.Y;
                this.Visible = true;
                this.TabIndex = 0;
                _ctl.Parent.Controls.SetChildIndex(this, 0);
                this.Parent.Controls.SetChildIndex(this, 0);
            }
        }

        public void ShowTipOff(Object sender, EventArgs e)
        {
            _timer.Stop();
            this.Visible = false;
        }
    }
}

 

