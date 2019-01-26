using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoBehaviour
{

    private Transform target;

    public Vector3 MULTIPLIER = new Vector3(1f, 1f, 1f);
    public float GRAVITY = 0.3f;
    public float RESISTANCE = 0.3f;

	public float MAX_AX = 15f;
    public float MAX_AY = 2.5f;

	private Vector3 initAcceleration;

    Vector3 MAX_PUSH = new Vector3(4, 3, 0);

    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -1f, 0);


    private bool grounded = false;
    private bool controllable = true;

    // Use this for initialization
    void Start()
    {
        this.target = gameObject.transform;
    }


    // Update is called once per frame
    void Update()
    {
        //if (this.grounded == true && controllable == true)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        this.Push(new Vector3(10, 2, 2));
        //    }

        //}

        if (this.grounded == false)
        {
            if (acceleration.y > -1f)
            {
                acceleration = new Vector3(acceleration.x, Mathf.Max(acceleration.y - GRAVITY, -1), acceleration.z);
            }
        }else{
            if (velocity.x > 0)
            {
                acceleration = new Vector3(acceleration.x - RESISTANCE, acceleration.y, acceleration.z);
            }
        }
        this.velocity += new Vector3(acceleration.x * MULTIPLIER.x, acceleration.y * MULTIPLIER.y, acceleration.z * MULTIPLIER.z) * Time.deltaTime;
        this.velocity = new Vector3(Mathf.Max(this.velocity.x, 0),this.velocity.y , this.velocity.z);
       
        if(velocity == Vector3.zero){
            controllable = true;
        }else{
            controllable = false;
        }

        if(this.grounded == false){
            DetectFloor();
        }
        //Debug.Log(this.velocity);
        this.target.position += velocity;
    }


    public float getResistance(float velocityx){
        return 0;
    }

    private void Grounding(){
        
    }

    private void DetectFloor(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (hit.point.y >= this.target.position.y + this.velocity.y)
            {
                OnFloorHit(hit);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }


    private void OnFloorHit(RaycastHit hit){
        this.grounded = true;
        this.target.position = new Vector3(this.target.position.x, hit.point.y, this.target.position.z);
        velocity = new Vector3(velocity.x, 0 ,velocity.z);
        acceleration = Vector3.zero;
    }

    public void Push(Vector3 dragAngle, float dragDistance){
        float ax = Mathf.Lerp(0, MAX_AX, dragDistance);
        float ay = Mathf.Lerp(0, MAX_AY, dragDistance);
        float az = 0;

        Vector3 targetAcceleration = new Vector3(ax, ay, az);
        Debug.Log("Push");
        this.grounded = false;
        this.velocity = new Vector3(targetAcceleration.x * MULTIPLIER.x, targetAcceleration.y * MULTIPLIER.y, 0) * Time.deltaTime;
        this.acceleration = new Vector3(0, targetAcceleration.y * MULTIPLIER.y, 0);
        this.initAcceleration = this.acceleration;
    }


    //public void Bounce(){
    //    Rebounce(0.3f);
    //}

    //public void Rebounce(float rebounceMultiplier){
    //    if (rebounceMultiplier > 0)
    //    {
    //        acceleration = initAcceleration * rebounceMultiplier;
    //        rebounceMultiplier -= 0.1f;
    //    }
    //}
}
