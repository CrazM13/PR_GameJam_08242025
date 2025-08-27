using DataVault;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class GameManager {

	private const string VAULT_NAME = "SAVE_DATA";

	#region Singleton

	private static GameManager instance;
	public static GameManager Instance {
		get {
			instance ??= new GameManager();

			return instance;
		}
	}

	private GameManager() { /* MT */ }

	#endregion

	public int Points { get; set; }
	public string Skin { get; set; } = "basic";
	public Array<string> UnlockedSkins { get; set; } = ["basic"];

	public Dictionary<string, float> Volume { get; private set; } = new Dictionary<string, float>() { { "Master", 1f }, { "Music", 1f }, { "SFX", 1f } };
	public bool SeasicknessMode { get; set; } = false;

	public void SaveGame() {
		Vault vault = VaultManager.GetVault(VAULT_NAME);
		vault ??= VaultManager.CreateVault(VAULT_NAME);

		vault.SetValue("points", Points);
		vault.SetValue("skin", Skin);
		vault.SetValue("unlocked_skins", UnlockedSkins.ToArray());

		// Options
		vault.SetValue("volume_master", Volume["Master"]);
		vault.SetValue("volume_music", Volume["Music"]);
		vault.SetValue("volume_sfx", Volume["SFX"]);

		vault.SetValue("seasickness_mode", SeasicknessMode);

		VaultManager.SaveVault(VAULT_NAME);
	}

	public void LoadGame() {
		Vault vault = VaultManager.GetVault(VAULT_NAME);
		if (vault == null) { // Try Loading
			VaultManager.LoadVault(VAULT_NAME);
			vault = VaultManager.GetVault(VAULT_NAME);
		}

		if (vault != null) {
			Points = vault.GetValue("points").As<int>();
			Skin = vault.GetValue("skin").AsString();
			UnlockedSkins = [.. vault.GetValue("unlocked_skins").AsStringArray()];

			// Options
			Volume["Master"] = vault.GetValue("volume_master").As<float>();
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), Mathf.LinearToDb(Volume["Master"]));
			Volume["Music"] = vault.GetValue("volume_music").As<float>();
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb(Volume["Music"]));
			Volume["SFX"] = vault.GetValue("volume_sfx").As<float>();
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), Mathf.LinearToDb(Volume["SFX"]));

			SeasicknessMode = vault.GetValue("seasickness_mode").AsBool();
			RenderingServer.GlobalShaderParameterSet("seasickness", SeasicknessMode);
		}

	}

}
