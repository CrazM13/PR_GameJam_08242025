using Godot;
using System;

public partial class FishController : Node {

	[Export] private float speed = 100;

	[Export] private Node2D fish;
	[Export] private AnimatedSprite2D sprite;

	[Export] private Rect2 playArea = new Rect2(32, 160, 1088, 488);

	private Node2D attractor;

	public bool AllowInput { get; set; } = true;

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		if (AllowInput) GetInput((float) delta);
		else if (IsInstanceValid(attractor)) {
			MoveToAttractor(attractor.GlobalPosition + (Vector2.Down * 16), (float) delta);
		}

	}

	public void GetInput(float delta) {

		Vector2 input;

		if (Input.IsMouseButtonPressed(MouseButton.Left)) {

			Vector2 mousePos = fish.GetGlobalMousePosition();
			if (mousePos.DistanceSquaredTo(fish.GlobalPosition) < 64) return;

			input = fish.GlobalPosition.DirectionTo(mousePos);
		} else {
			input = new Vector2(Input.GetAxis("move_left", "move_right"), Input.GetAxis("move_up", "move_down"));
		}

		if (input.X != 0) sprite.FlipH = input.X < 0;

		Vector2 lookAt = new Vector2(Mathf.Abs(input.X), sprite.FlipH ? -input.Y : input.Y);
		sprite.LookAt(sprite.GlobalPosition + lookAt);

		Vector2 moveTo = fish.GlobalPosition + input;
		if (!playArea.HasPoint(moveTo)) {
			if (moveTo.X > playArea.End.X || moveTo.X < playArea.Position.X) {
				input.X = 0;
			}

			if (moveTo.Y > playArea.End.Y || moveTo.Y < playArea.Position.Y) {
				input.Y = 0;
			}
		}

		fish.GlobalPosition = fish.GlobalPosition.MoveToward(fish.GlobalPosition + input, delta * speed);
		
	}

	public void MoveToAttractor(Vector2 attractor, float delta) {

		Vector2 direction = fish.GlobalPosition.DirectionTo(attractor);

		if (direction.X != 0) sprite.FlipH = direction.X < 0;

		Vector2 lookAt = new Vector2(Mathf.Abs(direction.X), sprite.FlipH ? -direction.Y : direction.Y);
		sprite.LookAt(sprite.GlobalPosition + lookAt);

		fish.GlobalPosition = fish.GlobalPosition.MoveToward(attractor, delta * speed * 2);
	}

	public void SetAttractor(Node2D node) {
		this.attractor = node;
	}

}
