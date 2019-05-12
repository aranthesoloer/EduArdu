using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{

	// Saving and Loading the coordinates of the player {
	public static void SavePlayer (PlayerController player){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.dataPath +"/Save"+ "/player.fun";
		FileStream stream = new FileStream(path, FileMode.Create);
		PlayerData data = new PlayerData(player);
		formatter.Serialize(stream, data);
		stream.Close();
		Debug.Log(" file save player successfully created..." );

	}

	public static PlayerData LoadPlayer(){
		string path = Application.dataPath +"/Save" + "/player.fun";
		if (File.Exists(path)){
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			return data;
		}
		else{
			Debug.Log("Save file not found in " + path);
			return null;
		}
	}

    public static void RefreshEditorProjectWindow(){
         #if UNITY_EDITOR
         UnityEditor.AssetDatabase.Refresh();
         #endif
     }
    public static void DeleteFile(string filename){
         string filePath = Application.dataPath +"/Save" + filename;

         // check if file exists
         if ( !File.Exists( filePath ) )
         {
             Debug.Log( "no file exists" );
         }
         else
         {
             Debug.Log(" file exists, deleting..." );
             File.Delete( filePath );
             File.Delete(filePath+".meta");
             RefreshEditorProjectWindow();
         }
     }

    
}
