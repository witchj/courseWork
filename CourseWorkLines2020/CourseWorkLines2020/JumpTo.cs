using System;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
	class JumpTo
	{
		Item _item;
		Timer _clockTimer;
		int _frame;
		int _size;

		public JumpTo(Item item)
		{
			_item = item;

			_frame = 0;
			_size = PublicFields.PieceSelectedView.Length;

			_clockTimer = new Timer {Interval = PublicFields.WaitForMove};
			_clockTimer.Tick += timer_Tick;
			_clockTimer.Start();
		}

		public void timer_Tick(object sender, EventArgs e)
		{
			_item.SetJumpAt(_frame++);
			if (_frame != _size)
			{
				return;
			}

			_frame = 0;
		}

		public void Stop()
		{
			_clockTimer.Stop();
			if (_item.Color <= 0)
			{
				return;
			}

			_item.SetNormal();
		}
	}
}
