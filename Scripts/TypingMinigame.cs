using Godot;
using System;
using System.Text;

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
					input.Text = BuildTyped();
				}
			} else {
				currentIndex = 0;
				input.Text = BuildTyped();
			}
		}
	}

	public void StartGame(string word) {
		preview.Text = word.ToUpper();
		input.Text = string.Empty;
		isPlaying = true;
		currentIndex = 0;
	}

	private string BuildTyped() {
		StringBuilder sb = new StringBuilder();

		if (currentIndex > 0) sb.Append(preview.Text.Substr(0, currentIndex));
		sb.Append('_', preview.Text.Length - currentIndex);

		return sb.ToString();
	}

}
