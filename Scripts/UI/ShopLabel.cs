using Godot;
using System;

public partial class ShopLabel : Label {

	public override void _Process(double delta) {
		base._Process(delta);

		this.Text = GetRealName();

	}

	private string GetRealName() {
		return GameManager.Instance.Skin switch {
			"basic" => "Bass",
			"clown" => "Clownfish",
			"cichlid" => "Cichlid",
			"angler" => "Anglerfish",
			"jelly" => "Jellyfish",
			"bivalve" => "Bivalve",
			"starfish" => "Starfish",
			"blob" => "Blobfish",
			"squid" => "Squid",
			"turtle" => "Turtle",
			"dumbo" => "Dumbo Octopus",
			"sheep" => "Leaf Sheep",
			"tuna" => "Tuna Can",
			"trash" => "Plastic Bag",
			"sing" => "Singing Bass",
			"goldy" => "Goldy the Goldfish",
			"fossil" => "Ammonite",
			"sub" => "Submarine",
			"scuba" => "Scuba Diver",
			"anura" => "Anura",
			_ => "UNKNOWN"
		};
	}

}
