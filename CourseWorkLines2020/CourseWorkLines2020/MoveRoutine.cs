using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    class MoveRoutine
	{
		VisualPanel _panel;
		ArrayList _list;
		int _count;
		Timer _timer;
		Point _point;
		int _color;

		public MoveRoutine(VisualPanel panel, ArrayList list)
		{
			panel.BackUp();

			_panel = panel;
			_list = list;

			_timer = new Timer();
			_timer.Interval = PublicFields.WaitDuringMove;
			_timer.Tick += timer_Tick;

			_point = (Point)list[0];
			_color = panel.GameForm.Field[_point.X, _point.Y];

			panel.GameForm.Field[_point.X, _point.Y] = 0;
			panel.Items[_point.X, _point.Y].Color = 0;

			_count = 0;
			panel.isLocked = true;

			_timer.Start();
		}

		public void timer_Tick(object sender, EventArgs e)
		{
			_count++;

			if (_panel.Items[_point.X, _point.Y].Color >= 0)
				_panel.Items[_point.X, _point.Y].Visible = false;
			else
				_panel.Items[_point.X, _point.Y].SetColourHints();

			_point = (Point)_list[_count];
			_panel.Items[_point.X, _point.Y].Image = CaptureImage.Piece[0, _color - 1];
			_panel.Items[_point.X, _point.Y].Visible = true;

			if (_count == _list.Count - 1)
			{
				_timer.Stop();
				_panel.GameForm.Field[_point.X, _point.Y] = _color;
				_panel.Items[_point.X, _point.Y].Color = _color;
				ArrayList arr = Algorithms.LinesCheck(_panel.GameForm.Field, _point.X, _point.Y);
				if (arr == null)
					_panel.Visual = new Visual(_panel, _panel.GameForm.Field);
				else
					_panel.Destruction = new Destruction(_panel, arr);
			}
		}

		public void Stop()
		{
			_timer.Stop();
		}
	}
}
