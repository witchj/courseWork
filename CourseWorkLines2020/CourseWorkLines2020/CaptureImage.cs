using System.Drawing;
using System.Drawing.Imaging;

namespace CourseWorkLines2020
{
    static class CaptureImage
    {
        public static Bitmap[,] Piece = Clip(PublicFields.Gameobjects, 22, 7);
        public static Bitmap[] ScoresDigit = Clip(PublicFields.Scores, 10);

        public static Bitmap[] NextColour = Clip(PublicFields.Colourrange, 7);
        public static Bitmap[] ClockDigit = Clip(PublicFields.Clock, 10);

        public static Bitmap[,] Clip(Bitmap bigImage, int col, int row)
        {
            Bitmap[,] images = new Bitmap[col, row];

            int pieceWidth = bigImage.Width / col;
            int pieceHeight = bigImage.Height / row;

            for (int i = 0; !(i >= col); i++)
            for (int j = 0; !(j >= row); j++)
            {
                images[i, j] = bigImage.Clone(
                    new Rectangle(i * pieceWidth, j * pieceHeight, pieceWidth, pieceHeight),
                    PixelFormat.DontCare);
            }

            return images;
        }

        public static Bitmap[,] Clip(string fileName, int col, int row)
        {
            var bitmaps = Clip(new Bitmap(fileName), col, row);
            return bitmaps;
        }

        public static Bitmap[] Clip(string fileName, int col)
        {
            var bitmaps = Clip(new Bitmap(fileName), col);
            return bitmaps;
        }

        public static Bitmap[] Clip(Bitmap bigImage, int col)
        {
            Bitmap[] images = new Bitmap[col];

            int pieceWidth = bigImage.Width / col;
            int pieceHeight = bigImage.Height;

            for (int i = 0; !(i >= col); i++)
            {
                images[i] = bigImage.Clone(
                    new Rectangle(i * pieceWidth, 0, pieceWidth, pieceHeight),
                    PixelFormat.DontCare);
            }

            return images;
        }
    }
}