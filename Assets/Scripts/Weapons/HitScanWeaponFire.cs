﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeaponFire : BaseWeaponFire 
{
    Ray shootRay = new Ray();                       // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.

    void Awake ()
    {
	}

	public override void Shoot()
	{
        Debug.Log("Shooting Weapon");

		shootableMask = LayerMask.GetMask("Shootable");

 		shootRay.origin = transform.position;
        shootRay.direction = transform.TransformDirection(Vector3.up);

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            Debug.Log("We hit something");
            // Try and find an EnemyHealth script on the gameobject hit.
        	IDamageable enemy = shootHit.collider.GetComponent <IDamageable> ();
            enemy.TakeDamage(10.0f);
        }
	}
	protected override void DisableAffects()
	{
	}
}