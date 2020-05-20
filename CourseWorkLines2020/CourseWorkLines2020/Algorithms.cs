using System;
using System.Collections;
using System.Drawing;

namespace CourseWorkLines2020
{
	static class Algorithms
	{
		public static int[] SplitNumber(int n, int size)
		{
			int[] number = new int[size];
			while (n > 0)
			{
				if (size == 0) break;
				number[--size] = n % 10;
				n = n / 10;
			}

			while (size > 0)
			{
				number[--size] = 0;
			}

			return number;
		}

		public static int[,] CreateNewFieldMatrix()
		{
			Random random = new Random();
			int[,] a = new int[9, 9];
			int i, j;

			for (i = 0; !(i >= 9); i++)
			{
				for (j = 0; !(j >= 9); j++)
				{
					a[i, j] = 0;
				}
			}

			var count = 81; // 9*9
			do
			{
				var remain = random.Next(count--) + 1;
				var stop = false;
				for (i = 0; i < 9; i++)
				{
					if (!stop)
						for (j = 0; !(j >= 9); j++)
						{
							if (a[i, j] != 0) continue;
							remain--;
							if (remain != 0) continue;
							a[i, j] = random.Next(7) + 1;
							stop = true;
							break;
						}
					else
						break;
				}
			} while (count > 76);

			AddNextColour(a);
			return a;
		}

		public static void AddNextColour(int[,] a)
		{
			Random random = new Random();
			int count = CountBlank(a);
			int tmp;

			for (tmp = 0; 3 > tmp; tmp++)
			{
				var remain = random.Next(count--) + 1;
				var stop = false;
				int i;
				for (i = 0; i < 9; i++)
				{
					if (stop)
					{
						break;
					}

					int j;
					for (j = 0; !(j >= 9); j++)
					{
						if (a[i, j] != 0)
						{
							continue;
						}

						remain--;
						if (remain != 0)
						{
							continue;
						}

						a[i, j] = -(random.Next(7) + 1);
						stop = true;
						break;
					}
				}
			}
		}

		public static int CountBlank(int[,] a)
		{
			int i;
			int count = 0;
			for (i = 0; !(i >= 9); i++)
			{
				int j;
				for (j = 0; !(j >= 9); j++)
				{
					if (0 < a[i, j]) continue;
					count++;
				}
			}

			return count;
		}

		public static ArrayList BuildPath(int[,] a, int i1, int j1, int i2, int j2)
		{
			int[,] startI = new int[9, 9];
			int[,] startJ = new int[9, 9];
			int[] chainI = new int[81];
			int[] chainJ = new int[81];

			int first = 0, last = 0;
			int x, y;

			for (x = 0; !(x >= 9); x++)
			{
				for (y = 0; !(y >= 9); y++)
				{
					startI[x, y] = -1;
				}
			}

			chainI[0] = i2;
			chainJ[0] = j2;
			startI[i2, j2] = -2;

			while (first <= last)
			{
				x = chainI[first];
				y = chainJ[first];
				first++;
				if (y > 0)
				{
					if (x == i1 & y - 1 == j1)
					{
						startI[i1, j1] = x;
						startJ[i1, j1] = y;
						return CreatePath(startI, startJ, i1, j1);
					}

					if (startI[x, y - 1] == -1 && a[x, y - 1] <= 0)
					{
						last++;
						chainI[last] = x;
						chainJ[last] = y - 1;
						startI[x, y - 1] = x;
						startJ[x, y - 1] = y;
					}
				}

				if (y < 8)
				{
					if (!(x == i1 & y + 1 == j1))
					{
						if (startI[x, y + 1] == -1 && a[x, y + 1] <= 0)
						{
							last++;
							chainI[last] = x;
							chainJ[last] = y + 1;
							startI[x, y + 1] = x;
							startJ[x, y + 1] = y;
						}
					}
					else
					{
						startI[i1, j1] = x;
						startJ[i1, j1] = y;
						return CreatePath(startI, startJ, i1, j1);
					}
				}

				if (x > 0)
				{
					if (x - 1 == i1 & y == j1)
					{
						startI[i1, j1] = x;
						startJ[i1, j1] = y;
						return CreatePath(startI, startJ, i1, j1);
					}

					if (startI[x - 1, y] == -1 && a[x - 1, y] <= 0)
					{
						last++;
						chainI[last] = x - 1;
						chainJ[last] = y;
						startI[x - 1, y] = x;
						startJ[x - 1, y] = y;
					}
				}

				if (x >= 8) continue;
				if (x + 1 == i1 & y == j1)
				{
					startI[i1, j1] = x;
					startJ[i1, j1] = y;
					return CreatePath(startI, startJ, i1, j1);
				}

				if (startI[x + 1, y] != -1) continue;
				if (a[x + 1, y] > 0) continue;
				last++;
				chainI[last] = x + 1;
				chainJ[last] = y;
				startI[x + 1, y] = x;
				startJ[x + 1, y] = y;
			}

			return null;
		}

		private static ArrayList CreatePath(int[,] startI, int[,] startJ, int i1, int j1)
		{
			ArrayList arr = new ArrayList();
			while (true)
			{
				arr.Add(new Point(i1, j1));
				var k = i1;
				i1 = startI[i1, j1];
				if (i1 != -2)
					j1 = startJ[k, j1];
				else
					break;
			}

			return arr;
		}

		public static ArrayList LinesCheck(int[,] a, int iCenter, int jCenter)
		{
			ArrayList list = new ArrayList();

			int[] u = {0, 1, 1, 1};
			int[] v = {1, 0, -1, 1};
			for (var t = 4 - 1; t >= 0; t--)
			{
				var k = 0;
				var i = iCenter;
				var j = jCenter;
				while (true)
				{
					i += u[t];
					j += v[t];
					if (!IsInner(i, j))
						break;
					if (a[i, j] != a[iCenter, jCenter])
						break;
					k++;
				}

				i = iCenter;
				j = jCenter;
				while (true)
				{
					i -= u[t];
					j -= v[t];
					if (!IsInner(i, j))
						break;
					if (a[i, j] != a[iCenter, jCenter])
						break;
					k++;
				}

				k++;
				if (k < 5) continue;
				while (k-- > 0)
				{
					i += u[t];
					j += v[t];
					if (i != iCenter || j != jCenter)
						list.Add(new Point(i, j));
				}
			}

			if (list.Count > 0) list.Add(new Point(iCenter, jCenter));
			else list = null;
			return list;
		}

		public static ArrayList Merge(ArrayList l1, ArrayList l2)
		{
			for (var index = l2.Count - 1; index >= 0; index--)
			{
				var p = (Point) l2[index];
				if (!IsPointExist(l1, p))
					l1.Add(p);
			}

			return l1;
		}

		private static bool IsPointExist(ArrayList l, Point point)
		{
			for (var index = l.Count - 1; index >= 0; index--)
			{
				var pp = (Point) l[index];
				if (pp.X == point.X && pp.Y == point.Y)
					return true;
			}

			return false;
		}

		private static bool IsInner(int i, int j)
		{
			var isInsideL = !(i < 0 || i >= 9);
			var isInsideR = !(j < 0 || j >= 9);
			return isInsideL && isInsideR;
		}
	}
}
