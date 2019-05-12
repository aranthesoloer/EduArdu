using UnityEngine;

public class Droppable : MonoBehaviour
{
 	public Material[] material;
	Renderer rend;
	private bool availability;
	private int portNumber;

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

	public void changeColor(int color){
		rend.sharedMaterial = material[color];
	}

	public bool isAvailable(){
		return availability;
	}

	public void setAvailability(bool x){
		availability = x;
	}

	public void determineAvailabilty(){
		RaycastHit hit;
		Debug.DrawRay(transform.position,Vector3.up * 1f, Color.blue);

		if( Physics.Raycast(transform.position, Vector3.up, out hit, 1f) ){
			string collide = hit.collider.gameObject.name;
			
			if(collide =="LED" | collide =="LED (1)"){
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
