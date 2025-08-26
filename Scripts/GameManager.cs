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

	public void SaveGame() {
		Vault vault = VaultManager.GetVault(VAULT_NAME);
		vault ??= VaultManager.CreateVault(VAULT_NAME);

		vault.SetValue("points", Points);
		vault.SetValue("skin", Skin);
		vault.SetValue("unlocked_skins", UnlockedSkins.ToArray());

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
		}

	}

}
