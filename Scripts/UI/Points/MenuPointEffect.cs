using Godot;
using System;

public partial class MenuPointEffect : Control {

	public float Time { get; set; }

	public Vector2 TargetPos { get; set; }
	public Vector2 StartPos { get; set; }

	public override void _Process(double delta) {
		base._Process(delta);

		Time += (float) delta * 2;
		this.GlobalPosition = StartPos.Lerp(TargetPos, Mathf.Clamp(Time, 0, 1));

		if (Time >= 1) {
			QueueFree();
		}

	}

}
