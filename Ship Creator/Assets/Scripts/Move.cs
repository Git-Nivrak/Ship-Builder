using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed = 30f;
    Vector3 lastMouseCordinate = Vector3.zero;
    float minFov = 15f;
    float maxFov = 150f;
    float sensitivity = 15f;
    Camera camera;
    private void Start()
    {
        camera = gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
        if (!Input.GetKey(KeyCode.Mouse2))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        } else
        {
            Vector3 mouseDelta = Input.mousePosition - lastMouseCordinate;
            float xmag = Mathf.Abs(mouseDelta.x);
            float ymag = Mathf.Abs(mouseDelta.y);

            if (xmag > ymag)
            {
                //mouse is moving mostly along the x axis

                if (mouseDelta.x > 0)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * fov / 2f);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime * fov / 2f);

                }
            }
            else if (ymag > xmag)
            {
                //mouse is moving mostly along y axis

                if (mouseDelta.y > 0)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * fov /2f);

                }
                else
                {
                    transform.Translate(Vector3.up * Time.deltaTime * fov /2f);
                }
            }
            else
            {
                //NO MOVEMENT
            }
              lastMouseCordinate = Input.mousePosition;
        }
        
    }
}
