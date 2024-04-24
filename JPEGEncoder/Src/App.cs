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

				// Divide the image into a list of 8 x 8 blocks (minimum coded units)
				List<int[,]> blocks = DCT.DivideToBlocks(img);

				// Declaring a new list to hold the calculated coefficient values
				List<double[,]> coefficients = new List<double[,]>();

				// Calculating coefficients
				foreach (int[,] block in blocks) coefficients.Add(DCT.CalculateDCTCoefficients(block));

				// Declaring a new list to hold the quantized coefficient values
				List<int[,]> quantcblocks = new List<int[,]>();

				// Quantization of DCT coefficients
				foreach (double[,] coef in coefficients) quantcblocks.Add(DCT.QuantizeCoefficients(coef));

				// List of lists to hold the integers from the zig-zag traversal of the array
				List<List<int>> zigzag = new List<List<int>>();

				// Create a list of values by traversing the block in a zig-zag pattern
				foreach (int[,] quantcblock in quantcblocks) {
					zigzag.Add(Utils.ZigZagTraverse(quantcblock));
				}

				// Initialize a list of lists containing the pairs from run-length encoding the last 63 AC coefficients in a block
				List<List<Tuple<int, int>>> pairs = new List<List<Tuple<int, int>>>();

				// Utilize run-length encoding on the list of coefficients
				// The first value (DC coefficient) is not encoded this way
				foreach(List<int> coef in zigzag) {
					pairs.Add(Entropy.RunLengthEncode(coef));
				}


			}
		}
	}
}
