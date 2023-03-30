using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MessageBox.Avalonia.Enums;
using NAudio.CoreAudioApi;

namespace FunnyVolumeApp.Views
{

	class VolumeHandler
	{
		void SetMuteLinux(bool muteSate)
		{
			int muteInt = muteSate? 1 : 0;
			
			string command = $"pactl set-sink-mute @DEFAULT_SINK@ {muteInt}";
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
		
		void SetMuteWindows(bool muteSate)
		{
			// throw new NotImplementedException();
			var deviceEnumerator = new MMDeviceEnumerator();
			var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

			device.AudioEndpointVolume.Mute = muteSate;
		}
		
		void SetVolumeLinux(int volumeLevel)
		{
			string command = $"pactl set-sink-volume @DEFAULT_SINK@ {volumeLevel}%";
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
			// string result = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			// Console.WriteLine(result);	
		}
		void SetVolumeWindows(int volumeLevel)
        {
	        var deviceEnumerator = new MMDeviceEnumerator();
	        var defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

	        float applicableVolume = Convert.ToSingle(volumeLevel) / 100;
					
	        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = applicableVolume;
        }

		public void SetVolume(int level)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					// Console.WriteLine("Windows platform detected");
					SetVolumeWindows(level);
					break;
				case PlatformID.Unix:
					if (OperatingSystem.IsMacOS())
					{
						// Console.WriteLine("Mac platform is unsupported");
						MessageBox.Avalonia.MessageBoxManager
							.GetMessageBoxStandardWindow(
								"Error", 
								"Mac platform is unsupported.",
								ButtonEnum.Ok,
								Icon.Error);

					}
					else
					{
						// Console.WriteLine("Linux platform detected");
						SetVolumeLinux(level);
					}
					break;
				default:
					MessageBox.Avalonia.MessageBoxManager
						.GetMessageBoxStandardWindow(
							"Error", 
							"Unknown platform detected. Can't operate.",
							ButtonEnum.Ok,
							Icon.Error);
					break;
			}
		}

		public void SetMute(bool state)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					// Console.WriteLine("Windows platform detected");
					SetMuteWindows(state);
					
					break;
				case PlatformID.Unix:
					if (OperatingSystem.IsMacOS())
					{
						// Console.WriteLine("Mac platform is unsupported.");
						MessageBox.Avalonia.MessageBoxManager
							.GetMessageBoxStandardWindow(
								"Error", 
								"Mac platform is unsupported.",
								ButtonEnum.Ok,
								Icon.Error);
					}
					else
					{
						// Console.WriteLine("Linux platform detected");
						SetMuteLinux(state);
					}

					break;
				default:
					// Console.WriteLine("Unknown platform detected. Can't operate.");
					MessageBox.Avalonia.MessageBoxManager
						.GetMessageBoxStandardWindow("Error", "Unknown platform detected. Can't operate.");
					break;
			}
		}

	}
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private readonly VolumeHandler _volumeHandler = new VolumeHandler();
		
		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			var radioButton = (RadioButton)sender;
			if (radioButton.IsChecked == true)
			{
				string? selectedOption = radioButton.Content.ToString();
				// Console.WriteLine($"Selected option: {selectedOption}");
				_volumeHandler.SetVolume(Convert.ToInt32(selectedOption));
			}
		}

		private void MuteChecked(object? sender, RoutedEventArgs e)
		{
			_volumeHandler.SetMute(true);
		}

		private void MuteUnchecked(object? sender, RoutedEventArgs e)
		{
			_volumeHandler.SetMute(false);
		}
	}
}
