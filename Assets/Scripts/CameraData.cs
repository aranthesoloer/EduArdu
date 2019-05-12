// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraData{
    public float [] rotation;

    public CameraData (PlayerMotor cam){
    	rotation = new float[3];
    	rotation[0] = cam.transform.rotation.x;
    	rotation[1] = cam.transform.rotation.y;
    	rotation[2] = cam.transform.rotation.z;
    }
}
