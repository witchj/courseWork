using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    class VisualPanel : Panel
	{
		public MainForm MainForm;
		public JumpTo JumpTo;
		public MoveRoutine MoveRoutine;
		public Visual Visual;
		public Destruction Destruction;
		public Clock Clock;

		public Item[,] Items = new Item[9, 9];
		
		public PictureBox Background = new PictureBox();
		public PictureBox[] NextColour = new PictureBox[3];
		
		public PictureBox[] Scores = new PictureBox[5];
		
		public PictureBox HH = new PictureBox();
		public PictureBox MM1 = new PictureBox();
		public PictureBox MM2 = new PictureBox();
		public PictureBox SS1 = new PictureBox();
		public PictureBox SS2 = new PictureBox();

		public GameForm GameForm;
		
		bool _isSelect;
		public bool isLocked;
		int _columnSelected, _rowSelected;

		int[,] _previousField = new int[9, 9];
		int _previousScores;

		public VisualPanel(MainForm mainForm)
		{
			MainForm = mainForm;

			Location = new Point(0, 24);
			Size = new Size(PublicFields.FieldSize.Width, PublicFields.FieldSize.Height);

			BuildPieces();
			BuildScores();
			BuildNextColour();
			
			BuildH();
			BuildM();
			BuildS();
			
			BuildBackground();

			LaunchNewGame();
		}

		public void LaunchNewGame()
		{
			SetGameField(GameForm.GetDefault());
		}

		public void SetGameField(GameForm gameForm)
		{
			GameForm = gameForm;
			_isSelect = false;
			isLocked = false;
			MainForm.IsStepBackEnabled(false);
			StopAllThread();
			Clock = new Clock(this);

			int[,] matrix = gameForm.Field;

			RepaintPieces(matrix);
			RepaintNextColour(matrix);
			RepaintScores(gameForm.Scores);
			RepaintH(gameForm.HH);
			RepaintM(gameForm.MM);
			RepaintS(gameForm.SS);
		}

		private void StopAllThread()
		{
			JumpTo?.Stop();
			MoveRoutine?.Stop();
			Visual?.Stop();
			Destruction?.Stop();
			Clock?.Stop();
		}

		public void BuildBackground()
		{
			Background.Location = new Point(0, 0);
			Background.Size = new Size(PublicFields.FieldSize.Width, PublicFields.FieldSize.Height);
			Background.Image = new Bitmap(PublicFields.Gamefield);
			Background.MouseClick += background_MouseClick;

			Controls.Add(Background);
		}

		public void BuildPieces()
		{
			for (int i = 0; !(i >= 9); i++)
				for (int j = 0; !(j >= 9); j++)
				{
					Items[i, j] = new Item(i, j);
					Items[i, j].Click += piece_Click;
					Controls.Add(Items[i, j]);
				}
		}

		public void RepaintPieces(int[,] matrix)
		{
			for (int i = 0; !(i >= 9); i++)
			for (int j = 0; !(j >= 9); j++)
				switch (matrix[i, j])
				{
					case 0:
						Items[i, j].Visible = false;
						Items[i, j].Color = 0;
						break;
					default:
					{
						if (matrix[i, j] <= 0)
						{
							Items[i, j].Visible = true;
							Items[i, j].Color = matrix[i, j];
							Items[i, j].SetColourHints();
						}
						else
						{
							Items[i, j].Visible = true;
							Items[i, j].Color = matrix[i, j];
							Items[i, j].SetNormal();
						}

						break;
					}
				}
		}

		public void BuildNextColour()
		{
			for (int i = 0; !(i >= 3); i++)
			{
				NextColour[i] = new PictureBox();
				NextColour[i].Location = PublicFields.NextPiece[i];
				NextColour[i].Size = PublicFields.FuturePieceSize;
				Controls.Add(NextColour[i]);
			}
		}

		public void RepaintNextColour(int[,] matrix)
		{
			int count = 0;
			for (int i = 0; !(i >= 9); i++)
				for (int j = 0; !(j >= 9); j++)
					if (matrix[i, j] < 0)
						NextColour[count++].Image = CaptureImage.NextColour[-matrix[i, j] - 1];
		}

		private void BuildScores()
		{
			for (int i = 0; !(i >= 5); i++)
			{
				Scores[i] = new PictureBox
				{
					Location = PublicFields.GetScoresLocation(i), Size = PublicFields.ScoreDigitsSize
				};
				Controls.Add(Scores[i]);
			}
		}

		public void RepaintScores(int score)
		{
			int[] digit = Algorithms.SplitNumber(score, 5);
			for (int i = 0; !(i >= 5); i++)
				Scores[i].Image = CaptureImage.ScoresDigit[digit[i]];
		}
		
		private void BuildH()
		{
			HH.Location = PublicFields.ClockPosition;
			HH.Size = PublicFields.ClockNumbersSize;
			Controls.Add(HH);
		}

		public void RepaintH(int v)
		{
			HH.Image = CaptureImage.ClockDigit[v];
		}

		private void BuildM()
		{
			MM1.Location = new Point(
				HH.Location.X + HH.Size.Width + 1 + 2 * PublicFields.ClockSpacingAfterComa,
				PublicFields.ClockPosition.Y);
			MM1.Size = PublicFields.ClockNumbersSize;

			MM2.Location = new Point(
				MM1.Location.X + MM1.Size.Width + PublicFields.ClockSpacing,
				PublicFields.ClockPosition.Y);
			MM2.Size = PublicFields.ClockNumbersSize;

			Controls.Add(MM1);
			Controls.Add(MM2);
		}
		public void RepaintM(int v)
		{
			int[] digit = Algorithms.SplitNumber(v, 2);
			MM1.Image = CaptureImage.ClockDigit[digit[0]];
			MM2.Image = CaptureImage.ClockDigit[digit[1]];
		}

		private void BuildS()
		{
			SS1.Location = new Point(
				MM2.Location.X + MM2.Size.Width + 1 + 2 * PublicFields.ClockSpacingAfterComa,
				PublicFields.ClockPosition.Y);
			SS1.Size = PublicFields.ClockNumbersSize;

			SS2.Location = new Point(
				SS1.Location.X + SS1.Size.Width + PublicFields.ClockSpacing,
				PublicFields.ClockPosition.Y);
			SS2.Size = PublicFields.ClockNumbersSize;

			Controls.Add(SS1);
			Controls.Add(SS2);
		}

		public void RepaintS(int v)
		{
			int[] digit = Algorithms.SplitNumber(v, 2);
			SS1.Image = CaptureImage.ClockDigit[digit[0]];
			SS2.Image = CaptureImage.ClockDigit[digit[1]];
		}

		private void piece_Click(object sender, EventArgs e)
		{
			if (isLocked) return;
			Item item = sender as Item;
			if (item != null && (_isSelect || item.Color >= 0))
			{
				if (_isSelect)
				{
					if (item.Color < 0)
						MoveTo(item.columns, item.rows);
					else
						NewJump(item);
				}
				else
				{
					NewJump(item);
					_isSelect = true;
				}
			}
		}

		private void background_MouseClick(object sender, MouseEventArgs e)
		{
			if (isLocked) return;
			Point p = PublicFields.GetAxisIndexes(e.X, e.Y);
			if (p.X == -1) return;
			if (GameForm.Field[p.X, p.Y] == 0)
			{
				if (!_isSelect) return;
				MoveTo(p.X, p.Y);
			}
			else
			{
				piece_Click(Items[p.X, p.Y], null);
			}
		}

		private void NewJump(Item item)
		{
			_columnSelected = item.columns;
			_rowSelected = item.rows;
			JumpTo?.Stop();
			JumpTo = new JumpTo(item);
		}

		public void MoveTo(int newCol, int newRow)
		{
			ArrayList list = Algorithms.BuildPath(GameForm.Field, _columnSelected, _rowSelected, newCol, newRow);
			if (list == null) return;
			JumpTo.Stop();
			MoveRoutine = new MoveRoutine(this, list);
			_isSelect = false;
		}

		public void SetGameOver()
		{
			JumpTo?.Stop();
			Clock?.Stop();
			LaunchNewGame();
		}

		public void BackUp()
		{
			for (int i = 0; !(i >= 9); i++)
				for (int j = 0; !(j >= 9); j++)
					_previousField[i, j] = GameForm.Field[i, j];
			_previousScores = GameForm.Scores;
			MainForm.IsStepBackEnabled(true);
		}

		public void StepBack()
		{
			JumpTo?.Stop();
			MoveRoutine?.Stop();
			Visual?.Stop();
			Destruction?.Stop();
			_isSelect = false;
			for (int i = 0; !(i >= 9); i++)
				for (int j = 0; !(j >= 9); j++)
					GameForm.Field[i, j] = _previousField[i, j];
			GameForm.Scores = _previousScores;
			RepaintScores(GameForm.Scores);
			RepaintPieces(GameForm.Field);
			RepaintNextColour(GameForm.Field);
			MainForm.IsStepBackEnabled(false);
		}
	}
}
