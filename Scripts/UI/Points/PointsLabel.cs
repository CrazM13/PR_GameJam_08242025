using Godot;
using System;

public partial class PointsLabel : Label {

	public override void _Process(double delta) {
		base._Process(delta);

		Text = $"x {GameManager.Instance.Points.ToString()}";

	}

}
