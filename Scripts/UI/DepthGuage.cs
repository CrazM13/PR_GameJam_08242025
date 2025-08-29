using Godot;
using System;

public partial class DepthGuage : Control {

	[Export] private Control guage;

	[Export] private Node2D target;
	[Export] private bool horizontal;
	[Export] private float min;
	[Export] private float max;


	public override void _Process(double delta) {
		base._Process(delta);

		if (horizontal) {
			UpdateH();
		} else {
			UpdateV();
		}

	}

	private void UpdateV() {
		float percent = (target.GlobalPosition.Y - min) / (max - min);

		float height = guage.Size.Y;
		float offset = guage.GlobalPosition.Y;

		this.GlobalPosition = new Vector2(this.GlobalPosition.X, (percent * height) + offset);
	}

	private void UpdateH() {
		float percent = (target.GlobalPosition.X - min) / (max - min);

		float width = guage.Size.X;
		float offset = guage.GlobalPosition.X;

		this.GlobalPosition = new Vector2((percent * width) + offset, this.GlobalPosition.Y);
	}

}
