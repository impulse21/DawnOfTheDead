﻿using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
	public uint selectedWeapon = 0;

	// Use this for initialization
	void Start () 
	{
		SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void SelectWeapon()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			if(i == selectedWeapon)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
			else
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}
