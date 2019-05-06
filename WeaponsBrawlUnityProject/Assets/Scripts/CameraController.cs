using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cam;
    private CinemachineCameraOffset offsetManager;
    //public GameObject player;       //Public variable to store a reference to the player game object
    //private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    private Func<float> GetCameraZoom;
    private bool isLooking = false;
    public float lookAroundSpeed = 100f;
    private float zoom;
    public float beginZoom = 5f;
    public float cameraZoomSpeed = 5f;
    public float zoomChange = 20f;
    public float scrollBoost = 5f;
    public float maxZoomIn = 5f;
    public float maxZoomOut = 100f;



    void Start()
    {
        cam = transform.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        offsetManager = transform.GetComponent<CinemachineCameraOffset>();
        zoom = beginZoom;
        cam.m_Lens.OrthographicSize = beginZoom;

    }



    void Update()
    {

    }
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {

        MovementHandler();
        ZoomHandler();

    }

    public void OnGUI()
    {

        if (Event.current.type == EventType.ScrollWheel)
        {
            MouseZoom(Event.current.delta.y);
        }
    }

    void MouseZoom(float delta)
    {
        if (delta < 0)
        {
            zoom -= zoomChange * Time.deltaTime * scrollBoost;
            Debug.Log(zoom);
        }
        if (delta > 0)
        {
            zoom += zoomChange * Time.deltaTime * scrollBoost;
            Debug.Log(zoom);
        }

    }
    void KeyZoom()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom -= zoomChange * Time.deltaTime;
            Debug.Log(zoom);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom += zoomChange * Time.deltaTime;
            Debug.Log(zoom);

        }

    }


    private void ZoomHandler()
    {

        KeyZoom();

        // MouseZoom(Input.GetAxis("Mouse ScrollWheel"));

        zoom = Mathf.Clamp(zoom, maxZoomIn, maxZoomOut);

        float cameraZoomDifference = zoom - cam.m_Lens.OrthographicSize;
        cam.m_Lens.OrthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;


    }

    private void MovementHandler()
    {
        if (Input.GetMouseButtonDown(1))
        {

            isLooking = true;
            Debug.Log(isLooking);
        }
        if (Input.GetMouseButtonUp(1))
        {
            isLooking = false;
            Debug.Log(isLooking);
        }

        if (isLooking)
        {
            LookAroundBehaviour();
        }
        else
        {
            offsetManager.m_Offset = new Vector3(0,0,0);
            //follow player behaviour
            // transform.position = player.transform.position + offset;
        }
    }

    private void LookAroundBehaviour()
    {
        if (Input.GetAxis("Mouse X") < 0)
        {
            offsetManager.m_Offset -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookAroundSpeed,
                                        Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookAroundSpeed, 0.0f);
            Debug.Log("fhit");
        }

        else if (Input.GetAxis("Mouse X") > 0)
        {
            Debug.Log("nhit");
            offsetManager.m_Offset -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookAroundSpeed,
                                        Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookAroundSpeed, 0.0f);
        }
        Debug.Log("hit");
    }
}

