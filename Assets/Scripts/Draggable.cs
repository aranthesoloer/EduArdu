using UnityEngine;
public class Draggable : MonoBehaviour
{
    Vector3 dist;
    Vector3 origin;
    Vector3 inventory;
    float posX;
    float posY;

    //Code implementation
    private int codePin;
    private float delay;
    private bool hasCode;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private BreadBoard board;

    [SerializeField]
    private Draggable resistor;

    [SerializeField]
    private Material glow;
    [SerializeField]
    private Material off;

    private Material [] materials;

    public float zvalue1 = 0f;
    public float zvalue2 = 3f;

    public float dropValue;

    private float grabZvalue = 55.18f; 

    private float verticalLength = 15;
    RaycastHit hit1;
    RaycastHit hit2;

    string previousFirst;
    string previousSecond;

    Vector3 rayPosition1;
    Vector3 rayPosition2;


    private GameObject pin1 = null;
    private GameObject pin2 = null;
    
    MeshRenderer rend;
    void DetectDropPoints(){
        Debug.DrawRay(rayPosition1,Vector3.down * verticalLength, Color.magenta);
        Debug.DrawRay(rayPosition2,Vector3.down * verticalLength, Color.yellow);

        if( Physics.Raycast(rayPosition1, Vector3.down, out hit1, verticalLength) & 
            Physics.Raycast(rayPosition2, Vector3.down, out hit2, verticalLength)  ){
            GameObject first = hit1.collider.gameObject;
            GameObject second = hit2.collider.gameObject;

            if(first.name != "table" & second.name!="table"){
                if(first.name == previousFirst & second.name == previousSecond){
                    board.changeAvailability(true, previousFirst, previousSecond);
                }
                board.hover(first.name, second.name);
            }
            
        }
        else{
            board.displayAvailability();
        }
    }

    void setPositionAfterDrop(){
        Debug.DrawRay(rayPosition1,Vector3.down * verticalLength, Color.magenta);
        Debug.DrawRay(rayPosition2,Vector3.down * verticalLength, Color.yellow);

        if( Physics.Raycast(rayPosition1, Vector3.down, out hit1, verticalLength) & 
            Physics.Raycast(rayPosition2, Vector3.down, out hit2, verticalLength)  ){
            GameObject first = hit1.collider.gameObject;
            GameObject second = hit2.collider.gameObject;

            //Debug.Log("--------  Dropping to: " + first.name + "& " + second.name + "-----------------");
            if ( first.name == "Inventory" & second.name == "Inventory" ){
                transform.position = inventory;
                origin = inventory;
                pin1 = null;
                pin2 = null;
            }


            else if(first.name != "table" & second.name !="table" & first.name != "resistor" & second.name !="resistor" ){
                if( first.GetComponent<Droppable>().isAvailable() & second.GetComponent<Droppable>().isAvailable() ){
                    board.changeAvailability(false, first.name, second.name);
                    pin1 = first;
                    pin2 = second;
                    previousFirst = first.name;
                    previousSecond = second.name;
                    origin.x = first.transform.position.x;
                    origin.y = dropValue;
                    if(name == "resistor"){
                        origin.z = 0.45f + (first.transform.position.z + second.transform.position.z)/2;
                    }
                    else{
                        origin.z = (first.transform.position.z + second.transform.position.z)/2;
                    }
                    
                    transform.position = origin;
                }
                else{
                    transform.position = origin;
                }
            }
            else{
                transform.position = origin;
            }
            
        }
        else{
            transform.position = origin;
        }

    }

    void OnMouseDown(){
        dist = cam.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    void OnMouseDrag(){
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = cam.ScreenToWorldPoint(curPos);
        worldPos.y = grabZvalue;  
        transform.position = worldPos;
        board.displayAvailability();
        DetectDropPoints();
    }
    
    void OnMouseUp(){
        board.hideAvailability();
        board.updateAvailability();
        setPositionAfterDrop();
    }

    void Start(){
        origin.x = transform.position.x;
        origin.y = transform.position.y;
        origin.z = transform.position.z;
        inventory = origin;
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        materials = rend.sharedMaterials;
        rend.sharedMaterials = materials;
        hasCode = false;
        
    }

    void Update(){
        rayPosition1.x = transform.position.x; 
        rayPosition1.y = transform.position.y;
        rayPosition1.z = transform.position.z - zvalue1;
        rayPosition2.x = transform.position.x; 
        rayPosition2.y = transform.position.y;
        rayPosition2.z = transform.position.z + zvalue2;
        board.updateAvailability();

        if (Input.GetKeyDown(KeyCode.Q)){
            Debug.Log(" ------ " + getPin1() + " and " + getPin2() );
            Debug.Log("------ " + hasCode + " and " + codePin + " and " + delay +" --------");
        }
        if( name == "LED" && pin1 != null && pin2 != null){
            if( getPin1()!= "none" && getPin2()!= "none" && 
                (pin1.GetComponent<Droppable>().checkNeighborsWithResistor()  || pin2.GetComponent<Droppable>().checkNeighborsWithResistor() ) &&
                (resistor.getPin1() == "GND 1" || resistor.getPin1() == "GND 2" || 
                resistor.getPin2() == "GND 2" || resistor.getPin2() == "GND 2") ) {
                //ledGlow();
                blinking();
                
            }
            else{
                ledOff();
            }
        }
        else{
            ledOff();
        }
       
        
    }
    string getPin1(){
        if(pin1 != null){
            return pin1.GetComponent<Droppable>().getPortNumber();
        }
        else{
            return "none";
        }
    }
    string getPin2(){
        if(pin2 != null){
            return pin2.GetComponent<Droppable>().getPortNumber();
        }
        else{
            return "none";
        }
    }

    //LED Mechanics
    void ledGlow(){
        if(name == "LED"){
            materials[1] = glow;
            rend.sharedMaterials = materials;
        }
        
    }

    void ledOff(){
        if(name == "LED"){
            materials[1] = off;
            rend.sharedMaterials = materials;
        }
        
    }
    void blinking(){
        if(name == "LED"){
            if(hasCode && getPin1() == "" + codePin && getPin2() == "" + codePin  ){
                ledGlow();
                Invoke("ledOff",delay);
            }
            else{
                ledGlow();
            }
        }
    }

    //Coding Implementation
    public void setCodePin(int x){
        codePin = x;
    }
    public void setDelay(float x){
        delay = x;
    }
    public void setHasCode( bool x){
        hasCode = x;
    }
}
