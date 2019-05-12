using UnityEngine;
public class Draggable : MonoBehaviour
{
    Vector3 dist;
    Vector3 origin;
    Vector3 inventory;
    float posX;
    float posY;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private BreadBoard board;

    public float zvalue1 = 0f;
    public float zvalue2 = 3f;

    private float grabZvalue = 55.18f; 

    private float verticalLength = 15;
    RaycastHit hit1;
    RaycastHit hit2;

    string previousFirst;
    string previousSecond;

    Vector3 rayPosition1;
    Vector3 rayPosition2;
    

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
                //Debug.Log("-------- Detected Valid Drop Points   " + first + "   "+second +" -----------------");    
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

            Debug.Log("--------  Dropping to: " + first.name + "& " + second.name + "-----------------");
            if ( first.name == "Inventory" & second.name == "Inventory" ){
                transform.position = inventory;
                origin = inventory;
            }


            else if(first.name != "table" & second.name !="table"){
                if( first.GetComponent<Droppable>().isAvailable() & second.GetComponent<Droppable>().isAvailable() ){
                    board.changeAvailability(false, first.name, second.name);
                    previousFirst = first.name;
                    previousSecond = second.name;
                    origin.x = first.transform.position.x;
                    origin.y = 50.85f;
                    origin.z = (first.transform.position.z + second.transform.position.z)/2;
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
        Debug.Log("-------------- Drag ended --------------------");
        //board.display();
    }

    void Start(){
        origin.x = transform.position.x;
        origin.y = transform.position.y;
        origin.z = transform.position.z;
        inventory = origin;
    }

    void Update(){
        rayPosition1.x = transform.position.x; 
        rayPosition1.y = transform.position.y;
        rayPosition1.z = transform.position.z - zvalue1;
        rayPosition2.x = transform.position.x; 
        rayPosition2.y = transform.position.y;
        rayPosition2.z = transform.position.z + zvalue2;
        //board.updateAvailability();
    }

}
