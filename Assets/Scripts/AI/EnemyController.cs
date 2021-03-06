﻿using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
	
	public int health = 100; 		// Starting health
	public int scoreValue = 10;		// Death score value

    ItemDrop itemDrop;
	int m_currentHealth;
	bool isDead = false;

    void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }

	void Awake()
	{
		m_currentHealth = health;
		isDead = false;
	}

    void OnEnable()
    {
        m_currentHealth = health;
        isDead = false;
    }

	// Update is called once per frame
	void Update () 
	{
		
	}
    
	public void TakeDamage(int dmg)
	{
		if(isDead)
		{
			return;
		}

		// TODO DMG auto
		m_currentHealth -= dmg;
		Debug.Log("Enemy " + gameObject.name + " took Dmg [" + m_currentHealth.ToString() + "]");

		if(m_currentHealth <= 0)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;
		gameObject.SetActive(false);

		Debug.Log("Enemy " + gameObject.name + " is Dead"); 
		ScoreManager.score += scoreValue;
        DropItem();
        // TODO Set Collider to trigger
        // Play Death Audio
        // Expolode
    }

    void DropItem()
    {
        if(itemDrop != null)
        {
            itemDrop.DropItem();
        }
    }
}
