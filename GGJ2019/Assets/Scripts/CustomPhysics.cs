using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoBehaviour
{

    private Transform target;
    private AudioSource AS;
    public AudioClip bouncySound;

    public Vector3 MULTIPLIER = new Vector3(1f, 1f, 1f);
    public float GRAVITY = 0.3f;
    public float RESISTANCE = 0.3f;

    public float MAX_AX = 10f;
    public float MAX_AY = 1.5f;

    private Vector3 initAcceleration;

    Vector3 MAX_PUSH = new Vector3(4, 3, 0);

    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -1f, 0);

    public bool grounded = false;
    public bool collided = false;
    public bool controllable = true;
    private Vector3 dragAngle = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start()
    {
        this.target = gameObject.transform;
        this.AS = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (this.grounded == false)
        {
            if (acceleration.y > -1f)
            {
                acceleration = new Vector3(acceleration.x, Mathf.Max(acceleration.y - GRAVITY, -1), acceleration.z);
            }
        }
        else
        {
            if ((velocity.x > 0 && dragAngle.x > 0) || (velocity.x < 0 && dragAngle.x < 0))
            {
                acceleration = new Vector3(acceleration.x - RESISTANCE * dragAngle.x, acceleration.y, acceleration.z);
            }
            else
            {
                velocity = new Vector3(0, velocity.y, velocity.z);
                acceleration = new Vector3(0, acceleration.y, acceleration.z);
            }

            if ((velocity.z > 0 && dragAngle.z > 0) || (velocity.z < 0 && dragAngle.z < 0))
            {
                acceleration = new Vector3(acceleration.x, acceleration.y, acceleration.z - RESISTANCE * dragAngle.z);
            }
            else
            {
                velocity = new Vector3(velocity.x, velocity.y, 0);
                acceleration = new Vector3(acceleration.x, acceleration.y, 0);
            }
            DetectNoFloor();
        }

        this.velocity += new Vector3(acceleration.x * MULTIPLIER.x, acceleration.y * MULTIPLIER.y, acceleration.z * MULTIPLIER.z) * Time.deltaTime;
        //float magnitute = Mathf.Max(this.velocity.x, 0);
        //Debug.Log(magnitute)
        //float vx = magnitute * Mathf.Cos(transform.eulerAngles.y - 90);
        //float vz = magnitute * Mathf.Sin(transform.eulerAngles.y - 90);

        //this.velocity = new Vector3(vx, this.velocity.y, vz);

        if (velocity == Vector3.zero)
        {
            controllable = true;
            collided = false;
        }
        else
        {
            controllable = false;
        }

        if (this.grounded == false)
        {
            DetectFloor();
        }

        //Debug.Log(this.velocity);
        DetectFront();


        this.target.position += velocity;
    }


    private void DetectFront()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f,0), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position+ new Vector3(0, 0.2f, 0), transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            float diffX = Mathf.Abs(hit.point.x - this.target.position.x);
            float diffZ = Mathf.Abs(hit.point.z - this.target.position.z);

            if (Mathf.Abs(this.velocity.x) > diffX)
            {
                if (collided == false)
                {
                    OnWallHit(hit);
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void OnWallHit(RaycastHit hit)
    {
        Debug.Log(hit.normal);
        this.target.position = new Vector3(hit.point.x, this.target.position.y, this.target.position.z);

        float newVx = Mathf.Approximately(hit.normal.x, 0) ? velocity.x : Mathf.Abs(velocity.x) * hit.normal.x;
        float newVy = velocity.y;
        float newVz = Mathf.Approximately(hit.normal.z, 0) ? velocity.z : Mathf.Abs(velocity.z) * hit.normal.z;

        float newAx = Mathf.Approximately(hit.normal.x, 0) ? acceleration.x : Mathf.Abs(acceleration.x) * hit.normal.x;
        float newAy = acceleration.y;
        float newAz = Mathf.Approximately(hit.normal.z, 0) ? acceleration.z : Mathf.Abs(acceleration.z) * hit.normal.z;

        velocity = new Vector3(newVx, newVy, newVz);
        acceleration = new Vector3(newAx, newAy, newAz);
        collided = true;
    }


    private void DetectNoFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.01f, 0), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position + new Vector3(0,0.01f,0), transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            grounded = false;
        }
    }

    private void DetectFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.01f, 0), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.01f, 0), transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (hit.point.y >= this.target.position.y + this.velocity.y - 0.01f)
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


    private void OnFloorHit(RaycastHit hit)
    {
        this.grounded = true;
        this.target.position = new Vector3(this.target.position.x, hit.point.y, this.target.position.z);
        velocity = new Vector3(velocity.x, 0, velocity.z);
        acceleration = Vector3.zero;
    }

    public void Push(Vector3 dragAngle, float dragDistance)
    {
        if (dragDistance < 0.1f)
        {
            return;
        }

        AS.PlayOneShot(bouncySound);

        this.dragAngle = dragAngle;
        float ax = MAX_AX * dragDistance * dragAngle.x;
        float ay = Mathf.Max(1.5f, MAX_AY * dragDistance);
        float az = MAX_AX * dragDistance * dragAngle.z;

        Vector3 targetAcceleration = new Vector3(ax, ay, az);
        Debug.Log(targetAcceleration);
        this.grounded = false;
        this.velocity = new Vector3(targetAcceleration.x * MULTIPLIER.x, targetAcceleration.y * MULTIPLIER.y, targetAcceleration.z * MULTIPLIER.z) * Time.deltaTime;
        this.acceleration = new Vector3(0, targetAcceleration.y * MULTIPLIER.y, 0);
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
