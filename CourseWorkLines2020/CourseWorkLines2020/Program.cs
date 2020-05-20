using System;
using System.Windows.Forms;

namespace CourseWorkLines2020
{
	static class Program
	{
		[STAThread] 
		//This attribute must be present on the entry point of any application that uses Windows Forms;
		//if it is omitted, the Windows components might not work correctly.
		static void Main()
		{
			Control.CheckForIllegalCrossThreadCalls = false; // to avoid cross threading errors during debugging 
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false); //set by default
			Application.Run(new MainForm());
		}
	}
}