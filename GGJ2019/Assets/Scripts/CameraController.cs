using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothTime;
    private Vector3 cameraVelocity = Vector3.zero;
    private Camera mainCamera;

    void Awake () 
    { 
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + new Vector3(0, 5, -6), ref cameraVelocity, smoothTime);
    }
}