using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoBehaviour {

    private Transform target;

    private Vector3 MULTIPLIER = new Vector3(1f, 1f , 1f);
    private Vector3 GRAVITY = new Vector3(0, 0.3f, 0);
    private Vector3 RESISTANCE = new Vector3(0.2f, 0, 0);

    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -1f, 0);

    float mockLimitPositionY = 0;   //TEST

	// Use this for initialization
	void Start () {
        this.target = gameObject.transform;
	}


	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0)){
            this.Push(new Vector3(4, 3, 2));
        }

        if(acceleration.y > -1f){
            acceleration = new Vector3(acceleration.x, acceleration.y - GRAVITY.y, acceleration.z);
        }
        if (acceleration.x > RESISTANCE.x)
        {
            acceleration = new Vector3(acceleration.x - RESISTANCE.x, acceleration.y, acceleration.z);
        }

        this.velocity += new Vector3 (acceleration.x * MULTIPLIER.x, acceleration.y * MULTIPLIER.y, acceleration.z * MULTIPLIER.z) * Time.deltaTime;


        this.target.position += velocity;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }


        if (target.position.y < mockLimitPositionY)
        {
            target.position = new Vector3(target.position.x, mockLimitPositionY, target.position.z);
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
        }
	}




	public void Push(Vector3 targetPosition){
        Debug.Log("Push");

        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(targetPosition.x * MULTIPLIER.x, targetPosition.y * MULTIPLIER.y, 0);
    }

    public void Rebounce(){
        
    }
}
