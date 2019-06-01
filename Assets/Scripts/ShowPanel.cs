using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{	[SerializeField]
	private GameObject [] panels;
    // Start is called before the first frame update
    void Start(){
    	for(int i=0; i<panels.Length; i++){
 			panels[i].SetActive(false);
 		}  
    }
    void Update() {
    	if(Input.GetMouseButton(1)) {
    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    	RaycastHit hit;
	    	if(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Clickable"))) { 
	   			for(int i=0; i<panels.Length; i++){
	   				if(panels[i].name == hit.collider.gameObject.name ){
	   					panels[i].SetActive(true);
	   				}
	   				else{
	   					panels[i].SetActive(false);
	   				}
		 		}  
	    	}
	    	else{
	    		for(int i=0; i<panels.Length; i++){
		 			panels[i].SetActive(false);
		 		}  
	    	}
    	}
    }


}
