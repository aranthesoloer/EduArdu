using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Coding : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI code;
	string text;

    public void compileCode(){
    	Debug.Log(code.text);
    	text = code.text;
    	//string [] split = text.Split(';');
    	string [] split = text.Split(new Char [] {';',' ','{', '}', '\n', '\t' });
   //  	foreach (string word in split){  
			// Debug.Log(word);
   //      }  

        Debug.Log(split[4]);
        Debug.Log(split[9]);

    }
}
