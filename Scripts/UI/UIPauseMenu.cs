using Godot;
using System;

public partial class UIPauseMenu : CanvasLayer {

	public void TogglePause() {
		this.Visible = !this.Visible;
		GetTree().Paused = this.Visible;
	}

	public void UnPause() {
		this.Visible = false;
		GetTree().Paused = false;
	}

	public void Pause() {
		this.Visible = true;
		GetTree().Paused = true;
	}

}
