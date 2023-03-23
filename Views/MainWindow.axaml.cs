using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FunnyVolumeApp.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
		}

		public void SetVolume(int level)
		{
			string command = $"pactl set-sink-volume @DEFAULT_SINK@ {level}%"; // Replace with your command
			Process process = new Process()
			{
				StartInfo = new ProcessStartInfo()
				{
					FileName = "/bin/bash",
					Arguments = $"-c \"{command}\"",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};
			process.Start();
			string result = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			// Console.WriteLine(result);
			
		}

		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			var radioButton = (RadioButton)sender;
			if (radioButton.IsChecked == true)
			{
				string? selectedOption = radioButton.Content.ToString();
				// Console.WriteLine($"Selected option: {selectedOption}");
				SetVolume(Convert.ToInt32(selectedOption));
			}
			/*
			var objRef = e.Source.InteractiveParent;
			Console.WriteLine("Clicked!");
			Console.WriteLine("Btn name is: {0}",objRef);
			// Process.Start("uname -a");
			SetVolume(10);
			// throw new System.NotImplementedException();
			*/
		}
	}
	
}