using Godot;
using System;
using System.Collections.Generic;

public partial class PointsSpawner : Node2D {

	[Export] private PackedScene prefab;
	[Export] private int value = 1;

	private Queue<Vector2> remainingSpawns = [];

	public void QueueSpawn(Vector2 position, int count = 1) {
		for (int _ = 0; _ < count; _++) {
			remainingSpawns.Enqueue(position);
		}
	}

	public void AttemptSpawn() {
		if (remainingSpawns.Count > 0) {

			PointsEffect pointEffect = prefab.Instantiate<PointsEffect>();
			pointEffect.Value = value;
			AddChild(pointEffect);
			pointEffect.GlobalPosition = remainingSpawns.Dequeue();
		}
	}

}
