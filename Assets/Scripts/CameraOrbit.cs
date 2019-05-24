using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

	[SerializeField]
	private Camera camera1;

	[SerializeField]
	private Camera camera2;

	protected Transform _XForm_Camera;
	protected Transform _XForm_Parent;

	protected Vector3 _LocalRotation; //store rotation of camera pivot every frame
	protected float _CameraDistance = 10f;

	public float MouseSensitivity = 4f;
	public float ScrollSensitivity = 2f;
	public float OrbitSpeed = 10f;
	public float ScrollSpeed = 6f;

	public bool CameraDisabled = false;


    // Start is called before the first frame update
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;

        camera1.enabled = true;
        camera2.enabled = false;
    }

    // LateUpdate is called once per frame, after Update() on every game object in the scene
    void LateUpdate()
    {
        if(!CameraDisabled) {
        	if(Input.GetKeyDown(KeyCode.F)) {
	        	// CameraDisabled = !CameraDisabled;
	        	camera1.enabled = !camera1.enabled;
	        	camera2.enabled = !camera2.enabled;
	        }

        	//Rotation of the Camear based on Mouse Coordinates
        	if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
        		_LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
        		_LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

        		//clamp y rotation to horizon and not flip over at the top
        		_LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);

        		//Zooming
        		if(Input.GetAxis("Mouse ScrollWheel") != 0f) {
        			float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

        			//makes camera zoom faster the further away it is from the target
        			ScrollAmount *= (this._CameraDistance * 0.3f);

        			this._CameraDistance += ScrollAmount * -1f;

        			// makes 1.5m <= camera <= 100m
        			this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.2f, 2.5f);
        		}
        	}
        }

        //y,x,z
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitSpeed);

        if(this._XForm_Camera.localPosition.z != this._CameraDistance * -1f) {
        	this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollSpeed));
        }
    }
}
