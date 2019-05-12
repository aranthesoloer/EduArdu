// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{
    public float [] position;
    public float [] mouse;

    public PlayerData (PlayerController player){
    	position = new float[3];
    	position[0] = player.transform.position.x;
    	position[1] = player.transform.position.y;
    	position[2] = player.transform.position.z;

    	mouse = new float[3];
    	mouse[0] = Input.GetAxisRaw("Mouse X");
    	mouse[1] = Input.GetAxisRaw("Mouse Y");

    }
}
