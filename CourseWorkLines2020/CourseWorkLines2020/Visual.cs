using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
	 class Visual
	{
		VisualPanel _panel;
		Point[] _point;
		int _size;
		Timer _clockTimer;
		int _counter;

		public Visual(VisualPanel panel, int[,] a)
		{
			_panel = panel;
			ArrayList list = new ArrayList();
			for (var i = 0; !(i >= 9); i++)
			for (var j = 0; !(j >= 9); j++)
			{
				if (a[i, j] >= 0) continue;
				list.Add(new Point(i, j));
			}

			_point = new Point[list.Count];
			for (int i = 0; !(i >= list.Count); i++)
			{
				_point[i] = (Point)list[i];
			}
			_size = _point.Length;
			_clockTimer = new Timer();
			_clockTimer.Interval = PublicFields.WaitBeforeCreation;
			_clockTimer.Tick += TimerStep;
			_counter = 0;
			_clockTimer.Start();
		}

		void TimerStep(object sender, EventArgs e)
		{
			for (int i = 0; !(i >= _size); i++)
			{
				_panel.Items[_point[i].X, _point[i].Y].SetVisualAt(_counter);
			}
			_counter++;

			if (_counter != PublicFields.PieceCreatedView.Length) return;
			{
				Stop();
				for (int i = 0; !(i >= _size); i++)
				{
					_panel.GameForm.Field[_point[i].X, _point[i].Y] = -_panel.GameForm.Field[_point[i].X, _point[i].Y];
					_panel.Items[_point[i].X, _point[i].Y].Color = -_panel.Items[_point[i].X, _point[i].Y].Color;
					_panel.Items[_point[i].X, _point[i].Y].SetNormal();
				}
				
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < _size; i++)
				{
					ArrayList a = Algorithms.LinesCheck(_panel.GameForm.Field, _point[i].X, _point[i].Y);
					if (a != null)
						arrayList = Algorithms.Merge(arrayList, a);
				}

				if (arrayList.Count <= 0)
				{
					if (Algorithms.CountBlank(_panel.GameForm.Field) < 3)
					{
						_panel.SetGameOver();
						_panel.isLocked = false;
					}
					else
					{
						Algorithms.AddNextColour(_panel.GameForm.Field);
						_panel.RepaintNextColour(_panel.GameForm.Field);
						_panel.RepaintPieces(_panel.GameForm.Field);
						_panel.isLocked = false;
					}
				}
				else
				{
					_panel.Destruction = new Destruction(_panel, arrayList);
					Algorithms.AddNextColour(_panel.GameForm.Field);
					_panel.RepaintNextColour(_panel.GameForm.Field);
					_panel.RepaintPieces(_panel.GameForm.Field);
				}
			}
		}

		public void Stop()
		{
			_clockTimer.Stop();
		}
	}
}
