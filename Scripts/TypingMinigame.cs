using Godot;
using System;

public partial class TypingMinigame : CanvasLayer {

	[Signal] public delegate void OnWinEventHandler();

	[Export] private Label preview;
	[Export] private Label input;

	private int currentIndex = 0;
	private bool isPlaying = false;

	public override void _Input(InputEvent @event) {
		base._Input(@event);

		if (!isPlaying) return;

		if (@event is InputEventKey keyEvent && keyEvent.Pressed) {
			if (keyEvent.AsTextKeyLabel() == preview.Text[currentIndex].ToString()) {
				currentIndex++;

				if (currentIndex == preview.Text.Length) {
					input.Text = preview.Text;
					isPlaying = false;
					EmitSignal(SignalName.OnWin);
				} else {
					input.Text = preview.Text.Substr(0, currentIndex);
				}
			} else {
				currentIndex = 0;
				input.Text = string.Empty;
			}
		}

	}

	public void StartGame(string word) {
		preview.Text = word.ToUpper();
		input.Text = string.Empty;
		isPlaying = true;
	}

}
