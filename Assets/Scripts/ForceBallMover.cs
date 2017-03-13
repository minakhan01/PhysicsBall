using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBallMover : MonoBehaviour {

	// Use this for initialization
    Vector3 forceVector;
    public GameObject ball;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveForceBall(Vector3 position)
    {
        Debug.Log("moveForceBall" + position);
        forceVector = position;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forceVector*10, Color.green);
    }

    public void applyForce()
    {
        Debug.Log("applyForce");
        ball.GetComponent<Rigidbody>().AddForce(forceVector);
        ball.GetComponent<BallManager>().PlayBall();
    }
}
