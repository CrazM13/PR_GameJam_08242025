using Godot;
using System;

public partial class HookManager : Node2D {

	[Export] private Rect2 spawnArea;
	[Export] private PackedScene hookPrefab;
	[Export] private TypingMinigame minigame;

	private FishController fish;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready() {
		base._Ready();

		minigame.OnWin += OnMinigameWin;

	}

	private void Spawn() {
		Hook hook = hookPrefab.Instantiate<Hook>();
		hook.GlobalPosition = GetSpawnPosition();

		hook.OnHookRetracted += this.OnHookRetracted;
		hook.OnFishHooked += this.OnFishHooked;

		AddChild(hook);
	}

	private void OnFishHooked(Hook hook, FishController fish) {
		this.fish = fish;
		minigame.StartGame("test");
	}

	private void OnHookRetracted(Hook hook) {
		hook.QueueFree();

		if (fish != null) {
			GetTree().ReloadCurrentScene();
		}
	}

	private Vector2 GetSpawnPosition() {
		float x = rng.RandfRange(spawnArea.Position.X, spawnArea.End.X);
		float y = rng.RandfRange(spawnArea.Position.Y, spawnArea.End.Y);

		return new Vector2(x, y);
	}

	private void OnMinigameWin() {
		if (fish == null) return;

		fish.SetAttractor(null);
		fish.AllowInput = true;

		fish = null;
	}

}
