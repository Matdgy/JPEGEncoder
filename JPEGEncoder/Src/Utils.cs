using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGEncoder
{
	internal class Utils {
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

		public static int[,] CenterValues(int[,] img) {
			
			for(int i = 0; i < img.GetLength(0); i++) {
				for (int j = 0; j < img.GetLength(1); j++ ) {
					img[i, j] = img[i, j] - 128;
				}
			}

			return img;
		}
	}
}
