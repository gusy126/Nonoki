using UnityEngine;
using System.Collections;

public class BS_Shield : MonoBehaviour {

	//there are animation events on Enable shield collider and disable shield collider. Info them out!


	[Space(10)]
	[Tooltip("To make a working shield you need to do a few things. First, assign up to 5 colliders to this object or it's children (for most shields, one, square box collider is quite enough). Shields can be very precise in shape, so for most cases it is a good idea to use simplified, bigger colliders than the actual mesh of the shield (unless you plan on using the added precision of it's shape. Then follow the variables below to customise your shield!  [INFO] This is an info bool. It does nothing, it just holds the mouse-over instructions.")]
	public bool _Info;
	[Tooltip("Before we start, if you plan on using this Shield with a character or object equiped with the BS_Main_Health system, please, assign it here. Otherwise the hit detection may not work properly")]
	public BS_Main_Health _ParentHealth; //it's referenced in other scripts.

	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. It is a point behind the center of the shield (from the safe side, behind the shield's collider (so it's simply a point near the center of the Shield's Wielder, if it's humantoid, for scale). It's used to calculate if the attack was coming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield and place it accordingly around the shield. Then put it's reference into this variable. [INFO 2:] If you're experiencing that the shild is being hit though the attack was clearly coming from the back (like if the shield is quite big or the attack swipe is large), don't be affraid to pull this spot a bit further behind the shield wielder")]
	public Transform _ShieldBackSpot;
	[Tooltip("Tha back spot is mostly used around the center of the shield-wielding character. If the shield is instantiated on the go, you can assign the Back Spot remotely, by typing it's name here - it will be find and assigned automaticaly at the spawn of this object. [INFO]: Mind the upper case letters!")]
	public string RemotelyFindShieldBackSpot;

	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. It is a point around the very center of the shield (can be a tiny bit in front of the shield's collider (so from the Attacker side). It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldCenterSpot;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot1;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot2;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot3;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot4;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot5;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot6;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot7;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot8;


	[Space(10)]
	[Tooltip("You can play randomised Block Animations upon, well, blocking an attack. If so, how many of them can we randomise from?")]
	public int _numberOfBlockAnimations;
	[Tooltip("If we're using Mecanim to play our animations, put it here. It will play one of the states: Block1, Block2, Block3, Block4 or Block5.")]
	public Animator _BlockAnimator;
	[Tooltip("If we're using Legacy Animations, put the animation component here. It will play one of the following animations: Block1, Block2, Block3, Block4 or Block5")]
	public Animation _BlockLegacyAnimation;

	[Space(10)]
	[Tooltip("If we plan on playing sounds on blocking, assign a reference to the AudioSource here")]
	public AudioSource SoundSource;
	[Tooltip("How many Block Sounds can we randomise from?")]
	[Range(0,5)]
	public int _numberOfBlockSounds;
	public AudioClip BlockSound1;
	public AudioClip BlockSound2;
	public AudioClip BlockSound3;
	public AudioClip BlockSound4;
	public AudioClip BlockSound5;

	[Space(10)]
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider1;
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider2;
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider3;
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider4;
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider5;
	[Tooltip("Disable shield when dying?")]
	public bool DisableShieldOnDeath;


	private int _BlockAnim_Randomisation;
	private int _BlockSound_Randomisation;


	void Start()
	{
		if (RemotelyFindShieldBackSpot != "" && RemotelyFindShieldBackSpot != " ") {
			if( GameObject.Find(RemotelyFindShieldBackSpot).transform == null)
			{
				Debug.LogError("Hey, umm, the BS Shield on "+transform.name + " cannot find it's Back Spot by Remote Finding. Check if you typed in the Back Spot's name right in the variable field please!");
			}
			_ShieldBackSpot = GameObject.Find(RemotelyFindShieldBackSpot).transform;
		}
	}

	void Update()
	{
		if (_ParentHealth._health <= 0) {
			DisableShieldCollider();
			this.enabled = false;
		}
	}
	public void BlockStagger()
	{
		if(_numberOfBlockAnimations != 0)
		{
			_BlockAnim_Randomisation = Random.Range(1,_numberOfBlockAnimations+1);   //Block animations
		}
		if(_numberOfBlockAnimations == 0)
		{
			_BlockAnim_Randomisation = 0;
		}
		
		if(_BlockAnimator != null)
		{
			if(_BlockAnim_Randomisation == 1)
			{
				_BlockAnimator.Play("Block1");
			}
			if(_BlockAnim_Randomisation == 2)
			{
				_BlockAnimator.Play("Block2");
			}
			if(_BlockAnim_Randomisation == 3)
			{
				_BlockAnimator.Play("Block3");
			}
			if(_BlockAnim_Randomisation == 4)
			{
				_BlockAnimator.Play("Block4");
			}
			if(_BlockAnim_Randomisation == 5)
			{
				_BlockAnimator.Play("Block5");
			}
		}
		if(_BlockLegacyAnimation != null)
		{
			if(_BlockAnim_Randomisation == 1)
			{
				_BlockLegacyAnimation.Play ("Block1");
			}
			if(_BlockAnim_Randomisation == 2)
			{
				_BlockLegacyAnimation.Play ("Block2");
			}
			if(_BlockAnim_Randomisation == 3)
			{
				_BlockLegacyAnimation.Play ("Block3");
			}
			if(_BlockAnim_Randomisation == 4)
			{
				_BlockLegacyAnimation.Play ("Block4");
			}
			if(_BlockAnim_Randomisation == 5)
			{
				_BlockLegacyAnimation.Play ("Block5");
			}
		}

		if(SoundSource != null)
		{
			if(_numberOfBlockSounds >0)
			{
				_BlockSound_Randomisation = Random.Range (1, _numberOfBlockSounds+1);
				
				if(_BlockSound_Randomisation == 1)
				{
					SoundSource.clip = BlockSound1;
					SoundSource.Play();
				}
				if(_BlockSound_Randomisation == 2)
				{
					SoundSource.clip = BlockSound2;
					SoundSource.Play();
				}
				if(_BlockSound_Randomisation == 3)
				{
					SoundSource.clip = BlockSound3;
					SoundSource.Play();
				}
				if(_BlockSound_Randomisation == 4)
				{
					SoundSource.clip = BlockSound4;
					SoundSource.Play();
				}
				if(_BlockSound_Randomisation == 5)
				{
					SoundSource.clip = BlockSound5;
					SoundSource.Play();
				}
				
			}
		}
	}



	public void DisableShieldCollider()
	{
		if (_shieldCollider1 != null) {
			_shieldCollider1.enabled = false;
		}
		if (_shieldCollider2 != null) {
			_shieldCollider2.enabled = false;
		}
		if (_shieldCollider3 != null) {
			_shieldCollider3.enabled = false;
		}
		if (_shieldCollider4 != null) {
			_shieldCollider4.enabled = false;
		}
		if (_shieldCollider5 != null) {
			_shieldCollider5.enabled = false;
		}
	}

	public void EnableShieldCollider()
	{
		if (_shieldCollider1 != null) {
			_shieldCollider1.enabled = true;
		}
		if (_shieldCollider2 != null) {
			_shieldCollider2.enabled = true;
		}
		if (_shieldCollider3 != null) {
			_shieldCollider3.enabled = true;
		}
		if (_shieldCollider4 != null) {
			_shieldCollider4.enabled = true;
		}
		if (_shieldCollider5 != null) {
			_shieldCollider5.enabled = true;
		}
	}

}
