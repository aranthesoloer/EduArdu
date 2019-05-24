using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoard : MonoBehaviour
{
	[SerializeField]
	private Droppable [] holes;

 	public void displayAvailability(){

 		for(int i = 0; i<holes.Length; i++){
 			holes[i].availabilityColor();
 		}
 	}

 	public void hideAvailability(){
 		for(int i = 0; i<holes.Length; i++){
 			holes[i].changeColor(0);
 		}
 	}
 	public void hover(string a, string b){
 		for(int i=0; i<holes.Length; i++){
 			if(holes[i]. isAvailable()){
 				if (holes[i].name == a | holes[i].name == b){
 					holes[i].changeColor(2);
	 			}
	 			else{
	 				holes[i].changeColor(1);
	 			}
 			}
 		}

 	}

 	public void hover(string a){
 		for(int i=0; i<holes.Length; i++){
 			if(holes[i]. isAvailable()){
 				if (holes[i].name == a){
 					holes[i].changeColor(2);
	 			}
	 			else{
	 				holes[i].changeColor(1);
	 			}
 			}
 		}

 	}

 	public void changeAvailability(bool x, string a, string b){
 		for(int i=0; i<holes.Length; i++){
 			if (holes[i].name == a | holes[i].name == b){
 				holes[i].setAvailability(x);
 			}
 		}
 	}
 	
 	public void changeWired(bool x, string a, string b){
 		for(int i=0; i<holes.Length; i++){
 			if (holes[i].name == a | holes[i].name == b){
 				holes[i].setWired(x);
 			}
 		}
 	}

 	public void updateAvailability(){
 		for(int i=0; i<holes.Length; i++){
 			holes[i].determineAvailabilty();	
 		}
 	}

 	public void display(){
 		for(int i=0; i<holes.Length; i++){
 			Debug.Log("------------ " + holes[i].name + ": " + holes[i].isAvailable() + " --------------------- " );	
 		}
 	}
}
