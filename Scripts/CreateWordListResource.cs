using Godot;
using System;
using System.Collections.Generic;

public partial class CreateWordListResource : Node {

	[Export(PropertyHint.File)] private string path;

	public override void _Ready() {
		base._Ready();

		using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);

		List<string> words = [];

		while (!file.EofReached()) {
			string newWord = file.GetLine().Trim().ToUpper();
			if (newWord.Length >= 3) words.Add(newWord);
		}

		WordList newWordList = new WordList();
		newWordList.SetWords([.. words]);

		string newPath = path.Replace(".txt", ".tres");
		ResourceSaver.Save(newWordList, newPath);
		GD.Print($"Saved to {newPath}");
	}

}
