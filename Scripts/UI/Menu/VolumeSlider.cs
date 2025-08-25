using Godot;
using System;

public partial class VolumeSlider : HSlider {

	[Export] private string bus;

	public override void _Ready() {
		base._Ready();

		this.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex(bus)));
		this.ValueChanged += this.OnValueChange;
	}

	private void OnValueChange(double value) {
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(bus), Mathf.LinearToDb((float) value));
	}
}
