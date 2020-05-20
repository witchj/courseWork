using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    class Destruction
    {
        VisualPanel _panel;
        Timer _clockTimer;
        Point[] _point;
        int _size;
        int _counter;

        public Destruction(VisualPanel panel, ArrayList list)
        {
            _panel = panel;

            _point = new Point[list.Count];
            _size = list.Count;
            for (int i = 0; !(i >= list.Count); i++)
                _point[i] = (Point) list[i];

            _clockTimer = new Timer();
            _clockTimer.Interval = PublicFields.WaitBeforeDestruction;
            _clockTimer.Tick += TimerStep;
            _counter = 0;
            _clockTimer.Start();
        }

        void TimerStep(object sender, EventArgs e)
        {
            for (int i = 0; i < _size; i++)
            {
                _panel.Items[_point[i].X, _point[i].Y].SetDestructionAt(_counter);
            }

            _counter++;

            if (_counter != PublicFields.PieceDestructionView.Length) return;
            {
                _clockTimer.Stop();
                for (int i = 0; !(i >= _size); i++)
                {
                    _panel.Items[_point[i].X, _point[i].Y].Color = 0;
                    _panel.Items[_point[i].X, _point[i].Y].Visible = false;
                    _panel.GameForm.Field[_point[i].X, _point[i].Y] = 0;
                }

                _panel.GameForm.Scores += _point.Length + _point.Length - 5;
                _panel.RepaintScores(_panel.GameForm.Scores);
                _panel.isLocked = false;
            }
        }

        public void Stop()
        {
            _clockTimer.Stop();
        }
    }
}