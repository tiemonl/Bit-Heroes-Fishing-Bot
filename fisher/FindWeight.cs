using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace fisher {
	class FindWeight {
		//static void Main(string[] args) {
		//	Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
		//	Bitmap need = new Bitmap("..\\..\\..\\Weight\\zero.bmp");
		//	Bitmap hay = new Bitmap(".\\fisher\\bin\\Debug\\screenie.bmp");
		//	Point? pt = Find(hay, need);
		//	Console.WriteLine(pt.ToString());
		//	Console.ReadLine();
		//}
		public static Point? Find(Bitmap haystack, Bitmap needle) {
			if (null == haystack || null == needle) {
				return null;
			}
			if (haystack.Width < needle.Width || haystack.Height < needle.Height) {
				return null;
			}

			var haystackArray = GetPixelArray(haystack);
			var needleArray = GetPixelArray(needle);

			foreach (var firstLineMatchPoint in FindMatch(haystackArray.Take(haystack.Height - needle.Height), needleArray[0])) {
				if (IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint, 1)) {
					return firstLineMatchPoint;
				}
			}

			return null;
		}

		private static int[][] GetPixelArray(Bitmap bitmap) {
			var result = new int[bitmap.Height][];
			var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);

			for (int y = 0; y < bitmap.Height; ++y) {
				result[y] = new int[bitmap.Width];
				Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
			}

			bitmap.UnlockBits(bitmapData);

			return result;
		}
		private static IEnumerable<Point> FindMatch(IEnumerable<int[]> haystackLines, int[] needleLine) {
			var y = 0;
			foreach (var haystackLine in haystackLines) {
				for (int x = 0, n = haystackLine.Length - needleLine.Length; x < n; ++x) {
					if (ContainSameElements(haystackLine, x, needleLine, 0, needleLine.Length)) {
						yield return new System.Drawing.Point(x, y);
					}
				}
				y += 1;
			}
		}

		private static bool ContainSameElements(int[] first, int firstStart, int[] second, int secondStart, int length) {
			for (int i = 0; i < length; ++i) {
				if (i == 19) {
					System.Diagnostics.Debugger.Break();
				}
				if (second[i + secondStart].Equals(-1)) {
					continue;
				}
				if (first[i + firstStart] != second[i + secondStart]) {

					return false;
				}
			}
			return true;
		}

		private static bool IsNeedlePresentAtLocation(int[][] haystack, int[][] needle, Point point, int alreadyVerified) {
			//we already know that "alreadyVerified" lines already match, so skip them
			for (int y = alreadyVerified; y < needle.Length; ++y) {
				if (!ContainSameElements(haystack[y + point.Y], point.X, needle[y], 0, needle[0].Length)) {
					return false;
				}
			}
			return true;

		}
	}
}
