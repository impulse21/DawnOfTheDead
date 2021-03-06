﻿using UnityEngine;

public class RangedAttack : MonoBehaviour 
{
	public GameObject projectile;				// Prefab of projectile to file
	public int numOfProjectiles;				// Number of projectiles
	public bool limitedFire;					// Limit the amount of shots to num of projectiles
	public int projectileSpeed = 5;				// Projectile Speed
	public float fireRate = 2.0f;
	public string targetTag = "";				// Target to shoot tag
    public float fireRange = 3.0f;                 // Distance to taraget

	protected float timer;

	private Animator m_animator;
	private ObjectPool m_projectilePool; 		// Object Pool for projectile
	private MoveTowardsPlayer m_movementComp;	// Reference to movement component
    private Transform m_playerTransform;        // Player Transform
	
	// Use this for initialization
	void Start () 
	{
		timer = 0;
		m_projectilePool = new ObjectPool(projectile, 10, limitedFire);
		
		m_movementComp = GetComponent<MoveTowardsPlayer>();
		m_animator = GetComponent<Animator>();

        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInRange())
        {
            Debug.Log("Player is in range");
            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                Shoot();

                timer = 0;
            }
        }
    }

	private void Shoot()
	{
		m_animator.SetTrigger("Attack");
		GameObject fireProjectile = m_projectilePool.getPooledObject();
		
		if(fireProjectile != null)
		{
			fireProjectile.transform.position = transform.position;
			// Not sure why I need to do this. Seems to stem from the asset itsself.
			// This is a temp fix for now
			fireProjectile.transform.rotation = 
				transform.rotation * Quaternion.Euler(0, 0, 180);

			fireProjectile.SetActive(true);

			fireProjectile.GetComponent<Rigidbody2D>().velocity = 
				fireProjectile.transform.up * projectileSpeed;	
		}
	}

    private bool IsPlayerInRange()        
    {
        Vector3 distanceVector = m_playerTransform.position - transform.position;

        Debug.Log("The distance to the player is " + distanceVector.magnitude);

        return (distanceVector.magnitude <= fireRange);
    }
}
