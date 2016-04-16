using UnityEngine;
using System.Collections;

public class PlayerShiftModel : MonoBehaviour {

	public enum State {
		Default,
		Sword,
		Bow,
		Shield
	}

	public float ShiftDuration = 1f;
	public State ShiftState { get; private set; }

	private Animator anim;

	void Awake()
	{
		ShiftState = State.Default;
	}

	void Start()
	{
		anim = transform.Find("PlayerSprite").GetComponent<Animator>();
	}

	public void NextState(State newState)
	{
		if (ShiftState == newState) return;

		switch(ShiftState)
		{
			case State.Sword:
				handleExitSwordState();
				break;
			case State.Bow:
				handleExitBowState();
				break;
			case State.Shield:
				handleExitShieldState();
				break;
			default:
				break;
		}

		switch(newState)
		{
			case State.Sword:
				handleEnterSwordState();
				break;
			case State.Bow:
				handleEnterBowState();
				break;
			case State.Shield:
				handleEnterShieldState();
				break;
			default:
				break;
		}

		ShiftState = newState;
	}

	#region States
	void handleExitSwordState()
	{
		anim.SetLayerWeight(1, 0.0f);
	}

	void handleEnterSwordState()
	{
		anim.SetLayerWeight(1, 1.0f);
	}

	void handleExitBowState()
	{
		anim.SetLayerWeight (2, 0.0f);
	}

	void handleEnterBowState()
	{
		anim.SetLayerWeight (2, 1.0f);
	}

	void handleExitShieldState()
	{
		anim.SetLayerWeight (3, 0.0f);
		GetComponent<PlayerCanBeShot>().m_bBlocking = false;
	}

	void handleEnterShieldState()
	{
		anim.SetLayerWeight (3, 1.0f);
		GetComponent<PlayerCanBeShot>().m_bBlocking = true;
	}

	#endregion

}
