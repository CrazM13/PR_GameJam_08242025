using Godot;
using System;

public partial class BGFishSpawner : Node2D {


	[Export] private PackedScene fishPrefab;
	[Export] private Vector2 velocity;
	[Export] private float speedModifier = 1;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public void SpawnSolitary() {
		string skin = GameManager.Instance.UnlockedSkins[rng.RandiRange(0, GameManager.Instance.UnlockedSkins.Count - 1)];

		BGFish newFish = fishPrefab.Instantiate<BGFish>();

		newFish.GlobalPosition = this.GlobalPosition;
		newFish.SetSkin(skin);
		newFish.SetVelocity(velocity * speedModifier);

		AddChild(newFish);
	}

	public void SpawnSchool() {
		string skin = GameManager.Instance.UnlockedSkins[rng.RandiRange(0, GameManager.Instance.UnlockedSkins.Count - 1)];

		for (int i = 0; i < rng.RandiRange(3, 9); i++) {
			BGFish newFish = fishPrefab.Instantiate<BGFish>();

			newFish.GlobalPosition = this.GlobalPosition + (GetSpiralPoint(i) * 16);
			newFish.SetSkin(skin);
			newFish.SetVelocity(velocity * speedModifier);

			AddChild(newFish);
		}

	}

	public static Vector2 GetSpiralPoint(float angle) {

		float spacing = 1;

		float b = spacing / (0.5f * Mathf.Pi);

		float x = b * angle * Mathf.Cos(angle);
		float y = b * angle * Mathf.Sin(angle);

		return new Vector2(x, y);
	}

}
