using System;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    class Clock
    {
        VisualPanel _panel;
        Timer _clockTimer;

        public Clock(VisualPanel panel)
        {
            _panel = panel;
            _clockTimer = new Timer {Interval = 1000};
            _clockTimer.Tick += ClockTimerTick;
            _clockTimer.Start();
        }

        void ClockTimerTick(object sender, EventArgs e)
        {
            _panel.GameForm.SS++;
            if (_panel.GameForm.SS == 60)
            {
                _panel.GameForm.SS = 0;
                _panel.GameForm.MM++;
                if (_panel.GameForm.MM == 60)
                {
                    _panel.GameForm.MM = 0;
                    _panel.GameForm.HH++;
                }
            }

            _panel.RepaintH(_panel.GameForm.HH);
            _panel.RepaintM(_panel.GameForm.MM);
            _panel.RepaintS(_panel.GameForm.SS);
        }

        public void Stop()
        {
            _clockTimer.Stop();
        }
    }
}