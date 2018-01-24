﻿using UnityEngine;

public abstract class BaseWeaponFire : MonoBehaviour 
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
	public float effectsDisplayTime	= 0.2f;			// The delay between effects
 	protected float timer;                          // A timer to determine when to fire.

	/** Abstract methods 		*/
	public abstract void Shoot();
	protected abstract void DisableAffects();

	/** End Abstract methods 	*/
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward) * 100;
		Debug.DrawRay(transform.position, fwd * 100, Color.red, 4.0f);

		// Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            // ... shoot the gun.
             Shoot ();
			 timer = 0;
        }

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			//DisableAffects();
		}
	}

	void FixedUpdate()
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 800))
            print("There is something in front of the object!");
	}
}
