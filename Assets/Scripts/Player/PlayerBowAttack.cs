﻿using UnityEngine;
using System.Collections;

public class PlayerBowAttack : ProjectileWeapon
{
	private GameObject m_goActiveArrow;
	private Rigidbody m_rigidActiveArrow;
	public bool m_bIsShooting { get; private set;}
	[SerializeField]
	private Rigidbody m_rigidPlayer;


	// Use this for initialization
	protected void Start () 
	{
		base.Start ();
		m_fDestroyDelay = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_bIsShooting = false;
		if (m_goActiveArrow != null)
		{
			m_bIsShooting = true;
		} 
		else
		{
			m_goActiveArrow = null;
			m_rigidActiveArrow = null;
		}
	}

	public override void Fire()
	{
		m_goActiveArrow = GameObject.Instantiate (m_goProjectilePrefab, transform.position, transform.rotation) as GameObject;
		m_goActiveArrow.transform.parent = this.transform;
		m_rigidActiveArrow = m_goActiveArrow.GetComponent<Rigidbody> ();
		m_rigidActiveArrow.AddForce ((transform.forward + m_v3ShotDirectionOffset) * m_fShotPower, ForceMode.Impulse);
	}

	public void Teleport()
	{
		m_rigidPlayer.MovePosition (m_rigidActiveArrow.position);
		StartCoroutine (DestroyProjectile (m_goActiveArrow));
		m_goActiveArrow = null;
		m_rigidActiveArrow = null;
		m_bIsShooting = false;
	}
}