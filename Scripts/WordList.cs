using Godot;
using System;

public partial class WordList : Resource {

	[Export] private string[] words;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public string GetRandom() {
		return words[rng.RandiRange(0, words.Length - 1)];
	}

	public void SetWords(string[] words) {
		this.words = words;
	}

}
