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

				// Cosine wave have values in the range of -1 to 1
				// Center the values so they fall in the range of -128 to 127
				img = Utils.CenterValues(img);

				// Divide the image into a list of 8 x 8 blocks
				List<int[,]> blocks = DCT.DivideToBlocks(img);

				// Declaring a new list to hold the calculated coefficient values
				List<double[,]> coefficients = new List<double[,]>();

				// Calculating coefficients
				foreach (int[,] block in blocks) {
					coefficients.Add(DCT.CalculateDCTCoefficients(block));
				}

			}
		}
	}
}
