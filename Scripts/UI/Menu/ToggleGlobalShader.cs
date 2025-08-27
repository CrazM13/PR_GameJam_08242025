using BetterUISuite;
using Godot;
using System;

public partial class ToggleGlobalShader : BetterButton {

	public override void _Ready() {
		base._Ready();

		this.Toggled += this.OnToggle;

	}

	private void OnToggle(bool value) {
		GameManager.Instance.SeasicknessMode = value;
		GameManager.Instance.SaveGame();
		RenderingServer.GlobalShaderParameterSet("seasickness", value);
	}
}
