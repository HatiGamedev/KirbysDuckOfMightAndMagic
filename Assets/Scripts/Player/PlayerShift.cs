using UnityEngine;
using System.Collections;

public class PlayerShift : MonoBehaviour {

	private Animator anim;
	private PlayerMovement movement;
	private PlayerShiftModel model;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		movement = GetComponent<PlayerMovement>();
		model = GetComponent<PlayerShiftModel>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1"))
		{
			model.NextState(PlayerShiftModel.State.Sword);
		}
		else
		{
			model.NextState(PlayerShiftModel.State.Default);
		}
	}
}
