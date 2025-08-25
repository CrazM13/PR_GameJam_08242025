using Godot;
using System;

public partial class LoseScene : Node {

	[Export] private Control pupil;
	[Export] private float pupilSpeed;

	private Vector2 pupilStartPos;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready() {
		base._Ready();

		pupilStartPos = pupil.Position;

	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (pupilStartPos.DistanceSquaredTo(pupil.Position) > 64) {
			pupil.Position = pupil.Position.MoveToward(pupilStartPos, ((float) delta) * pupilSpeed);
		} else {
			Vector2 targetPosition = pupilStartPos + (new Vector2(rng.RandfRange(-1, 1), rng.RandfRange(-1, 1))) * pupilSpeed;
			pupil.Position = pupil.Position.MoveToward(targetPosition, ((float) delta) * pupilSpeed);
		}

	}

}
