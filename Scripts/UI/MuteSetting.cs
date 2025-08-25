using Godot;
using System;

public partial class MuteSetting : Control {

	[Export] private VolumeSlider[] volumeSliders;

	public void Mute() {
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);

		foreach (VolumeSlider volumeSlider in volumeSliders) {
			volumeSlider.SelfModulate = new Color(1, 1, 1, 0.5f);
			volumeSlider.MouseFilter = MouseFilterEnum.Ignore;
		}
	}

	public void Unmute() {
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), false);

		foreach (VolumeSlider volumeSlider in volumeSliders) {
			volumeSlider.SelfModulate = Colors.White;
			volumeSlider.MouseFilter = MouseFilterEnum.Pass;
		}
	}


}
