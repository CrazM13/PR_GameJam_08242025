using Godot;
using System;

public partial class BGM : AudioStreamPlayer {

	private static float position;

	public override void _Ready() {
		base._Ready();

		this.Play(position);

	}

	public override void _Process(double delta) {
		base._Process(delta);

		position = this.GetPlaybackPosition();

	}

}
