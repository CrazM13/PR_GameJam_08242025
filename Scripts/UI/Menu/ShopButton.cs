using BetterUISuite;
using Godot;
using System;

public partial class ShopButton : BetterButton {

	[Export] private AnimatedSprite2D display;
	[Export] private TextureRect priceIcon;
	[Export] private Label priceTag;

	[Export] public string SkinID { get; set; }
	[Export] public int Price { get; set; }
	public Shop ShopMenu { get; set; }

	private bool unlocked;
	private bool equipped;

	public override void _Ready() {
		base._Ready();

		UpdateButton();
		if (!string.IsNullOrEmpty(SkinID)) display.Play(SkinID);
		else display.Visible = false;

		this.Pressed += this.OnPress;
	}

	private void OnPress() {
		
		if (!unlocked) {
			GameManager.Instance.Points -= Price;
			GameManager.Instance.UnlockedSkins.Add(SkinID);
			ShopMenu.SpawnPointEffect(this.GlobalPosition, Price);
		}

		GameManager.Instance.Skin = SkinID;

		GameManager.Instance.SaveGame();

		UpdateButton();
	}

	public void UpdateButton() {
		equipped = GameManager.Instance.Skin == SkinID;
		unlocked = GameManager.Instance.UnlockedSkins.Contains(SkinID);

		this.Disabled = string.IsNullOrEmpty(SkinID) || equipped || (!unlocked && GameManager.Instance.Points < Price);


		priceTag.Text = equipped ? "USING" : (unlocked ? "OWNED" : $"x{Price}");
		priceIcon.Visible = !unlocked;
	}

}
