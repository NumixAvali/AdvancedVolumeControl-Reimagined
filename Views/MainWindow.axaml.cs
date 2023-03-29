using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NAudio.CoreAudioApi;

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
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					// Console.WriteLine("Windows platform detected");
					var deviceEnumerator = new MMDeviceEnumerator();
					var defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

					// ReSharper disable once PossibleLossOfFraction
					float applicableVolume = level / 100;
					
					defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = applicableVolume;
					break;
				case PlatformID.Unix:
					if (OperatingSystem.IsMacOS())
					{
						Console.WriteLine("Mac platform is unsupported");
					}
					else
					{
						// Console.WriteLine("Linux platform detected");
						string command = $"pactl set-sink-volume @DEFAULT_SINK@ {level}%";
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
					break;
				default:
					Console.WriteLine("Unknown platform detected. Can't operate.");
					break;
			}
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
		}

		private void MuteChecked(object? sender, RoutedEventArgs e)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					// Console.WriteLine("Windows platform detected");
					break;
				case PlatformID.Unix:
					if (OperatingSystem.IsMacOS())
					{
						Console.WriteLine("Mac platform is unsupported.");
					}
					else
					{
						// Console.WriteLine("Linux platform detected");
						string command = "pactl set-sink-mute @DEFAULT_SINK@ 1";
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
						process.WaitForExit();
					}

					break;
				default:
					Console.WriteLine("Unknown platform detected. Can't operate.");
					break;
			}
		}

		private void MuteUnchecked(object? sender, RoutedEventArgs e)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					// Console.WriteLine("Windows platform detected");
					break;
				case PlatformID.Unix:
					if (OperatingSystem.IsMacOS())
					{
						Console.WriteLine("Mac platform is unsupported.");
					}
					else
					{
						// Console.WriteLine("Linux platform detected");
						string command = $"pactl set-sink-mute @DEFAULT_SINK@ 0";
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
						process.WaitForExit();

					}
					break;
				default:
					Console.WriteLine("Unknown platform detected. Can't operate.");
					break;
			}
		}
	}
}
