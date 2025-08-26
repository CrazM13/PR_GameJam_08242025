using Godot;
using System;

public partial class LoadGame : Node {

	public override void _Ready() {
		base._Ready();

		GameManager.Instance.LoadGame();

	}

}
