// PlayUp Tools - www.playuptools.com

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

using System;
using System.Xml;

public class PlayUpImport {
	
    public static void FiletoObj(string levelName)
    {
	
		char[] delimiterChars = { ',' };
		GameObject level = new GameObject(Path.GetFileNameWithoutExtension(levelName));
		
		//Create the XmlDocument.
		XmlDocument doc = new XmlDocument();
		string path = "Assets\\PlayUp\\Levels\\" + levelName;
		FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
		doc.Load(fs);
		XmlNodeList node = doc.GetElementsByTagName("OBJECT");
		for (int i=0; i<node.Count; i++)
		{
			GameObject prefab = null;
			GameObject clone = null;
			XmlAttributeCollection attrc = node[i].Attributes;
			for (int j=0; j<attrc.Count; j++)
			{
				if (attrc[j].Name == "NAME"){
					prefab = Resources.LoadAssetAtPath("Assets/PlayUp/Objects/" + attrc[j].Value + ".dae", typeof(GameObject)) as GameObject; 
					clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
				}
				if (attrc[j].Name == "POSITION"){
					string[] vals = attrc[j].Value.Split(delimiterChars);
					Vector3 pos;
					pos.x = -float.Parse(vals[0]);
					pos.z = -float.Parse(vals[1]);
					pos.y = float.Parse(vals[2]);
					clone.transform.position = pos;
				}
				if (attrc[j].Name == "ROTATION"){
					string[] vals = attrc[j].Value.Split(delimiterChars);
					float qX, qY, qZ, qW;
					qW = float.Parse(vals[0]);
					qX = float.Parse(vals[1]);
					qY = float.Parse(vals[2]);
					qZ = float.Parse(vals[3]);
					clone.transform.rotation = new Quaternion(-qX,qZ,-qY,qW);		
				} 
				if (attrc[j].Name == "SCALE"){
					string[] vals = attrc[j].Value.Split(delimiterChars);
					Vector3 scale;
					scale.x = float.Parse(vals[0]);
					scale.y = float.Parse(vals[1]);
					scale.z = float.Parse(vals[2]);
					clone.transform.localScale = scale;
				}
			}
		clone.transform.parent = level.transform;
		}		
    }
}