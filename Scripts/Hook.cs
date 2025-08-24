using Godot;
using System;

public partial class Hook : Area2D {

	[Signal] public delegate void OnFishHookedEventHandler(Hook hook, FishController fish);
	[Signal] public delegate void OnHookRetractedEventHandler(Hook hook);

	[Export] private float speed = 100;
	[Export] private Sprite2D baitSprite;

	private float extend = 0;
	private float maxExtend = 0;

	private bool retracting = false;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	private Vector2 startPos;

	public override void _Ready() {
		base._Ready();

		this.BodyEntered += this.OnBodyEnter;

		maxExtend = rng.RandfRange(256, 512);

		startPos = GlobalPosition;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		if (!retracting && extend < maxExtend) {
			extend += ((float) delta) * speed;

			GlobalPosition = startPos + (Vector2.Down * extend);
		} else if (retracting && extend > 0) {
			extend -= ((float) delta) * speed;

			GlobalPosition = startPos + (Vector2.Down * extend);

			if (extend <= 0) {
				EmitSignal(SignalName.OnHookRetracted, this);
			}
		}

	}

	private void OnBodyEnter(Node2D body) {
		
		if (body is AnimatableBody2D) {
			foreach (Node node in body.GetChildren()) {
				if (node is FishController controller) {
					controller.AllowInput = false;
					controller.SetAttractor(this);

					this.retracting = true;
					baitSprite.Visible = false;

					this.SetDeferred("monitoring", false);
					this.SetDeferred("monitorable", false);

					EmitSignal(SignalName.OnFishHooked, this, controller);
				}
			}
		}

	}
}
