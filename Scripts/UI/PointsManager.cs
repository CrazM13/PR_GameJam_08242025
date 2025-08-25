using Godot;
using System;

public class PointsManager {

	#region Singleton

	private static PointsManager instance;
	public static PointsManager Instance {
		get {
			instance ??= new PointsManager();

			return instance;
		}
	}

	private PointsManager() { /* MT */ }

	#endregion

	public int Points { get; set; }

}
