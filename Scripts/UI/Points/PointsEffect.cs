using Godot;
using System;

public partial class PointsEffect : VisibleOnScreenNotifier2D {

	[Export] private float speed;

	private Vector2 velocity;

	public int Value { get; set; }

	public override void _Ready() {
		base._Ready();

		velocity = GetViewportRect().Size.Normalized() * speed;

		this.ScreenExited += this.OnScreenExited;

	}

	private void OnScreenExited() {
		GameManager.Instance.Points += Value;
		QueueFree();
	}

	public override void _Process(double delta) {
		base._Process(delta);

		velocity += this.GlobalPosition.DirectionTo(new Vector2(0, GetViewportRect().Size.Y)) * speed;

		this.GlobalPosition += velocity * (float) delta;
	}

}
