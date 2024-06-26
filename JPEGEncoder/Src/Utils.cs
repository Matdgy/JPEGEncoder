﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGEncoder
{
	internal class Utils {
		/// <summary>
		/// Converts the bitmap into an integer array. This function also converts the 3 color channels to 1 channel (greyscale)
		/// using the weighed sums of the color components according to the Luma coding.
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns>Array of integers in the range of 0 - 255</returns>
		public static int[,] ConvertBitmapToInt(Bitmap bitmap) {
			
			int[,] img = new int[bitmap.Width, bitmap.Height];

			for (int i = 0; i < bitmap.Width; i++) {
				for (int j = 0; j < bitmap.Height; j++) {

					Color act = bitmap.GetPixel(i, j);
					double greyscaleValue = act.R * 0.2989 + act.G * 0.5870 + act.B * 0.1140;

					img[i,j] = Convert.ToInt32(greyscaleValue) < 255 ? Convert.ToInt32(greyscaleValue) : 255;

				}
			}
			return img;
		}

		/// <summary>
		/// Adds padding to the integer based image representation by copying the rightmost values of the array to the new columns and rows.
		/// This results in an image representation where the number of rows and the number of columns are divisible by 8.
		/// </summary>
		/// <param name="img"></param>
		/// <returns>Array of integers in the range of 0 - 255 where n of rows and columns are divisible by 8</returns>
		public static int[,] AddPadding(int[,] img) {

			int addX = (8 - img.GetLength(0) % 8) < 8 ? 8 - img.GetLength(0) % 8 : 0;
			int addY = (8 - img.GetLength(1) % 8) < 8 ? 8 - img.GetLength(1) % 8 : 0;

			int[,] result = new int[img.GetLength(0) + addX, img.GetLength(1) + addY];

			if (addX != 0 || addY != 0) {

				for (int i = 0; i < img.GetLength(0); i++) {
					for (int j = 0; j < img.GetLength(1); j++) {
						result[i, j] = img[i, j];
					}
				}

				for (int j = 0; j < img.GetLength(1); j++) {
					for (int i = 1; i <= addX; i++) {
						result[img.GetLength(0) - 1 + i, j] = result[img.GetLength(0) - 1 + i - 1, j];
					}
				}

				for (int j = 0; j < result.GetLength(0); j++) {
					for (int i = 1; i <= addY; i++) {
						result[j, img.GetLength(1) - 1 + i] = result[j, img.GetLength(1) - 1 + i - 1];
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Centers values around 0. Resulted integeres are in the range of -128 to 127.
		/// </summary>
		/// <param name="img"></param>
		/// <returns>Array of integers in the range of -128 to 127</returns>
		public static int[,] CenterValues(int[,] img) {
			
			for(int i = 0; i < img.GetLength(0); i++) {
				for (int j = 0; j < img.GetLength(1); j++ ) {
					img[i, j] = img[i, j] - 128;
				}
			}

			return img;
		}

		/// <summary>
		/// Create a list (vector) of elements from an array by traversing the array in a zig-zag pattern.
		/// </summary>
		/// <param name="block"></param>
		/// <returns>List of integers from the block</returns>
		public static List<int> ZigZagTraverse(int[,] block) {

			List<int> result = new List<int>();
			bool reverse = false;

			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					result.Add(block[x, reverse ? 7 - y : y]);
				}
				reverse = !reverse;
			}

			return result;
		}
	}
}
