using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGEncoder
{
	internal class DCT
	{
		/// <summary>
		/// This function divides the integer array representing the image to 8x8 blocks.
		/// </summary>
		/// <param name="img"></param>
		/// <returns>List of 8x8 integer arrays</returns>
		public static List<int[,]> DivideToBlocks(int[,] img) {
			
			List<int[,]> blocks = new List<int[,]>();
		
			int x = img.GetLength(0) / 8;
			int y = img.GetLength(1) / 8;

			for(int i = 0; i < x; i++) {
				for (int j = 0; j < y; j++) {
					int[,] nextBlock = new int[8, 8];

					int k = 0;
					int l = 0;
					while ( k < 8 ) {
						while (l < 8) {
							nextBlock[k, l] = img[i * 8 + k, j * 8 + l];
							l++;
						}
						l = 0;
						k++;
					}

					blocks.Add(nextBlock);
				}
			}

			return blocks;
		}

		/// <summary>
		/// Calculates the Discrete Cosine Function coeffiecients of an 8x8 integer block.
		/// </summary>
		/// <param name="block"></param>
		/// <returns>8x8 array of double values representing the coeffiecients of the input block</returns>
		public static double[,] CalculateDCTCoefficients(int[,] block) {

			Func<int, double> scalingfunction = index => {
				double res = index == 0 ? 1 / Math.Sqrt(2) : 1;
				return res;
			};

			double[,] coef = new double[8, 8];

			for (int u = 0; u < 8; u++) {
				for (int v = 0; v < 8; v++) {

					double scale = 0.25d * scalingfunction(u) * scalingfunction(v);
					double sum = 0;

					for (int x = 0; x < 8; x++) {
						for (int y = 0; y < 8; y++) {
							sum += block[x, y] * Math.Cos((2 * x + 1) * u * Math.PI / 16) * Math.Cos((2 * y + 1) * v * Math.PI / 16);
						}
					}
					
					coef[u,v] = sum * scale;
				}
			}

			return coef;
		}

		/// <summary>
		/// This function calculates the quantized values of an 8x8 array of DCT coeffiecients.
		/// The quantization matrix used represents a 50% quality compression as specified in the original JPEG standard.
		/// </summary>
		/// <param name="coef"></param>
		/// <returns>Integer values of quantized coefficients</returns>
		public static int[,] QuantizeCoefficients(double[,] coef)
		{
			int[,] quantized = new int[8,8];
			int[,] quantmatrix = { { 16, 11, 10, 16, 24, 40, 51, 61 },
												{ 12, 12, 14, 19, 26, 58, 60, 55 },
												{ 14, 13, 16, 24, 40, 57, 69, 56 },
												{ 14, 17, 22, 29, 51, 87, 80, 62 },
												{ 18, 22, 37, 56, 68, 109, 103, 77 },
												{ 24, 35, 55, 64, 81, 104, 113, 92 },
												{ 49, 64, 78, 87, 103, 121, 120, 101 },
												{ 72, 92, 95, 98, 112, 100, 103, 99 }
			};

			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					quantized[i, j] = (int)(coef[i, j] / quantmatrix[i, j]);
				}
			}

			return quantized;
		}
	}
}
