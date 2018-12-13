using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BS_Marker_Manager : MonoBehaviour {
	

	//Manager should be close to the weapon's model - ideally in the center.


	[Tooltip("To start, simply assign weapon's Markers (from a Prefab Folder) around the damage-dealing zone (like a blade) as children of any object in the Weapon's hierarchy (preferably as children of this Weapon Manager object) and fill out the variables!     [INFO]: It's an info bool. It does nothing, it just stores the instruction Tooltip")]
	public bool Instructions;

	[Header ("Basic Features")]
	[Tooltip("Choose which Object stores all the weapon's Makers. If it's the same Object as the one with this script attached, then you can leave it blank.")]
	public Transform _MarkersParent; 
	[Tooltip("What is the Tag of objects which can be hit? (Like enemies, exploding barrels, enemy players, etc.) [INFO]: You can set the target Layers on each individual Marker too. Everything what is not a Target or a Shield is considered a wall.")]
	public string _targetTag = "Enemy";
	[Tooltip("If we're using shields (BS_Shield system), what is their Tag? Everything what is not a Target or a Shield is considered a wall.")]
	public string _shieldTag = "Shield";
	[Tooltip("How much damage should this weapon deal with each hit? It can be changed with an Animation Event or a function call SetDamage<number>(), below.")]
	public int _damage = 1; 
	[Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage1() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	public int _damageType1;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage2() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType2;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage3() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType3;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage4() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType4;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage5() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType5;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage6() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType6;
	 [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage7() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType7;
	 [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage8() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType8;
	 [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage9() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType9;



	 [Tooltip("A weapon can work in two ways: Manual and Continuous. Manual bases on an idea that each attack (like a sword swing) can hit each target only once - it then needs to be manualy reloaded through a function call or an Animation Event (for example by ClearTargets() - explained in the Info Bool, at the bottom of this Inspector window). This method is very precise, as you can be certain that each Target will get damage and  Hurt animation (on BS_Main_Health script) triggered only once with each swing. Manual should be used in most situations, mostly for animation-driven combat (like Dark Souls or God of War). The Continuous mode deals damage constantly, in a given time interval, giving you a constant damage-dealing weapon. It's better for VR and games with free-moving weapons (like when the Player can swing his sword by moving his mouse in any desired direction) - the downside of Continuous damage is that it takes away the precise control of the damage dealt (as each Target may be hit more than once depending on how fast the weapon is being driven around). Continuous should be used with blades which can move freely, independent from animations. [INFO:] Continuous damage is not fit for working with shields (BS_Shield script objects).")]
	public bool ContinuousDamage;
	[Tooltip("If you choose the Continuous damage, what should the interval of damage dealing be? (In seconds)")]
	public float ContinuousDamageInterval = 0.2f;
	float _ContinuousDamage_Timer;
	[Tooltip("You can spawn Blood upon hit both from the Weapon side and from the BS_Main_Health side. If you want to spawn it from Weapon side, put your Blood prefab here or leave empty if there's no blood (or sparks or anything equivalent) upon hit!")]
	public GameObject Blood;
	bool HitFlesh;
	bool HitWall;
	bool HitShield;
	BS_Marker [] _markers; 
	List<Transform> _Targets_Raw_Hit = new List<Transform>(); //Targets initialy hit by the blade (pre-check
	List<Transform> _Used_Targets = new List<Transform>(); //Targets which were excluded from being hit or were already hit in that frame
	List<Transform> _Shields_Hit = new List<Transform> ();
	List<Vector3> _Blade_Direction = new List<Vector3>();   
	List<Vector3> _Blade_Startpoint = new List<Vector3>();
	List<Vector3> _wallHitPositions = new List<Vector3>();
	bool _markersAreEnabled;
	GameObject _missSparks; //it's an inside variable. For true miss sparks look for WallHitSparks.
	float dh; //These DH and DS variables are Distances to the shield spots. Whie the shield is active, DH ("Distance to Health", distance to the back point of the shiled) has to be less than all the other shield edge spots (DS, "Distance to Shield")
	float ds1;
	float ds2;
	float ds3;
	float ds4;
	float ds5;
	float ds6;
	float ds7;
	float ds8;
	float ds9;

	BS_Main_Health _Raw_Target_Instance; //A single target which was hit.

	[Tooltip("Should the Markers be active upon the Start of this weapon?")]
	public bool _startActivated = true;
	[Tooltip("An option for more accurate shield detection on Humanoid targets. If in the same frame both a Shield and an Enemy were hit (which may happen at REALY high speeds), ticking this option True will provide additional check to see what was closer to the Attack orign - the shield or the Enemy. It's a good idea to have it turned ON by default, but it's not mandatory. The only situations when this option is not adviced is when your shield wielder is enormously big. Like, a building-big. (though it's not a rule and it may still work fine).")]
	public bool _AdvancedShieldDetection = true;  
	[Tooltip("This object's center is used as the Attack's Origin for Advanced Shield Detection, so it should be placed right in the middle of your Character (it's used to determine from which direction the attacks are coming). [INFO]: Simply create an empty GameObject, and put it somewhere around your weapon and assign it as Attack Origin. Just remember, that it CAN NOT be a child of Marker's Parent!")]
	public Transform _advShieldDetectionOrigin; 
	[Tooltip("If you do not want to assign the Attack Origin via Inspector (because for example, your Attack Origin is supposed to be the Player and you want to instantiate your weapons frequently), then you can remotely assign it by typing it's name in here. It then will be found in hierarchy and assigned automatically. [INFO]: Mind the upper-case letters!")]
	public string _advShieldDetectionSearch; 

	[Space(15)]
	[Tooltip("If we're going to use any Target or Wall hit sounds, put the AudioSource here!")]
	 public AudioSource SoundSource;
	[Range(0,5)]
	[Tooltip("How many Wall Sounds can we randomize from? (these sounds are played upon hitting something which is not a Target or a Shield (so mostly environment hits, like hitting a wall)")]
	public int _numberOfWallHitSounds;
	public AudioClip WallHitSound1;
	public AudioClip WallHitSound2;
	public AudioClip WallHitSound3;
	public AudioClip WallHitSound4;
	public AudioClip WallHitSound5;
	[Range(0,5)]
	[Tooltip("How many Target Sounds can we randomize from? (these sounds are played upon hitting an Object with a Target Tag")]
	public int _numberOfTargetHitSounds;
	public AudioClip TargetHitSound1;
	public AudioClip TargetHitSound2;
	public AudioClip TargetHitSound3;
	public AudioClip TargetHitSound4;
	public AudioClip TargetHitSound5;

	[Range(0,5)]
	[Tooltip("How many Shield Sounds can we randomize from? (these sounds are played upon hitting an Object with a Shield Tag")]
	public int _numberOfShieldHitSounds;
	public AudioClip ShieldHitSound1;
	public AudioClip ShieldHitSound2;
	public AudioClip ShieldHitSound3;
	public AudioClip ShieldHitSound4;
	public AudioClip ShieldHitSound5;

	[Header ("Stagger and Block Features")]
	[Space(15)]
	[Tooltip("If we want to use Stagger Animations (like when you hit a wall or a shield), choose if you want to use Legacy or Mecanim animator. If Mecanim, put your Animator here! [INFO]: When staggered, the Animator will play a state called Stagger, so set your Animation States accordingly!")]
	public Animator _staggerAnimator;
	[Tooltip("If we want to use Stagger Animations (like when you hit a wall or a shield), choose if you want to use Legacy or Mecanim animator. If Legacy, put your Animation component here! [INFO]: When staggered, an animation called Stagger will be played!")]
	public Animation _staggerLegacyAnimation;
	[Tooltip("Should we play a Stagger animation (like attack interruption) upon hitting a wall (object with a different Tag then Target Tag or Shield Tag?")]
	public bool StaggerOnWallHit;
	[Tooltip("Should we play a Stagger animation (like attack interruption) upon hitting an object with a Shield Tag?")]
	public bool StaggerOnShieldHit;
	[Tooltip("Should we spawn some sparks or equivalent object upon hitting a wall or a shield?")]
	public GameObject _wallHitSparks;
	[Tooltip("[ADVANCED] You can also send a message to call a function on another object when we get staggered. If yes, assign the reciever here - a function called Stagger() will be called upon it!")]
	public GameObject _sendStaggerMessage;
	[Tooltip("Should we disable markers automatically upon being staggered on shield hit or wall hit? [WARNING!] When Markers are being disabled, they need to be manualy re-enabled with EnableMarkers() function, through Animation Event, SendMessage or a direct function call")]
	public bool _disableMarkersOnBlock;

	[Space(10)]
	[Tooltip("Should the markers be disabled when this object gets destroyed or disabled? It's good to keep this function on, so there are no hit detection glitches on enabling and disabling weapons")]
	public bool DisableMarkersOnObjectDisable = true;


	private int sRoll; //This is a dummy variable, used in various Random Range rolls (like rolling for sound randomisation).


	
	void Start() 
	{

		if (_AdvancedShieldDetection && _advShieldDetectionOrigin == null) {
			Debug.LogError("I am terribly sorry, but "+gameObject.name+ " has Advanced Shield Detection ON, but it's Origin is not specified. Please specify the Origin or turn off the Advanced Shield Detection!");
		}
		if (_MarkersParent == null) {
			_MarkersParent = transform;
		}
		_markers = new BS_Marker[_MarkersParent.childCount];

		
		for(int i = 0; i < _markers.Length; i++) 
		{
			_markers[i] = _MarkersParent.GetChild(i).gameObject.GetComponent<BS_Marker>();
		}

		if (_startActivated) 
		{
			EnableMarkers();
		}

		if (_AdvancedShieldDetection && _advShieldDetectionSearch != "") {
			_advShieldDetectionOrigin = GameObject.Find(_advShieldDetectionSearch).transform;
		}
	}
	
	
	public void EnableMarkers()
	{
		_markersAreEnabled = true;
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_Shields_Hit.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}
	
	public void DisableMarkers()
	{
		_markersAreEnabled = false;
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_Shields_Hit.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}

	public void OnDisable()
	{
		if (DisableMarkersOnObjectDisable) {
			DisableMarkers();
		}
	}

	public void ClearTargets()
	{
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_Shields_Hit.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			_markers[i2]._hit = null;
			_markers[i2]._target.Clear();
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}

	public void CancelStagger()
	{
		_staggerAnimator.SetBool ("Stagger", false);

	}
	
	
	
	
	
	
	
	
	void Update() 
	{

	
	if (_markersAreEnabled) {
		
			int i; 
			for (i = 0; i < _markers.Length; i++) { //Let's check what each marker hits, shall we?
			
			
			if (_markers [i].HitCheck () != null) {

					//Each target in this marker's Hit Check get's checked
					for(int t = 0; t<_markers[i]._target.Count; t++)
					{

					if (_markers [i]._target[t].tag == _shieldTag && _Targets_Raw_Hit.Contains(_markers [i]._target[t]) == false && _Shields_Hit.Contains(_markers [i]._target[t].transform) == false && _Used_Targets.Contains(_markers [i]._target[t]) == false && _Used_Targets.Contains(_markers [i]._target[t].GetComponent<BS_Shield>()._ParentHealth.transform) == false)
					{
						BS_Shield TheS = _markers [i]._target[t].GetComponent<BS_Shield>();

						if(_AdvancedShieldDetection)
						{
							dh = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldBackSpot.transform.position);
							ds1 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldCenterSpot.transform.position);  //center
							ds2 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot1.transform.position);  //Top
							ds3 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot2.transform.position);  //Top Left
							ds4 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot3.transform.position);  //Top Right
							ds5 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot4.transform.position);  //Bottom
							ds6 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot5.transform.position);  //Bottom Left
							ds7 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot6.transform.position);  //Bottom Right
							ds8 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot7.transform.position);  //Right
							ds9 = Vector3.Distance(_advShieldDetectionOrigin.position, TheS._ShieldEdgeSpot8.transform.position);  //Left
							 

							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldCenterSpot.transform.position - _advShieldDetectionOrigin.position, Color.red, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot7.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot1.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot2.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot3.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot4.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot6.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot5.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
							Debug.DrawRay(_advShieldDetectionOrigin.position,   TheS._ShieldEdgeSpot8.transform.position - _advShieldDetectionOrigin.position, Color.blue, 5);
												
						}
						if(((dh > ds1) || (dh > ds2) || (dh > ds3) || (dh > ds4) || (dh > ds5) || (dh > ds6)||(dh > ds7)||(dh > ds8)||(dh > ds9) ) || (_AdvancedShieldDetection == false))  
						{
		

							_wallHitPositions.Add(_markers[i]._hit[t].point);
							_Shields_Hit.Add(_markers [i]._target[t].transform);
							HitShield = true;
							if(TheS._ParentHealth.transform != null)
							{
								_Used_Targets.Add(TheS._ParentHealth.transform);
							}
						}


				}
				


					if (_markers [i]._target[t].tag == _targetTag && _Targets_Raw_Hit.Contains(_markers [i]._target[t]) == false && _Used_Targets.Contains(_markers [i]._target[t]) == false) 
					{
					
					 
					_Blade_Direction.Add(_markers [i]._tempPos);
					_Blade_Startpoint.Add(_markers [i]._hit[t].point);
					HitFlesh = true;
						if(_markers [i]._target[t].GetComponent<BS_Main_Health>() != null)
						{
							_Raw_Target_Instance = _markers [i]._target[t].GetComponent<BS_Main_Health>();
						}
						if(_markers [i]._target[t].GetComponent<BS_Limb_Hitbox>() != null)
						{
							_Raw_Target_Instance = _markers [i]._target[t].GetComponent<BS_Limb_Hitbox>().MainHealth;
							_Used_Targets.Add(_markers [i]._target[t].transform);
						}
						if(_Raw_Target_Instance != null)
						{
						_Targets_Raw_Hit.Add(_Raw_Target_Instance.transform);
						}
							if(_Raw_Target_Instance != null && _Raw_Target_Instance._shield  != null)
							{
							_Used_Targets.Add(_Raw_Target_Instance._shield.transform);

							}



				}


					if (_markers [i]._target[t].tag != _targetTag && _markers [i]._target[t].tag != _shieldTag && _Used_Targets.Contains(_markers [i]._target[t]) == false) 
				{

						_Used_Targets.Add(_markers [i]._target[t].transform);
					
						HitWall = true;

						_wallHitPositions.Add(_markers [i]._hit[t].point);

				}

				}

			}
			
			if (i > _markers.Length) {
				i = 0;
			}
			
		}
			// dealing damage and staggering
			
			if(HitShield)
			{
				for (int i1 = 0; i1 < _Shields_Hit.Count; i1++) 
				{

					{
						if(!_Used_Targets.Contains(_Shields_Hit[i1])) 
						{	PlayShieldHitSound(); 
							BS_Shield shield = _Shields_Hit[i1].GetComponent<BS_Shield>();
							shield.BlockStagger();
							_Used_Targets.Add(_Shields_Hit[i1]);
						}
					}
					
				}
				
				
			}
			
			

			
			if (HitWall || HitShield) { 
				if(HitWall)
				{
					PlayWallHitSound();
				}
				for (int i3 = 0; i3 < _wallHitPositions.Count; i3++)
				{
					
					if(_wallHitSparks != null){
						_missSparks = Instantiate(_wallHitSparks, _wallHitPositions[i3], Quaternion.identity) as GameObject;
						_missSparks.transform.LookAt(_MarkersParent);
					}
				}
				
				
				if(StaggerOnShieldHit && HitShield)
				{
					Stagger();
				}
				
				if(StaggerOnWallHit && HitWall)
				{
					Stagger ();
				}
			}
			if (HitFlesh) { 
				
				for (int i2 = 0; i2 < _Targets_Raw_Hit.Count; i2++) 
				{
					if( _Targets_Raw_Hit[i2]!= null && _Targets_Raw_Hit[i2].GetComponent<BS_Main_Health> () != null && _Used_Targets.Contains(_Targets_Raw_Hit[i2]) == false)
					{
						 BS_Main_Health TargetRawHealth = _Targets_Raw_Hit[i2].GetComponent<BS_Main_Health> ();
						TargetRawHealth.Bloodflood (_Blade_Direction[i2], _Blade_Startpoint[i2]);
						TargetRawHealth.ApplyDamage (_damage);
						PlayTargetHitSound();
						if(Blood != null)
						{
							GameObject b =  Instantiate(Blood, _Blade_Startpoint[i2], Quaternion.identity) as GameObject;
							b.transform.LookAt(_MarkersParent);
						}
						_Used_Targets.Add(_Targets_Raw_Hit[i2]);
					}
					if( _Targets_Raw_Hit[i2]!= null && _Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> () != null && _Used_Targets.Contains(_Targets_Raw_Hit[i2]) == false)
					{
						BS_Limb_Hitbox TargetRawLimb = _Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> ();
						TargetRawLimb.MainHealth.Bloodflood (_Blade_Direction[i2], _Blade_Startpoint[i2]);
						TargetRawLimb.MainHealth.ApplyDamage (_damage);
						PlayTargetHitSound();
						if(Blood != null)
						{
							GameObject b =  Instantiate(Blood, _Blade_Startpoint[i2], Quaternion.identity) as GameObject;
							b.transform.LookAt(_MarkersParent);
						}
						_Used_Targets.Add(TargetRawLimb.MainHealth.transform);
					}
				}
				
			}
			
			
		}
		
		
		
		_Blade_Direction.Clear();
		_Blade_Startpoint.Clear();
		_Targets_Raw_Hit.Clear();
	
		_wallHitPositions.Clear ();
		
		HitWall = false;
		HitFlesh = false;
		HitShield = false;
		
		if(ContinuousDamage)
		{
			_ContinuousDamage_Timer += Time.deltaTime;
			if(_ContinuousDamage_Timer >= ContinuousDamageInterval)
			{
				ClearTargets();
				_ContinuousDamage_Timer = 0;
			}
		}


	}


		
	public void Stagger()
	{
		if(_staggerAnimator != null)
		{
			_staggerAnimator.Play("Stagger");
		}
		
		if(_staggerLegacyAnimation != null)
		{
			_staggerLegacyAnimation.Play("Stagger");
		}
		
		if(_sendStaggerMessage != null)
		{
			_sendStaggerMessage.SendMessage("Stagger", SendMessageOptions.DontRequireReceiver);
		}
		
		if(_disableMarkersOnBlock)
		{
			DisableMarkers();
		}
	}


	public void SetDamage1()
	{
		_damage = _damageType1;
	}
	public void SetDamage2()
	{
		_damage = _damageType2;
	}
	public void SetDamage3()
	{
		_damage = _damageType3;
	}
	public void SetDamage4()
	{
		_damage = _damageType4;
	}
	public void SetDamage5()
	{
		_damage = _damageType5;
	}
	public void SetDamage6()
	{
		_damage = _damageType6;
	}
	public void SetDamage7()
	{
		_damage = _damageType7;
	}
	public void SetDamage8()
	{
		_damage = _damageType8;
	}
	public void SetDamage9()
	{
		_damage = _damageType9;
	}




	void PlayTargetHitSound()
	{
		if(SoundSource != null)
		{
			if(_numberOfTargetHitSounds >0)
			{
				sRoll = Random.Range (1, _numberOfTargetHitSounds+1);
				
				if(sRoll == 1)
				{
					SoundSource.PlayOneShot(TargetHitSound1);
					
				}
				if(sRoll == 2)
				{
					SoundSource.PlayOneShot(TargetHitSound2);
					
				}
				if(sRoll == 3)
				{
						SoundSource.PlayOneShot(TargetHitSound3);
					
				}
				if(sRoll == 4)
				{
					SoundSource.PlayOneShot(TargetHitSound4);
					
				}
				if(sRoll == 5)
				{
							SoundSource.PlayOneShot(TargetHitSound5);
					
				}
				
			}
		}
		
		
	}

	void PlayShieldHitSound()
	{
		if(SoundSource != null)
		{
			if(_numberOfShieldHitSounds >0)
			{
				sRoll = Random.Range (1, _numberOfShieldHitSounds+1);
				
				if(sRoll == 1)
				{
					SoundSource.PlayOneShot(ShieldHitSound1);
					
				}
				if(sRoll == 2)
				{
								SoundSource.PlayOneShot(ShieldHitSound2);
					
				}
				if(sRoll == 3)
				{
					SoundSource.PlayOneShot(ShieldHitSound3);
					
				}
				if(sRoll == 4)
				{
									SoundSource.PlayOneShot(ShieldHitSound4);
					
				}
				if(sRoll == 5)
				{
					SoundSource.PlayOneShot(ShieldHitSound5);
					
				}
				
			}
		}


	}


	void PlayWallHitSound()
	{
		if (SoundSource != null) {
			if (_numberOfWallHitSounds > 0) {
				sRoll = Random.Range (1, _numberOfWallHitSounds + 1);
			
				if (sRoll == 1) {
					SoundSource.PlayOneShot(WallHitSound1);
					
				}
				if (sRoll == 2) {
													SoundSource.PlayOneShot(WallHitSound2);
					
				}
				if (sRoll == 3) {
													SoundSource.PlayOneShot(WallHitSound3);
					
				}
				if (sRoll == 4) {
													SoundSource.PlayOneShot(WallHitSound4);
					
				}
				if (sRoll == 5) {
													SoundSource.PlayOneShot(WallHitSound5);
					
				}
			
			}
		}
	}
}




