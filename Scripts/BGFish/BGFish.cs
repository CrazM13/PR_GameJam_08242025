using Godot;
using System;

public partial class BGFish : AnimatableBody2D {

	[Export] private AnimatedSprite2D sprite;
	[Export] private VisibleOnScreenNotifier2D detection;

	private Vector2 velocity;

	private bool active = false;

	public bool IsSwimming { get; set; } = true;

	public override void _Ready() {
		base._Ready();

		detection.ScreenEntered += this.OnEnterScreen;
		detection.ScreenExited += this.OnExitScreen;

		sprite.FlipH = velocity.X < 0;

	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (IsSwimming) {
			this.GlobalPosition += velocity * (float) delta;
		}

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
