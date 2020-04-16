using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    GameObject currentObject;
    public GameObject AxisX;
    public GameObject AxisY;
    public GameObject AxisZ;
    GameObject xArrow;
    GameObject yArrow;
    GameObject zArrow;
    List<float> xArrowCollider;
    List<float> yArrowCollider;
    List<float> zArrowCollider;
    Vector3 lastMouseCordinate = Vector3.zero;
    char axis;
    bool isMoving = false;
    Vector3 initLocation;
    int Cooldown;
    Vector3[] vertices;
    List<GameObject> spheres = new List<GameObject>();
    Vector3 FirstVertex = Vector3.zero;
    GameObject FirstVertexObject;
    Vector3 SecondVertex = Vector3.zero;


    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(currentObject);
            currentObject = null;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentObject != null)
        {
            currentObject.transform.Rotate(0, 0, 90);
            foreach (Transform child in currentObject.transform)
            {
                if (child.gameObject == yArrow)
                {
                    yArrow.transform.Rotate(0, 0, -90);
                }
                if (child.gameObject == xArrow)
                {
                    xArrow.transform.Rotate(0, 0, -90);
                }

            }
            
        }

        if (Input.GetKeyDown(KeyCode.V))
        {

            xArrow.GetComponent<CapsuleCollider>().height = 0;
            xArrow.GetComponent<CapsuleCollider>().radius = 0;

            yArrow.GetComponent<CapsuleCollider>().height = 0;
            yArrow.GetComponent<CapsuleCollider>().radius = 0;

            zArrow.GetComponent<CapsuleCollider>().height = 0;
            zArrow.GetComponent<CapsuleCollider>().radius = 0;

            vertices = currentObject.GetComponent<MeshFilter>().mesh.vertices;
            FirstVertexObject = currentObject;
            foreach (Vector3 vert in vertices)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
                sphere.transform.position = currentObject.transform.TransformPoint(vert);
                sphere.transform.localScale = new Vector3(currentObject.transform.localScale.x/22, currentObject.transform.localScale.y / 22, currentObject.transform.localScale.z / 22);
                sphere.tag = "Vert";
                spheres.Add(sphere);
            }
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            foreach (GameObject vert in spheres)
            {
                Destroy(vert);
            }
            xArrow.GetComponent<CapsuleCollider>().height = 10;
            xArrow.GetComponent<CapsuleCollider>().radius = 0.6f;

            yArrow.GetComponent<CapsuleCollider>().height = 10;
            yArrow.GetComponent<CapsuleCollider>().radius = 0.6f;

            zArrow.GetComponent<CapsuleCollider>().height = 10;
            zArrow.GetComponent<CapsuleCollider>().radius = 0.6f;

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Object")
                {
                    if (currentObject == null)
                    {
                        currentObject = hit.transform.gameObject;
                        xArrow = Instantiate(AxisX, currentObject.transform);
                        zArrow = Instantiate(AxisZ, currentObject.transform);
                        yArrow = Instantiate(AxisY, currentObject.transform);
                        foreach (Transform child in currentObject.transform)
                        {
                            if (child.gameObject == zArrow)
                            {
                                zArrow.transform.rotation = Quaternion.Euler(90, 0, 0);
                            }
                            if (child.gameObject == xArrow)
                            {
                                xArrow.transform.rotation = Quaternion.Euler(0, 0, -currentObject.transform.rotation.z - 90);
                            }if (child.gameObject == yArrow)
                            {
                                yArrow.transform.rotation = Quaternion.Euler(0, 0, -currentObject.transform.rotation.z);
                            }
                        }

                     }
                    else
                    {
                        foreach (Transform child in currentObject.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        currentObject = hit.transform.gameObject;
                        xArrow = Instantiate(AxisX, currentObject.transform);
                        zArrow = Instantiate(AxisZ, currentObject.transform);
                        yArrow = Instantiate(AxisY, currentObject.transform);
                    }
                }else if (hit.transform.tag == "Vert")
                {
                    if (FirstVertex == Vector3.zero)
                    {
                        FirstVertex = hit.transform.position;
                    }else if (SecondVertex == Vector3.zero)
                    {
                        SecondVertex = hit.transform.position;
                    }
                    foreach (GameObject vert in spheres)
                    {
                         Destroy(vert);
                    }
                }
                else if (hit.transform.tag == "Axis")
                {

                }

                else
                {
                    if (currentObject != null)
                    {
                        foreach (Transform child in currentObject.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        currentObject = null;
                    }
                    
                }
            } else
            {
                if (currentObject != null)
                {
                    foreach (Transform child in currentObject.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    currentObject = null;
                }
                
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!isMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.gameObject == xArrow)
                    {
                        axis = 'X';
                        isMoving = true;

                    }
                    else if (hit.transform.gameObject == yArrow)
                    {
                        axis = 'Y';
                        isMoving = true;

                    }
                    else if (hit.transform.gameObject == zArrow)
                    {
                        axis = 'Z';
                        isMoving = true;
                    }

                }
            }
            else
            {
                float dragSpeed = 9; 


                if (axis == 'X')
                {
                    float DragX = (Input.GetAxis("Mouse X"));
                    var pos = currentObject.gameObject.transform.localPosition;
                    currentObject.gameObject.transform.localPosition = new Vector3(DragX * Time.deltaTime * dragSpeed + pos.x, pos.y, pos.z);

                }

                if (axis == 'Y')
                {
                    float DragY = (Input.GetAxis("Mouse Y"));
                    var pos = currentObject.gameObject.transform.localPosition;
                    currentObject.gameObject.transform.localPosition = new Vector3(pos.x, pos.y + DragY * Time.deltaTime * dragSpeed, pos.z);
                }

                if (axis == 'Z')
                {
                    float planeY = 0;
                    Transform draggingObject = currentObject.transform;

                    Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    float distance; // the distance from the ray origin to the ray intersection of the plane
                    if (plane.Raycast(ray, out distance))
                    {
                        draggingObject.position = new Vector3(draggingObject.position.x, draggingObject.position.y, draggingObject.position.x + ray.GetPoint(distance).z / 10); // distance along the ray
                    }
                }

            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isMoving = false;
        }
        if (FirstVertex != Vector3.zero && SecondVertex != Vector3.zero)
        {
            Vector3 dist = FirstVertex - SecondVertex;
            FirstVertexObject.transform.position += dist;
            FirstVertex = Vector3.zero;
            SecondVertex = Vector3.zero;
            FirstVertexObject = null;
        }

    }



    

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public Vector3 GetWorldPositionOnPlaneY(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }


}
