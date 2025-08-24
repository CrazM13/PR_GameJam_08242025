using Godot;
using System;

public partial class DepthGuage : Control {

	[Export] private Control guage;

	[Export] private Node2D target;
	[Export] private float minY;
	[Export] private float maxY;


	public override void _Process(double delta) {
		base._Process(delta);

		float percent = (target.GlobalPosition.Y - minY) / (maxY - minY);

		float height = guage.Size.Y;
		float offset = guage.Position.Y;

		this.Position = new Vector2(this.Position.X, (percent * height) + offset);

	}

}
