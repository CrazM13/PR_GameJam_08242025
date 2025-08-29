using Godot;
using SceneManagement;
using System;

public partial class RhythmGame : Node {

	[Export] private Node2D fish;
	[Export] private RhythmDisplay rhythm;
	[Export] private PointsSpawner pointSpawner;
	[Export] private SceneManager sceneManager;
	[Export] private float catchX = 200f;
	[Export] private float escapeX = 1000f;
	[Export] private float passiveReelSpeed = 0.25f;

	private float catchPercentage = 0.75f;
	private float displayCatchPercentage = 0.75f;

	private int difficulty = 0;

	public bool IsStarted { get; set; } = false;

	public override void _Ready() {
		base._Ready();

		fish.GlobalPosition = new Vector2(Mathf.Lerp(catchX, escapeX, catchPercentage), fish.GlobalPosition.Y);

		rhythm.OnRhythmHit += this.OnRhythmHit;
		rhythm.OnRhythmMiss += this.OnRhythmMiss;

	}

	private void OnRhythmMiss() {
		catchPercentage = displayCatchPercentage - 0.125f;
	}

	private void OnRhythmHit() {
		catchPercentage = displayCatchPercentage + 0.125f;
		if (catchPercentage > 1) catchPercentage = 1;
		if (!IsStarted) IsStarted = true;

		pointSpawner.QueueSpawn(fish.GlobalPosition);
		pointSpawner.AttemptSpawn();

		difficulty = Mathf.Min(difficulty + 5, 100);
		rhythm.SetTarget(((difficulty / 100f) * 0.5f) + 0.5f);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (!IsStarted) return;

		catchPercentage -= passiveReelSpeed * (float) delta;

		if (catchPercentage <= 0) {
			sceneManager.LoadScene("res://Scenes/LoseScreen.tscn");
			GameManager.Instance.SaveGame();
			IsStarted = false;
		}

		displayCatchPercentage = MoveToward(displayCatchPercentage, catchPercentage, 0.1f * (float) delta);
		fish.GlobalPosition = new Vector2(Mathf.Lerp(catchX, escapeX, displayCatchPercentage), fish.GlobalPosition.Y);

	}

	private float MoveToward(float value, float target, float amount) {
		if (value < target) {
			float newVal = value + amount;
			if (newVal > target) {
				return target;
			}
			return newVal;
		} else if (value > target) {
			float newVal = value - amount;
			if (newVal < target) {
				return target;
			}
			return newVal;
		}
		return target;
	}

}
