using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace JPEGEncoder
{
	internal class Entropy
	{

		/// <summary>
		/// This function calculates the number of zeroes before each non zero value in a list of integers.
		/// The first value is not calculated. Also appends an EOB marker at the end.
		/// </summary>
		/// <param name="coef"></param>
		/// <returns></returns>
		public static List<Tuple<int,int>> RunLengthEncode(List<int> coef) {

			List<Tuple<int,int>> result = new List<Tuple<int,int>>();

			for (int i = 1; i < coef.Count; i++) {

				int n = 0;
				while (i < coef.Count - 1 && coef[i] == 0) {
					n++;
					i++;
				}

				if (coef[i] != 0) result.Add(new Tuple<int, int>(n, coef[i]));

			}

			result.Add(new Tuple<int,int> (0, 0));
			return result;
		}
	}
}
