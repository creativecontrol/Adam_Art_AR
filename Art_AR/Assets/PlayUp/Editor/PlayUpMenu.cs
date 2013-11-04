// PlayUp Tools - www.playuptools.com

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

using System;
using System.Xml;

public class PlayUpMenu: EditorWindow {

	string myString = "";
	
	[MenuItem ("PlayUp/Import Level")]
	
	static void  Init () {
		// Get existing open window or if none, make a new one:
		PlayUpMenu window = (PlayUpMenu)EditorWindow.GetWindow (typeof (PlayUpMenu));
		window.Show ();
	}
	
	void OnGUI  () {
		Texture2D texty = Resources.LoadAssetAtPath("Assets/PlayUp/Editor PlayUp Resources/playup-logo-unity.png", typeof(Texture2D)) as Texture2D; 
		if (texty) GUI.DrawTexture(new Rect(20,0,227, 60), texty); 	
		GUI.BeginGroup (new  Rect (10, 70, 270, 200));
		
		GUILayout.BeginHorizontal (GUILayout.Width(200));
		GUILayout.Space (50);
		if(GUILayout.Button("www.playuptools.com", GUILayout.Width(170))) {
			Application.OpenURL("http://www.playuptools.com");
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (10);
		if (myString != "") {
			GUILayout.Label ("LEVEL SELECTED FOR IMPORT: ", EditorStyles.boldLabel);
			GUILayout.Label (myString, EditorStyles.boldLabel);
			GUILayout.Label ("");
			GUILayout.Label ("Click on the Import button to load the level.");
		}
		else {
			GUILayout.Label ("LEVEL SELECTED FOR IMPORT: ", EditorStyles.boldLabel);
			GUILayout.Label ("none", EditorStyles.boldLabel);
			GUILayout.Label ("");
			GUILayout.Label ("Select a level file by clicking the Browse button.");
		}
		GUILayout.BeginHorizontal (GUILayout.Width(200));
	   if(GUILayout.Button("Browse", GUILayout.Width(100))) {
		   myString = Path.GetFileName(EditorUtility.OpenFilePanel("Choose the Level File", "Assets/PlayUp/Levels/", "lvl"));
	   } 
	   if (myString != "") {
		   if(GUILayout.Button("Import", GUILayout.Width(100))) {
			   if (myString != "") PlayUpImport.FiletoObj(myString);
			   else Debug.Log("You have not yet selected a level file.  Please select a level file by clicking the Browse button.");
		   } 
		}
	GUILayout.EndHorizontal ();
		GUI.EndGroup ();
	}
}