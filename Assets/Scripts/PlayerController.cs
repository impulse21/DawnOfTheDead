﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAimInputType
{
    Mouse,
    Joystick
}

public class PlayerController : MonoBehaviour 
{
    public EAimInputType aimInputType = EAimInputType.Joystick;

    [Header("Movement")]
	public float TurnRate;
	public float Speed;

	// Private varables
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();        
	}

	// Update is called once per frame
	void Update () 
	{
        processAim();        
    }
	
	// Update called at a fix rate
	void FixedUpdate()
	{
        processMovement();
    }

	protected void processMovement()
	{
		float horizontalMov = Input.GetAxis("Horizontal");
		float verticalMov = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2(horizontalMov, verticalMov);

		rigidBody.velocity = movement.normalized * Speed;
	}

	protected void processAim()
	{
        switch(aimInputType)
        {
        case EAimInputType.Joystick:
            handleJoystickAim();
            break;
        default:
            handleMouseAim();
            break;
        }
	}

    protected void handleMouseAim()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        target.x += transform.position.x;
        target.y += transform.position.y;

        Vector3 vectorToTarget = target - transform.position;

        rotateToTarget(vectorToTarget);
    }

    protected void rotateToTarget(Vector3 target)
    {
        transform.LookAt(target, Vector3.forward);

        //Lock any other rotation
        Vector3 lockVector = new Vector3(0, 0, -transform.eulerAngles.z);
        transform.eulerAngles = lockVector;
    }

    private void handleJoystickAim()
    {
        float aimX = Input.GetAxis("AimX");
        float aimY = Input.GetAxis("AimY");

        Vector3 aimDir = new Vector3(aimX, aimY, 0.0f);

        if (aimDir != Vector3.zero)
        {
            transform.up = aimDir;
        }
    }
}

