using System.Drawing;

namespace CourseWorkLines2020
{
	class PublicFields
	{
		public static string Gamefield = "field.bmp";
		public static string Gameobjects = "pieces.bmp"; //balls
		public static string Colourrange = "colourRange.bmp";
		public static string Clock = "clock.bmp";
		public static string Scores = "scores.bmp";

		public static Size FieldSize = new Size(410, 461);
		public static Size PieceSize = new Size(40, 40);
		public static Size FuturePieceSize = new Size(25, 25);
		public static Size ClockNumbersSize = new Size(7, 13);
		public static Size ScoreDigitsSize = new Size(18, 35);

		public static Point PiecePoint = new Point(5, 56);
		public static Size IntervalPieces = new Size(5, 5);

		public static Point ScoresPosition = new Point(290, 7);
		public static int ScoresAdjustmentOnX = 2;

		public static Point ClockPosition = new Point(183, 34);
		public static int ClockSpacing = 3;
		public static int ClockSpacingAfterComa = 2;

		public static Point[] NextPiece = { new Point(164, 9), new Point(193, 9), new Point(222, 9) };

		public static int[] PieceSelectedView = { 1, 4, 3, 2, 3, 4, 1, 5, 6, 7, 6, 5 };
		public static int[] PieceCreatedView = { 22, 21, 20, 19, 18 };
		public static int[] PieceDestructionView = { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
		public static int PieceHint = 21;

		public static int WaitForMove = 60;
		public static int WaitBeforeCreation = 60;
		public static int WaitBeforeDestruction = 30;
		public static int WaitDuringMove = 20;

		public static Point GetLocationOfPoint(int col, int row)
		{
			return new Point(
				PiecePoint.X + (PieceSize.Width + IntervalPieces.Width) * col,
				PiecePoint.Y + (PieceSize.Height + IntervalPieces.Height) * row);
		}

		public static Point GetAxisIndexes(int x, int y)
		{
			if (x < PiecePoint.X) return new Point(-1, -1);
			if (y < PiecePoint.Y) return new Point(-1, -1);
			return new Point(
				(x - PiecePoint.X) / (PieceSize.Width + IntervalPieces.Width),
				(y - PiecePoint.Y) / (PieceSize.Height + IntervalPieces.Height));
		}

		public static Point GetScoresLocation(int index)
		{
			return new Point(
				ScoresPosition.X + (ScoreDigitsSize.Width + ScoresAdjustmentOnX) * index,
				ScoresPosition.Y);
		}
	}
}
