using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public EndPoint endPoint;
    public Transform target;
    public float smoothTime;
    private Vector3 cameraVelocity = Vector3.zero;
    private Camera _mainCamera;

    void Awake () 
    { 
        _mainCamera = Camera.main;
    }

	private void Start() {
		target = GameObject.FindObjectOfType<PlayerController>().transform;
	}

	void Update()
    {
        if (endPoint.CelebrationPlaying)
        {
            transform.position = Vector3.SmoothDamp(transform.position, endPoint.Bed.transform.position + new Vector3(-6, 10, -6), ref cameraVelocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + new Vector3(-6, 10, -6), ref cameraVelocity, smoothTime);
        }
    }
}