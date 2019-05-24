using UnityEngine;

public class holeSelector : MonoBehaviour
{


	public GameObject table;
	public Camera cam;

	private GameObject wireFirst = null;
	private GameObject wireSecond = null;
	private bool buttonWasDown = false;

	WireHandler _table = null;
	
	int ctr = 1;

    // Start is called before the first frame update
    void Start(){
    	_table = table.GetComponent<WireHandler>();
    }

    // Update is called once per frame
    void Update(){

   		if (Input.GetMouseButton(0)) {
	        bool selectFirst = false;

	        // Select first
	        if(!buttonWasDown) {
	        	selectFirst = true;
	        }
	        buttonWasDown = true;
	        // Track breadboard position and keep chip on top of it
	        RaycastHit hit;

	       
	       	if  (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask("Wire"))) {

	        	Debug.Log("Destroying " + hit.collider.gameObject.name);
	        	Destroy(hit.collider.gameObject);
	        }

	        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask("Wireable"))) {

	        	Vector3 offset = hit.point;
	        	GameObject target = hit.collider.gameObject;

	        	if(selectFirst) {
	            	wireFirst = target;
	        	} 
	        	else if(wireFirst != null && wireFirst != target) {            
	            	wireSecond = target;
	          	}
	        }
	        
	    } 
	    else {
	        if(buttonWasDown) {
	        	if(wireFirst && wireSecond) {
	            	Vector3 start = wireFirst.transform.position;
	            	Vector3 end = wireSecond.transform.position;
	            	Debug.Log(wireFirst.name + " ---- " + wireSecond.name);
		            Debug.Log("----- "+ start + "&& " + end +" -----------------");
		            wireFirst = null;
		            wireSecond = null;
		            buttonWasDown = false;
		            //tempWireBetweenPoints(start, end, ctr);
		            ctr ++;
		            Debug.Log("-----------Wire " + ctr + " generated----------------");
		            
	        	}
	        }

	        wireFirst = null;
	        buttonWasDown = false;
	    }
    
	}
}
