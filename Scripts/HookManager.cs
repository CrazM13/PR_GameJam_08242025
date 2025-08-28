using Godot;
using SceneManagement;
using System;

public partial class HookManager : Node2D {

	[Export] private Rect2 spawnArea;
	[Export] private PackedScene[] hookPrefabs;
	[Export] private PointsSpawner pointSpawner;
	[Export] private TypingMinigame minigame;
	[Export] private SceneManager sceneManager;

	private FishController fish;
	private Hook hookWithFish = null;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready() {
		base._Ready();

		minigame.OnWin += OnMinigameWin;
		minigame.OnStart += OnMinigameStart;
		minigame.OnAbort += OnMinigameAborted;

	}

	private void OnMinigameAborted() {
		if (fish == null) return;

		hookWithFish.Pause = false;
		hookWithFish = null;
		fish.SetAttractor(null);
		fish.AllowInput = true;

		fish = null;
	}

	private void OnMinigameStart() {
		hookWithFish.ConsumeHook();
		hookWithFish.Pause = false;
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
		hookWithFish.Pause = true;
		minigame.StartGame(hook.WordList);
	}

	private void OnHookRetracted(Hook hook) {
		if (hook == hookWithFish) {
			GameManager.Instance.SaveGame();
			sceneManager.LoadScene("res://Scenes/LoseScreen.tscn");
		}
	}

	private Vector2 GetSpawnPosition() {
		float x = rng.RandfRange(spawnArea.Position.X, spawnArea.End.X);

		return new Vector2(x, 133);
	}

	private void OnMinigameWin() {
		if (fish == null) return;

		pointSpawner.QueueSpawn(hookWithFish.GlobalPosition, hookWithFish.Value);
		hookWithFish.Pause = false;
		hookWithFish = null;
		fish.SetAttractor(null);
		fish.AllowInput = true;

		fish = null;
	}

}
