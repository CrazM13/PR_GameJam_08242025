using Godot;
using System;

public partial class Hook : Area2D {

	[Signal] public delegate void OnFishHookedEventHandler(Hook hook, FishController fish);
	[Signal] public delegate void OnHookRetractedEventHandler(Hook hook);

	[Export] private float speed = 100;
	[Export] private Sprite2D baitSprite;
	[Export] private AudioStreamPlayer2D biteSound;
	[Export] private AudioStreamPlayer2D reelSound;

	private float extend = -100;
	private float maxExtend = 0;

	private bool retracting = false;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	private Vector2 startPos;

	private float timeRemaining;

	public override void _Ready() {
		base._Ready();

		this.BodyEntered += this.OnBodyEnter;

		maxExtend = rng.RandfRange(256, 400);
		timeRemaining = rng.RandfRange(10, 30);

		startPos = GlobalPosition;
		GlobalPosition = startPos + (Vector2.Down * extend);
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		if (timeRemaining > 0) {
			timeRemaining -= (float) delta;
			if (timeRemaining <= 0) {
				retracting = true;
				reelSound.Play();
			}
		}

		if (!retracting && extend < maxExtend) {
			extend += ((float) delta) * speed;

			GlobalPosition = startPos + (Vector2.Down * extend);

			if (extend >= maxExtend) {
				reelSound.Stop();
			}
		} else if (retracting && extend > 0) {
			extend -= ((float) delta) * speed;

			GlobalPosition = startPos + (Vector2.Down * extend);

			if (extend <= 0) {
				EmitSignal(SignalName.OnHookRetracted, this);
			}
		} else if (retracting) {
			extend -= ((float) delta) * speed;
			if (extend < -10) {
				QueueFree();
			}
		}

	}

	private void OnBodyEnter(Node2D body) {
		
		if (body is AnimatableBody2D) {
			foreach (Node node in body.GetChildren()) {
				if (node is FishController controller) {
					EmitSignal(SignalName.OnFishHooked, this, controller);
				}
			}
		}

	}

	public void ConsumeHook() {
		this.retracting = true;
		reelSound.Play();
		baitSprite.Visible = false;
		biteSound.Play();

		this.SetDeferred("monitoring", false);
		this.SetDeferred("monitorable", false);
	}
}
