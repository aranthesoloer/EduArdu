using UnityEngine;

public class Droppable : MonoBehaviour
{
 	public Material[] material;

 	[SerializeField]
	private Droppable [] neighbors;
	Renderer rend;
	private bool availability;
	private bool isWired = false;
	[SerializeField]
	private string portNumber;

	void Start(){
		availability = true;
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}

	public void availabilityColor(){
		if(availability){
			rend.sharedMaterial = material[1];
		}
		else{
			rend.sharedMaterial = material[0];
		}
	}

	public string getPortNumber(){
		return portNumber;
	}
	public void setPortNumber(string x){
		portNumber = x;
	}
	public void updateNeighborPorts(string x){
		if(neighbors.Length != 0){
			for(int i=0; i<neighbors.Length; i++){
	 			neighbors[i].setPortNumber(x);
	 		}
 		}
	}

	public void changeColor(int color){
		rend.sharedMaterial = material[color];
	}

	public bool isAvailable(){
		return availability;
	}

	public void setAvailability(bool x){
		availability = x;
	}
	public void setWired(bool x){
		isWired = x;
	}

	public void determineAvailabilty(){
		RaycastHit hit;
		Debug.DrawRay(transform.position,Vector3.up * 1f, Color.blue);
		if(isWired){
			availability = false;
		}
		else{
			if( Physics.Raycast(transform.position, Vector3.up, out hit, 1f) ){
			string collide = hit.collider.gameObject.name;
				
				if(collide =="LED" | collide =="resistor"){
					Debug.Log("------ " + collide + " from below------");
					availability = false;
					Debug.Log("------------- NANI THE HECKKKK --------- " + collide);
				}
				else{
					availability = true;
				}
			}

			else{
				availability = true;
			}
		}
		
	}

}
