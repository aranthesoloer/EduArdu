using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject wireFirst = null;
	private GameObject wireSecond = null;
   	public void setConnection(GameObject x, GameObject y){
   		Debug.Log("-----Setting up connection----------");
   		wireFirst = x;
   		wireSecond = y;
   	}
   	public void resetConnection(){
   		wireFirst.GetComponent<Droppable>().setWired(false);
   		wireSecond.GetComponent<Droppable>().setWired(false);
   		wireFirst.GetComponent<Droppable>().setAvailability(true);
   		wireSecond.GetComponent<Droppable>().setAvailability(true);

   		wireSecond.GetComponent<Droppable>().updateNeighborPorts("none");
   		wireFirst.GetComponent<Droppable>().updateNeighborPorts("none");
   	}
}
