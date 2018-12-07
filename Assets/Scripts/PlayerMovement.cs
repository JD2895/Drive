using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float acceleration = 9.0f;
    public float steering = 2.5f;
    public float setSpeed = 1.0f;
    public float maxSpeed = 5.0f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // -- Get input --
        float h = -Input.GetAxis("Horizontal");
        float v = setSpeed;
        //float v = Input.GetAxis("Vertical");

        // -- Calculate speed from input and acceleration (transform.up is forward) --
        // Determines speed
        Vector2 speed = transform.up * (v * acceleration);
        // Adds speed to object
        rb.AddForce(speed);


        // -- Create car rotation --
        // Gets the angle between the object's velocty and 'up' direction
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        // Rotates the object so that 'up' in in-line with the velcoty
        if (direction != 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / 5.0f);
        } else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / 5.0f);
        }
        
        // -- Change velocity based on rotation --
        Vector2 forward = new Vector2(0.0f, 5f);    // Magnitude doesn't matter, direction does.
        float steeringRightAngle;   
        if (rb.angularVelocity > 0)     // this if statement doesnt make a difference???
        {
            steeringRightAngle = -90;
        } else
        {
            steeringRightAngle = 90;
        }

        
        // Quaternion AngleAxis(float angle, Vector3 axis) - Creates a rotation which rotates angle degrees around axis.
        // Rotations are done in the syntax: rotation_to_be_done = quaternion * rotation_rotation_to_be_done;
        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        //Debug.Log("H= " + h);
        //Debug.Log("ANG ROT= " + rb.rotation);
        //Debug.Log("ANG VEL= " + rb.angularVelocity);
        // Debug.Log("DIR= " + direction);
        // Debug.Log("VEL= " + rb.velocity);

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        // currentSpeed = rb.velocity.magnitude;    // create a private variable "currentSpeed" if you want to track current speed

    }
}
