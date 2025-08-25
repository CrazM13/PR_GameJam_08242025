using Godot;
using System;

public partial class HookManager : Node2D {

	[Export] private Rect2 spawnArea;
	[Export] private PackedScene[] hookPrefabs;
	[Export] private TypingMinigame minigame;

	private FishController fish;
	private Hook hookWithFish = null;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready() {
		base._Ready();

		minigame.OnWin += OnMinigameWin;

	}

	private void Spawn() {
		Hook hook = hookPrefabs[rng.RandiRange(0, hookPrefabs.Length - 1)].Instantiate<Hook>();
		hook.GlobalPosition = GetSpawnPosition();

		hook.OnHookRetracted += this.OnHookRetracted;
		hook.OnFishHooked += this.OnFishHooked;

		AddChild(hook);
	}

	private void OnFishHooked(Hook hook, FishController fish) {

		if (hookWithFish != null) return;

		fish.AllowInput = false;
		fish.SetAttractor(hook);

		this.fish = fish;
		hookWithFish = hook;
		hook.ConsumeHook();
		minigame.StartGame(hook.WordList);
	}

	private void OnHookRetracted(Hook hook) {
		if (hook == hookWithFish) {
			GetTree().ReloadCurrentScene();
		}
	}

	private Vector2 GetSpawnPosition() {
		float x = rng.RandfRange(spawnArea.Position.X, spawnArea.End.X);

		return new Vector2(x, 133);
	}

	private void OnMinigameWin() {
		if (fish == null) return;

		// TODO Increase Score
		hookWithFish = null;
		fish.SetAttractor(null);
		fish.AllowInput = true;

		fish = null;
	}

}
