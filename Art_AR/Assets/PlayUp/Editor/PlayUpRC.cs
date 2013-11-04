// PlayUp Tools - www.playuptools.com

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

using System;
using System.Xml;

public class PlayUpRC {
	
	[MenuItem ("Assets/Import Level via PlayUp")]
	
	static void  Init () {
		UnityEngine.Object obj = Selection.activeObject;
		string path = AssetDatabase.GetAssetPath(obj);
		string fn = Path.GetFileName(path);
		if ((Path.GetExtension(fn) == ".lvl") && (Path.GetDirectoryName(path) == "Assets/PlayUp/Levels")) {
			PlayUpImport.FiletoObj(fn);
		}
		else {
			if (Path.GetExtension(fn) != ".lvl")	Debug.Log("The file you have selected is not a valid .LVL file.");
			if (Path.GetDirectoryName(path) != "Assets/PlayUp/Levels") Debug.Log("The file you have selected is not located in the Assets/PlayUp/Levels directory.");
		}
	}
	
}