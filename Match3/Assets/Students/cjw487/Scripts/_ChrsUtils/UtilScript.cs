using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
//using SimpleJSON;

//UtilScript uses a collection of static functions
//to make programming more convinent in Unity.
//You can think of it as a very basic version of a
//library, like UnityEngine or System.
/* 
public class UtilScript : MonoBehaviour {

	/// <summary>
	/// Write a JSONClass to a file
	/// </summary>
	/// <param name="path">Path to file to write</param>
	/// <param name="fileName">fileName of file to write</param>
	/// <param name="json">JSONClass to put into file</param>
	public static void WriteJSONtoFile(string path, string fileName, JSONNode json){
		WriteStringToFile(path, fileName, json.ToString());
	}

	/// <summary>
	/// Write a string to a file
	/// </summary>
	/// <param name="path">Path to file to write</param>
	/// <param name="fileName">fileName of file to write</param>
	/// <param name="content">string to put into file</param>
	public static void WriteStringToFile(string path, string fileName, string content){
		StreamWriter sw = new StreamWriter(path + "/" + fileName);

		sw.Write(content);

		sw.Close();
	}

	/// <summary>
	/// Read a file into a JSONNode, then return that JSONNode
	/// </summary>
	/// <param name="path">Path to file to read</param>
	/// <param name="fileName">fileName of file to read</param>
	public static JSONNode ReadJSONFromFile(string path, string fileName){
		JSONNode result = JSON.Parse(ReadStringFromFile(path, fileName));

		return result;
	}

	/// <summary>
	/// Read a string from a file at a path
	/// </summary>
	/// <param name="path">Path to file to read</param>
	/// <param name="fileName">fileName of file to read</param>
	public static string ReadStringFromFile(string path, string fileName){
		StreamReader sr = new StreamReader(path + "/" + fileName);

		string result = sr.ReadToEnd();

		sr.Close();

		return result;
	}

	/// <summary>
	/// Make a copy of a Vector3
	/// </summary>
	/// <param name="vec">Vector3 to Clone</param>
	public static Vector3 CloneVector3(Vector3 vec){
		return new Vector3(vec.x, vec.y, vec.z);
	}
		
	/// <summary>
	/// Make a copy of a Vector3 and modify some values
	/// </summary>
	/// <param name="vec">Vector3 to Clone</param>
	/// <param name="xMod">amount to mod x value by</param>
	/// <param name="yMod">amount to mod y value by</param>
	/// <param name="zMod">amount to mod z value by</param>
	public static Vector3 CloneModVector3(
		Vector3 vec, 
		float xMod,
		float yMod,
		float zMod){
		return new Vector3(
			vec.x + xMod,
			vec.y + yMod,
			vec.z + zMod);
	}
}
*/