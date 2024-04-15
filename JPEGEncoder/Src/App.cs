using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JPEGEncoder
{
	internal class App {
		static public void Main(string[] args) {
			foreach (string arg in args) {

				// Read file
				Bitmap bitmap = new Bitmap(arg);

				// Convert the bitmap to a 2d integer array
				// This function also converts the ARGB values to greyscale
				int[,] img = Utils.ConvertBitmapToInt(bitmap);

				// DCT requires an image which has its width and height divisible by 8
				// Extend the image by repeating the last pixels
				img = Utils.AddPadding(img);

			}
		}
	}
}
