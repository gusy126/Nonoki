using UnityEngine;
using System.Collections;

public class Demo_Enemy_Shielded_AI : MonoBehaviour {

	public Animator _animator;
	public BS_Main_Health _health;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("ShieldToggle", 0, 6);
	}

	void Update()
	{
		if (_health._health <= 0) {
			StopCoroutine("ShieldToggle");
			this.enabled = false;
		}
	}

	private void  ShieldToggle() {
		if (_health._health <= 0) {
			return;
		}
		if (_animator.GetBool ("Defend") == true) {
			_animator.SetBool ("Defend", false);
			return;
		}
		if (_animator.GetBool ("Defend") == false) {
			_animator.SetBool ("Defend", true);
			return;
		}
	}
}
