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

		public static List<Tuple<int,int>> RunLengthEncode(List<int> coef) {

			List<Tuple<int,int>> result = new List<Tuple<int,int>>();
			int n = 0;

			for (int i = 1; i < coef.Count; i++) {

				while (coef[i + n] == 0) {
					n++;
				}
				result.Add(new Tuple<int, int>(n, coef[i+n]));
				i = i + n;
				n = 0;
			}

			return result;
		}
	}
}
