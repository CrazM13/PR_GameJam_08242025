using Godot;
using System;

public partial class SetPlayerSkin : AnimatedSprite2D {

	public override void _Ready() {
		base._Ready();

		this.Play(GameManager.Instance.Skin);

	}

	public void Reload() {
		this.Play(GameManager.Instance.Skin);
	}

}
