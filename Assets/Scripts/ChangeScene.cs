// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

	public void ChangeToPracticeScene(){
 		SceneManager.LoadScene("Room");
 	}
 	public void ReturnToMainMenu(){
 		SceneManager.LoadScene("main_menu");
 	}
 	public void ChangeToViewArduino(){
 		SceneManager.LoadScene("View Arduino");
 	}

 	public void QuitApp(){
 		Application.Quit();
 	}
}
