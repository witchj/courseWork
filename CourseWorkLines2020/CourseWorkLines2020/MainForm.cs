using System;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
    public partial class MainForm : Form
	{
		VisualPanel _panel;
		
		public MainForm()
		{
			InitializeComponent();
		}

		private void LoadMainForm(object sender, EventArgs e)
		{
			_panel = new VisualPanel(this);
			Controls.Add(_panel);
		}

		private void MenuNewGame(object sender, EventArgs e)
		{
			_panel.LaunchNewGame();
		}

		private void ExitMenuClick(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void CloseMainForm(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(0);
		}

		private void EndGameClick(object sender, EventArgs e)
		{
			_panel.SetGameOver();
		}

		public void IsStepBackEnabled(bool boolean)
		{
			StepBackStripMenu.Enabled = boolean;
		}

		private void StepBackClick(object sender, EventArgs e)
		{
			_panel.StepBack();
		}

		private void RulesOfTheGameClick(object sender, EventArgs e)
		{
			new GameRules().ShowDialog();
		}
	}
}