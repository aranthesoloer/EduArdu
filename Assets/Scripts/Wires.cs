using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject wireFirst = null;
	  private GameObject wireSecond = null;
   	public void setConnection(GameObject x, GameObject y){
   		wireFirst = x;
   		wireSecond = y;
      string portNumber1 = wireFirst.GetComponent<Droppable>().getPortNumber();
      string portNumber2 = wireSecond.GetComponent<Droppable>().getPortNumber();

      if(portNumber1!="none"){
        wireSecond.GetComponent<Droppable>().updateNeighborPorts(portNumber1);
        wireSecond.GetComponent<Droppable>().setPortNumber(portNumber1);
      }
      else{
        if(portNumber2!="none"){
          wireFirst.GetComponent<Droppable>().updateNeighborPorts(portNumber2);
          wireFirst.GetComponent<Droppable>().setPortNumber(portNumber2);
        }
      }
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
