using Godot;
using System;

public partial class SkinPreview : Control {

	[Export] private Control waypointStart;
	[Export] private Control waypointEnd;
	[Export] private Control fishBox;
	[Export] private AnimatedSprite2D fish;
	[Export] private float speed;

	private float time;
	private bool reverse;

	public override void _Process(double delta) {
		base._Process(delta);

		time += ((float) delta) * speed * (reverse ? -1 : 1);

		if (time >= 1) {
			reverse = true;
			fish.FlipH = true;
		} else if (time <= 0) {
			reverse = false;
			fish.FlipH = false;
		}

		fishBox.Position = waypointStart.Position.Lerp(waypointEnd.Position, time);

	}

}
