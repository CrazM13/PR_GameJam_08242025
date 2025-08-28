using Godot;
using System;

public partial class UIPauseMenu : CanvasLayer {

	public void TogglePause() {
		this.Visible = !this.Visible;
		GetTree().Paused = this.Visible;
		GameManager.Instance.SaveGame();
	}

	public void UnPause() {
		this.Visible = false;
		GetTree().Paused = false;
		GameManager.Instance.SaveGame();
	}

	public void Pause() {
		this.Visible = true;
		GetTree().Paused = true;
		GameManager.Instance.SaveGame();
	}

}
