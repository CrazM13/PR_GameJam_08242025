using Godot;
using System;

public partial class VolumeSlider : HSlider {

	[Export] private string bus;

	public override void _Ready() {
		base._Ready();

		this.Value = GameManager.Instance.Volume[bus];
		this.ValueChanged += this.OnValueChange;
	}

	private void OnValueChange(double value) {
		GameManager.Instance.Volume[bus] = (float) value;
		GameManager.Instance.SaveGame();

		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(bus), Mathf.LinearToDb((float) value));
	}
}
