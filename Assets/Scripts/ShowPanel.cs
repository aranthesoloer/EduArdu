using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{	[SerializeField]
	private GameObject panel1;
	[SerializeField]
	private GameObject panel2;
    // Start is called before the first frame update
    void Start(){
    	panel1.SetActive(false);
    	panel2.SetActive(false);    
    }
    void Update() {
    	if(Input.GetMouseButton(1)) {
    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    	RaycastHit hit;

	    	if(Physics.Raycast(ray, out hit)) { 
	   			
	   			if(hit.collider.gameObject.name == "USB"){
	   				panel1.SetActive(true);  
	   				panel2.SetActive(false);
	   			}
	   			else if(hit.collider.gameObject.name == "Power"){
	   				panel1.SetActive(false);  
	   				panel2.SetActive(true);
	   			}
	   			else{
	   				panel1.SetActive(false);  
	   				panel2.SetActive(false);
	   			}
	    	}
    	}


    	
    }
}
