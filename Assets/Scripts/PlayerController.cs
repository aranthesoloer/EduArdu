using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Camera camera2;

	[SerializeField]
	private TextMeshProUGUI outputCode;

	[SerializeField]
	private Camera camera3;

	[SerializeField]
	private float speed = 17f;
	[SerializeField]
	private float lookSensitivity = 3f;
	private PlayerMotor motor;


	// Raycasting {
	public float distanceToSee = 30;
    RaycastHit whatIHit;


    [SerializeField]
	private GameObject panel;

	[SerializeField]
	private GameObject textEditor;

	[SerializeField]
	private Text interact;
	// }

	// public void SavePlayer(){
	// 	SaveSystem.SavePlayer(this);
	// }

	// public void LoadPlayer(){
	// 	PlayerData data = SaveSystem.LoadPlayer();

	// 	if (data != null){

	// 		//Calculate rotation as a 3D Vector (turning around)
	// 		float _yRotation = data.mouse[0];
	// 		Vector3 _rotation = new Vector3(0f,_yRotation, 0f ) * lookSensitivity;

	// 		// Apply rotation
	// 		motor.Rotate(_rotation);

	// 		//Calculate camera rotation as a 3D Vector (turning around)
	// 		float _xRotation = data.mouse[1];
	// 		Vector3 _cameraRotation = new Vector3(_xRotation,0f, 0f ) * lookSensitivity;

	// 		// Apply camera rotation
	// 		motor.RotateCamera(_cameraRotation);



	// 		Vector3 position;
	// 		position.x = data.position[0];	
	// 		position.y = data.position[1];
	// 		position.z = data.position[2];
	// 		transform.position = position;
	// 	}	
		

		
		
	//}

	public void ExitEditor(){
		motor.EnablePlayerCamera();
		textEditor.SetActive(false);
		camera3.enabled = false;
		outputCode.text = "";
	}

	void Start(){
		panel.SetActive(false);
		textEditor.SetActive(false);
		motor = GetComponent<PlayerMotor>();
		//LoadPlayer();
		// SaveSystem.DeleteFile("/player.fun");
		// SaveSystem.DeleteFile("/playercamera.fun");
		camera2.enabled = false;
		camera3.enabled = false;
	}  

	void Update(){
		// Calculate movement velocity as a 3D vector
		if(camera2.enabled == false && camera3.enabled == false ){
			float _xMovement = Input.GetAxisRaw("Horizontal");
			float _zMovement = Input.GetAxisRaw("Vertical");

			Vector3 _movHorizontal = transform.right * _xMovement; //(1,0,0)
			Vector3 _movVertical = transform.forward * _zMovement; //(0,0,1)

			//Final movement vector
			Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;// For direction

			//Apply Movement
			motor.Move(_velocity);

			//Calculate rotation as a 3D Vector (turning around)
			float _yRotation = Input.GetAxisRaw("Mouse X");
			Vector3 _rotation = new Vector3(0f,_yRotation, 0f ) * lookSensitivity;

			// Apply rotation
			motor.Rotate(_rotation);


			//Calculate camera rotation as a 3D Vector (turning around)
			float _xRotation = Input.GetAxisRaw("Mouse Y");
			Vector3 _cameraRotation = new Vector3(_xRotation,0f, 0f ) * lookSensitivity;

			// Apply camera rotation
			motor.RotateCamera(_cameraRotation);
		}

		


		//Raycasting {

		Vector3 position;
		position.x = this.transform.position.x;	
		position.y = 45f;
		position.z = this.transform.position.z;
		Debug.DrawRay(position, this.transform.forward * distanceToSee, Color.magenta);
        if(Physics.Raycast(position, this.transform.forward, out whatIHit, distanceToSee)){
        	string current = whatIHit.collider.gameObject.name;

        	//Debug.Log(" I touched " + whatIHit.collider.gameObject.name);
        	if(current == "table"){
        		interact.text = "Press 'E' to interact with\n[ " + whatIHit.collider.gameObject.name +" ]";

    			if (Input.GetKeyDown(KeyCode.E)){
    				motor.DisablePlayerCamera();
    				camera2.enabled = true;
    			}
    			else if (Input.GetKeyDown(KeyCode.Escape)){
        			motor.EnablePlayerCamera();
    				camera2.enabled = false;
	    		}

	    		if(camera2.enabled == true){
	    			panel.SetActive(false);
	    		}
	    		else{
	    			panel.SetActive(true);
	    		}
        		
        	}
        	else if(current == "computer"){
        		interact.text = "Press 'E' to interact with\n[ " + whatIHit.collider.gameObject.name +" ]";
        		if (Input.GetKeyDown(KeyCode.E)){
        			panel.SetActive(false);
        			textEditor.SetActive(true);
        			motor.DisablePlayerCamera();
    				camera3.enabled = true;
        		}

	    		if(camera3.enabled == true){
	    			panel.SetActive(false);
	    		}
	    		else{
	    			panel.SetActive(true);
	    		}
        	}   

        	else if(current == "Door"){
        		interact.text = "Press 'E' to interact with\n[ " + whatIHit.collider.gameObject.name +" ] To Return to MainMenu";
        		panel.SetActive(true);
        		if (Input.GetKeyDown(KeyCode.E)){
        			SceneManager.LoadScene("main_menu");
        		}

        	}	

        	else{
        		panel.SetActive(false);
        	}
        }
        else{
        	
        	panel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
			motor.EnablePlayerCamera();
			camera2.enabled = false;
	    }
	}
}
