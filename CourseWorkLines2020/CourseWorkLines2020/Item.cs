using System.Drawing;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    class Item : PictureBox
	{
		public int columns, rows;
		public int Color;

		public Item(int i, int j)
		{
			columns = i;
			rows = j;
			Location = PublicFields.GetLocationOfPoint(i, j);
			Size = new Size(
				PublicFields.PieceSize.Width, PublicFields.PieceSize.Height);
		}

		public void SetNormal()
		{
			Image = CaptureImage.Piece[0, Color - 1];
		}

		public void SetColourHints()
		{
			Image = CaptureImage.Piece[PublicFields.PieceHint, -Color - 1];
		}

		public void SetJumpAt(int frame)
		{
			Image = CaptureImage.Piece[PublicFields.PieceSelectedView[frame] - 1, Color - 1];
		}

		public void SetVisualAt(int frame)
		{
			Image = CaptureImage.Piece[PublicFields.PieceCreatedView[frame] - 1, -Color - 1];
		}

		public void SetDestructionAt(int frame)
		{
			Image = CaptureImage.Piece[PublicFields.PieceDestructionView[frame] - 1, Color - 1];
		}
	}
}
