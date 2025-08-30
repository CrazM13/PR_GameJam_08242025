using Godot;
using System;

public partial class RhythmDisplay : Control {

	[Signal] public delegate void OnRhythmHitEventHandler();
	[Signal] public delegate void OnRhythmMissEventHandler();

	[Export] private TextureProgressBar display;
	[Export] private Control marker;

	private float markerPos;

	private float targetRange = 1f;
	private float targetOffset = 0f;
	public bool IsActive { get; set; } = true;

	private bool wasPressed = false;

	public override void _Ready() {
		base._Ready();
		SetTarget(0f);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		targetOffset = (targetOffset - (float) delta);
		if (targetOffset < 0) targetOffset += 1;
		display.RadialInitialAngle = (targetOffset * 360f) + 80f;

		markerPos = (markerPos + (0.1f * (float) delta)) % 1;
		Test();

		marker.GlobalPosition = display.GetGlobalRect().GetCenter() + (Vector2.Right.Rotated(markerPos * Mathf.Tau) * 96);
		marker.Rotation = (markerPos * Mathf.Tau) + Mathf.Pi;

		marker.SelfModulate = marker.SelfModulate.Lerp(Colors.White, 10f * (float) delta);
	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);

		if (!IsActive) return;

		if (@event is InputEventKey keyEvent) {

			if (keyEvent.Pressed && !wasPressed) {
				OnHit();

				wasPressed = true;
			} else if (!keyEvent.Pressed) {
				wasPressed = false;
			}
		} else if (@event is InputEventMouseButton mouseEvent) {
			if (mouseEvent.Pressed && !wasPressed) {
				OnHit();

				wasPressed = true;
			} else if (!mouseEvent.Pressed) {
				wasPressed = false;
			}
		}

	}

	private void OnHit() {
		float targetAngle = markerPos;
		if (targetAngle < targetOffset) {
			targetAngle += 1;
		}

		if (targetAngle <= targetRange + targetOffset && targetAngle >= targetOffset) {
			EmitSignal(SignalName.OnRhythmHit);
			marker.SelfModulate = Colors.Green;
		} else {
			EmitSignal(SignalName.OnRhythmMiss);
			marker.SelfModulate = Colors.Red;
		}
	}

	public void SetTarget(float difficulty) {
		//targetOffset = (targetOffset + (Mathf.Pi * difficulty)) % 1;
		targetRange = ((1 - difficulty) * 0.95f) + 0.05f;

		//display.RadialInitialAngle = (targetOffset * 360f) + 80f;
		display.Value = targetRange;
	}

	private void Test() {
		float targetAngle = markerPos;
		if (targetAngle < targetOffset) {
			targetAngle += 1;
		}

		if (targetAngle <= targetRange + targetOffset && targetAngle >= targetOffset) {
			marker.SelfModulate = Colors.Green;
		} else {
			marker.SelfModulate = Colors.Red;
		}
	}

}
