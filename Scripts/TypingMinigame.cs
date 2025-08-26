using Godot;
using System;
using System.ComponentModel;
using System.Text;

public partial class TypingMinigame : CanvasLayer {

	[Signal] public delegate void OnStartEventHandler();
	[Signal] public delegate void OnWinEventHandler();

	[Export] private Control container;
	[Export] private Label preview;
	[Export] private Label input;
	[Export] private bool restartOnMistake;

	private int currentIndex = 0;
	private bool isPlaying = false;
	private bool isStarted = false;

	private float wrongAnimTime = -1;

	public override void _Input(InputEvent @event) {
		base._Input(@event);

		if (!isPlaying) return;

		if (@event is InputEventKey keyEvent && keyEvent.Pressed) {
			if (keyEvent.AsTextKeyLabel() == preview.Text[currentIndex].ToString()) {
				currentIndex++;
				if (!isStarted) {
					EmitSignal(SignalName.OnStart);
					isStarted = true;
				}

				if (currentIndex == preview.Text.Length) {
					input.Text = preview.Text;
					isPlaying = false;
					container.Visible = false;
					EmitSignal(SignalName.OnWin);
				} else {
					input.VisibleCharacters = currentIndex;
				}
			} else {
				wrongAnimTime = 0;
				input.Modulate = Colors.Red;
				if (restartOnMistake) {
					currentIndex = 0;
					input.VisibleCharacters = 0;
				}
			}
		}
	}

	public void StartGame(WordList wordList) {
		input.Text = preview.Text = wordList.GetRandom().ToUpper();
		input.VisibleCharacters = 0;
		isPlaying = true;
		isStarted = false;
		currentIndex = 0;

		container.Visible = true;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (wrongAnimTime > -1) {
			wrongAnimTime += (float) delta * 2;
			input.Position = new Vector2(Mathf.Sin(wrongAnimTime * 50) * (1f - wrongAnimTime) * 10, 0);
			input.Modulate = Colors.Red.Lerp(Colors.White, wrongAnimTime);

			if (wrongAnimTime >= 1) {
				wrongAnimTime = -1;
			}
		}

		

	}

}
