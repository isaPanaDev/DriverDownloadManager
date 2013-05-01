using System;
using System.Drawing;
using System.Windows.Forms;

namespace DriverDownloader
{
	/// <summary>
	/// Summary description for SampleImages.
	/// </summary>
	public class SampleImages
	{
		public readonly static string ModelIcon;
        public readonly static Image DeleteIcon;
        public readonly static Image DownIcon;
        public readonly static Image UpIcon;
        public readonly static Image PauseIcon;
        public readonly static Image ResetIcon;
        private const string prefix = "Images/";

		static SampleImages()
		{
            try
            {
                DeleteIcon = System.Drawing.Image.FromFile(prefix + "toolRemove.Image.png");
                DownIcon = Image.FromFile(prefix + "toolMoveSelectionsDown.Image.png");
                UpIcon = Image.FromFile(prefix + "toolMoveSelectionsUp.Image.png");
                PauseIcon = Image.FromFile(prefix + "toolPause.Image.png");
                ResetIcon = Image.FromFile(prefix + "toolStart.Image.png");

                ModelIcon = prefix + "modelmark.png";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image Error! Message: " + ex.Message);
            }

		}
	}
}
