using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Control {

	[Export] private Control pointsSpawnPos;
	[Export] private PackedScene pointEffectPrefab;
	[Export] private Control container;
	[Export] private SetPlayerSkin skinPreview;

	private List<ShopButton> shopButtons = [];

	public override void _Ready() {
		base._Ready();

		foreach (Node child in container.GetChildren()) {
			if (child is ShopButton shopButton) {
				shopButtons.Add(shopButton);
				shopButton.ShopMenu = this;
				shopButton.Pressed += this.OnButtonPressed;
			}
		}

	}

	private void OnButtonPressed() {
		foreach (ShopButton btn in shopButtons) {
			btn.UpdateButton();
		}
		skinPreview.Reload();
	}

	public void SpawnPointEffect(Vector2 targetPos, int count) {
		for (int i = 0; i < count; i++) {
			MenuPointEffect pointEffect = pointEffectPrefab.Instantiate<MenuPointEffect>();
			pointEffect.Time = i * -0.1f;
			pointEffect.StartPos = pointsSpawnPos.GlobalPosition;
			pointEffect.TargetPos = targetPos;

			GetTree().CurrentScene.AddChild(pointEffect);
		}
	}

}
