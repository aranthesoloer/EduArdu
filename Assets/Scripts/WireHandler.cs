using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHandler : MonoBehaviour
{
    // Start is called before the first frame update

	public Material lineMaterial;
	private float lineWidth= 0.1f;
	public float depth = 5f;

	public GameObject wirePrefab;

	[SerializeField]
	private Camera cam;

	[SerializeField]
    private BreadBoard wiring;

    int wireLimit = 2;



	//For wire connection
	private GameObject wireFirst = null;
	private GameObject wireSecond = null;
	private bool buttonWasDown = false;
	int ctr = 0;
	
	Vector3 startTemp;
	Vector3 endTemp;

    void Start () {   
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.T)){
			Debug.Log("------ Toggle Wires --------");
            wiring.displayAvailability();
        }

        
		if (Input.GetMouseButton(0)) {
	        bool selectFirst = false;

	        // Select first
	        if(!buttonWasDown) {
	        	selectFirst = true;
	        }
	        buttonWasDown = true;
	        // Track breadboard position and keep chip on top of it
	        RaycastHit hit;

	       
	       	if  (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100)) {

	        	if(hit.collider.gameObject.name == "Colider"){
	        		hit.collider.transform.parent.gameObject.GetComponent<Wires>().resetConnection();
	        		Destroy(hit.collider.transform.parent.gameObject);

	        		ctr--;
	        	}
	        }

	        if ( ctr < wireLimit && Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask("Wireable"))) {

	        	Vector3 offset = hit.point;
	        	GameObject target = hit.collider.gameObject;
	        	if(target.GetComponent<Droppable>().isAvailable()){
	        		if(selectFirst) {
	        			wiring.hover(target.name);
		            	wireFirst = target;
		            	startTemp = GetMouseCameraPoint();
		        	} 
		        	else if(wireFirst != null && wireFirst != target) {    
		        		wiring.hover(wireFirst.name,target.name);        
		            	wireSecond = target;
		          	}
	        	}
	        }
	        
	    } 
	    else {
	        if(buttonWasDown) {
	        	if(wireFirst && wireSecond) {
	        		wiring.changeAvailability(false, wireFirst.name, wireSecond.name);
	        		wiring.changeWired(true, wireFirst.name, wireSecond.name);
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
	        		endTemp = GetMouseCameraPoint();
	            	Vector3 start = wireFirst.transform.localPosition;
	            	Vector3 end = wireSecond.transform.localPosition;
	            	ctr ++;
		            createWire(startTemp, endTemp, ctr);
		            wireFirst = null;
		            wireSecond = null;
		            buttonWasDown = false;
		            		            
	        	}
	        }

	        wireFirst = null;
	        buttonWasDown = false;
	    }

	}

	public void createWire( Vector3 start, Vector3 end, int ctr){
		Vector3 normal = transform.rotation*Vector3.up;
		GameObject wire = (GameObject)Instantiate(wirePrefab, normal*0.03f, Quaternion.identity);
		wire.GetComponent<Wires>().setConnection(wireFirst, wireSecond);
		wire.name = "Wire " + ctr;
		var lineRenderer = wire.AddComponent<LineRenderer>();
		lineRenderer.material = lineMaterial;
		lineRenderer.SetPositions(new Vector3[] {start, end});
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;
		BoxCollider col = new GameObject("Colider").AddComponent<BoxCollider>();
		col.transform.parent = lineRenderer.transform;
		float lineLength = Vector3.Distance(start, end);
		col.size = new Vector3(lineLength, 0.1f, 0.11f);
		Vector3 midPoint = (start + end)/2;
		col.transform.position = midPoint;

		float angle = (Mathf.Abs(start.y-end.y)/ Mathf.Abs (start.x - end.x));
		if((start.y < end.y && start.x > end.x) || (end.y<start.y && end.x > start.x)){
			angle *= -1;
		}
		angle = Mathf.Rad2Deg * Mathf.Atan(angle);
		col.transform.Rotate(0,0,angle);

	}


	private Vector3 GetMouseCameraPoint(){
		var ray = cam.ScreenPointToRay(Input.mousePosition);
		return ray.origin + ray.direction * depth;
	}

}
