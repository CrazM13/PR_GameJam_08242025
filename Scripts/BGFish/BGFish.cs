using Godot;
using System;

public partial class BGFish : VisibleOnScreenNotifier2D {

	[Export] private AnimatedSprite2D sprite;

	private Vector2 velocity;

	private bool active = false;

	public override void _Ready() {
		base._Ready();

		this.ScreenEntered += this.OnEnterScreen;
		this.ScreenExited += this.OnExitScreen;

		sprite.FlipH = velocity.X < 0;

	}

	public override void _Process(double delta) {
		base._Process(delta);

		this.GlobalPosition += velocity * (float) delta;

	}

	public void SetVelocity(Vector2 velocity) {
		this.velocity = velocity;
	}

	public void SetSkin(string skin) {
		sprite.Play(skin);
	}

	private void OnEnterScreen() {
		active = true;
	}

	private void OnExitScreen() {
		if (active) QueueFree();
	}
}
