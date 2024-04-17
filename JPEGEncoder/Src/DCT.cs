using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGEncoder
{
	internal class DCT
	{
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
	}
}
