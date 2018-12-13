using UnityEngine;
using System.Collections;

public class BS_Main_Health : MonoBehaviour {





	[Tooltip("How much health should this object have? When it reaches 0, Death functions will be triggered on")]
	public int _health;
	[Space(10)]
	[Tooltip("If we're using animations when the object get's hit, determine if it should be Mecanim or Legacy. If Mecanim, put your Animator here! [INFO]: On being hit, this Animator will play one of the Hurt states.")] 
	public Animator _hurtAnimator;
	[Tooltip("If we're using animations when the object get's hit, determine if it should be Mecanim or Legacy. If Legacy, put your animation component here! [INFO]: On being hit, Object will play an animation named Hurt1, Hurt2, Hurt3, Hurt4 or Hurt5, accordingly to the below variable.")] 
	public Animation _hurtLegacyAnimation;
	[Tooltip("How many Hurt Animations are there to randomize from? In case of Mecanim, states Hurt1, Hurt2, Hurt3, Hurt4 or Hurt5 will be played upon being hit. If it's Legacy, animations of the same name will be randomly played. [INFO]: Mind the upper case letters in names!")] 
	[Range(0,5)]
	public int _numberOfHurtAnimations;
	[Tooltip("You can set a prefab to be instantiated upon being hit - like blood or flying robot parts! Simply put your Blood prefab here. The same function can be called from the BS_Marker_Manager if you wish.")]
	public GameObject Blood;

	[Space(10)]
	[Header("Sound")]
	[Tooltip("If we're using any sounds, put your AudioSource here!")]
	public AudioSource SoundSource;
	[Range(0,5)]
	[Tooltip("How many Hurt Sounds Can We randomise from?")]
	public int _numberOfHurtSounds;
	public AudioClip HurtSound1;
	public AudioClip HurtSound2;
	public AudioClip HurtSound3;
	public AudioClip HurtSound4;
	public AudioClip HurtSound5;
	[Space(5)]
	[Range(0,5)]
	[Tooltip("How many Death sounds can we randomise from?")]
	public int _numberOfDeathSounds;
	public AudioClip DeathSound1;
	public AudioClip DeathSound2;
	public AudioClip DeathSound3;
	public AudioClip DeathSound4;
	public AudioClip DeathSound5;


		
	[Space (10)]
	[Header("Death Feautres")]
	[Tooltip("Should this Object be destroyed upon death?")]
	public bool _destroyOnDeath = false;
	[Tooltip("If the above is true, after how many seconds should it be destroyed?")]
	public float _destroyDelay;
	[Tooltip("Should something be instantiated on Death (like an explosion of special blood splat)? Put your prefab here if yes")]
	public GameObject _SpawnOnDeath;
	[Space(10)]
	[Tooltip("Should some components be destroyed on Death?")]
	public Component _destroyComponent1;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyComp1Delay;
	[Tooltip("Should some components be destroyed on Death?")]
	public Component _destroyComponent2;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyComp2Delay;
	[Tooltip("Should some components be destroyed on Death?")]
	public Component _destroyComponent3;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyComp3Delay;
	[Tooltip("Should some components be destroyed on Death?")]
	public Component _destroyComponent4;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyComp4Delay;
	[Tooltip("Should some components be destroyed on Death?")]
	public Component _destroyComponent5;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyComp5Delay;
	[Space(10)]
	[Tooltip("Should some GameObjects be destroyed on Death?")]
	public GameObject _destroyGameObject1;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyObj1Delay;
	[Tooltip("Should some GameObjects be destroyed on Death?")]
	public GameObject _destroyGameObject2;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyObj2Delay;
	[Tooltip("Should some GameObjects be destroyed on Death?")]
	public GameObject _destroyGameObject3;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyObj3Delay;
	[Tooltip("Should some GameObjects be destroyed on Death?")]
	public GameObject _destroyGameObject4;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyObj4Delay;
	[Tooltip("Should some GameObjects be destroyed on Death?")]
	public GameObject _destroyGameObject5;
	[Tooltip("If the above is not empty, after how many seconds should it be destroyed?")]
	public float _destroyObj5Delay;

	[Space(10)]
	[Tooltip("[ADVANCED]: Upon Death, this Object can SendMessage to another gameobject, calling Death() function")]
	public GameObject _sendDeathMessage;

	[Space(10)]
	[Tooltip("You can randomise up to 5 different Death Animations to play from upon death. Do we have any?")]
	[Range(0,5)]
	public int _numberOfDeathAnimations;
	[Tooltip("If we're using Mecanim, set the Death animation Animator here. It will randomise to play a state named Death1, Death2, Death3, Death4 or Death5 to True.")]
	public Animator _deathAnimator;
	[Tooltip("If we're using Legacy Animation, set the animation component here. Upon Death it will randomise to play animation named Death1, Death2, Death3, Death4 or Death5")]
	public Animation _deathLegacyAnimation;

	[Space(10)]
	[Tooltip("If you plan on using the BS_Shield with this charcter or object, please, assign it's reference here. Otherwise the hit detection may fail in some cases.")]
	public BS_Shield _shield;  //it's referenced in other scripts
	

	GameObject _Blood_Instance;
	int _HurtAnim_Randomisation;
	int _DeathAnim_Randomisation;
	int _HurtSound_Randomisation;



	public void Bloodflood(Vector3 _prevMarkerPos, Vector3 _hitPos)   //Instantiate blood in the direction of the marker which hit this object.
	{
		if (Blood != null && _health >0) 
		{
			_Blood_Instance =  Instantiate(Blood, _hitPos, transform.rotation) as GameObject;
			_Blood_Instance.transform.LookAt(2* _Blood_Instance.transform.position - _prevMarkerPos);

		}

	}



	public void ApplyDamage(int _dmg)   //Let's apply some damage on hit, shall we?
	{
		_health -= _dmg;

		if (_health < 0) 
		{
			_health = 0;
		}

		if (_health > 0) 
		{
			if(_numberOfHurtAnimations != 0)
			{
			_HurtAnim_Randomisation = Random.Range(1,_numberOfHurtAnimations+1);   //Hurt animations
			}
			if(_numberOfHurtAnimations == 0)
			{
				_HurtAnim_Randomisation = 0;
			}

			if(_hurtAnimator != null)
			{
				if(_HurtAnim_Randomisation == 1)
				{
					_hurtAnimator.Play("Hurt1");
				}
				if(_HurtAnim_Randomisation == 2)
				{
					_hurtAnimator.Play("Hurt2");
				}
				if(_HurtAnim_Randomisation == 3)
				{
					_hurtAnimator.Play("Hurt3");
				}
				if(_HurtAnim_Randomisation == 4)
				{
					_hurtAnimator.Play("Hurt4");
				}
				if(_HurtAnim_Randomisation == 5)
				{
					_hurtAnimator.Play("Hurt5");
				}
			}
			if(_hurtLegacyAnimation != null)
			{
				if(_HurtAnim_Randomisation == 1)
				{
					_hurtLegacyAnimation.Play ("Hurt1");
				}
				if(_HurtAnim_Randomisation == 2)
				{
					_hurtLegacyAnimation.Play ("Hurt2");
				}
				if(_HurtAnim_Randomisation == 3)
				{
					_hurtLegacyAnimation.Play ("Hurt3");
				}
				if(_HurtAnim_Randomisation == 4)
				{
					_hurtLegacyAnimation.Play ("Hurt4");
				}
				if(_HurtAnim_Randomisation == 5)
				{
					_hurtLegacyAnimation.Play ("Hurt5");
				}
			}

			if(SoundSource != null)
			{
				if(_numberOfHurtSounds >0)
				{
					_HurtSound_Randomisation = Random.Range (1, _numberOfHurtSounds+1);
					
						if(_HurtSound_Randomisation == 1)
						{
							SoundSource.clip = HurtSound1;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 2)
						{
							SoundSource.clip = HurtSound2;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 3)
						{
							SoundSource.clip = HurtSound3;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 4)
						{
							SoundSource.clip = HurtSound4;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 5)
						{
							SoundSource.clip = HurtSound5;
							SoundSource.Play();
						}

				}
			}
		
		}

		if (_health <= 0) //DEATH
		{


			if( _SpawnOnDeath != null)
			{
				Instantiate( _SpawnOnDeath, transform.position, transform.rotation);
			}


			//components
			if(_destroyComponent1 != null)
			{
				Destroy(_destroyComponent1, _destroyComp1Delay);
			}
			if(_destroyComponent2 != null)
			{
				Destroy(_destroyComponent2, _destroyComp2Delay);
			}
			if(_destroyComponent3 != null)
			{
				Destroy(_destroyComponent3, _destroyComp3Delay);
			}
			if(_destroyComponent4 != null)
			{
				Destroy(_destroyComponent4, _destroyComp4Delay);
			}
			if(_destroyComponent5 != null)
			{
				Destroy(_destroyComponent5, _destroyComp5Delay);
			}


			//Objects
			if(_destroyGameObject1 != null)
			{
				Destroy(_destroyGameObject1, _destroyObj1Delay);
			}
			if(_destroyGameObject2 != null)
			{
				Destroy(_destroyGameObject2, _destroyObj2Delay);
			}
			if(_destroyGameObject3 != null)
			{
				Destroy(_destroyGameObject3, _destroyObj3Delay);
			}
			if(_destroyGameObject4 != null)
			{
				Destroy(_destroyGameObject4, _destroyObj4Delay);
			}
			if(_destroyGameObject5 != null)
			{
				Destroy(_destroyGameObject5, _destroyObj5Delay);
			}

			//This one is about sending a message...
			if(_sendDeathMessage != null)
			{
				_sendDeathMessage.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
			}

			// animation

			if(_numberOfDeathAnimations != 0)
			{
				_DeathAnim_Randomisation = Random.Range(1,_numberOfDeathAnimations+1);   //Hurt animations
			}
			if(_numberOfDeathAnimations == 0)
			{
				_DeathAnim_Randomisation = 0;
			}

			if(_deathAnimator != null)
			{
				if(_DeathAnim_Randomisation == 1)
				{
				_deathAnimator.Play("Death1");
				}
				if(_DeathAnim_Randomisation == 2)
				{
					_deathAnimator.Play("Death2");
				}
				if(_DeathAnim_Randomisation == 3)
				{
					_deathAnimator.Play("Death3");
				}
				if(_DeathAnim_Randomisation == 4)
				{
					_deathAnimator.Play("Death4");
				}
				if(_DeathAnim_Randomisation == 5)
				{
					_deathAnimator.Play("Death5");
				}
			}



			if(_deathLegacyAnimation != null)
			{
				if(_DeathAnim_Randomisation == 1)
				{
					_deathLegacyAnimation.Play ("Death1");
				}
				if(_DeathAnim_Randomisation == 2)
				{
					_deathLegacyAnimation.Play ("Death2");
				}
				if(_DeathAnim_Randomisation == 3)
				{
					_deathLegacyAnimation.Play ("Death3");
				}
				if(_DeathAnim_Randomisation == 4)
				{
					_deathLegacyAnimation.Play ("Death4");
				}
				if(_DeathAnim_Randomisation == 5)
				{
					_deathLegacyAnimation.Play ("Death5");
				}
			}

			if(SoundSource != null)
			{
				if(_numberOfDeathSounds >0)
				{
					_HurtSound_Randomisation = Random.Range (1, _numberOfDeathSounds+1);
					
						if(_HurtSound_Randomisation == 1)
						{
							SoundSource.clip = DeathSound1;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 2)
						{
							SoundSource.clip = DeathSound2;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 3)
						{
							SoundSource.clip = DeathSound3;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 4)
						{
							SoundSource.clip = DeathSound4;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 5)
						{
							SoundSource.clip = DeathSound5;
							SoundSource.Play();
						}

				}
			}

			if(_destroyOnDeath)
			{
				Destroy(gameObject, _destroyDelay);
			}
		}
	}
}