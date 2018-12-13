using UnityEngine;
using System.Collections;

public class BS_Weapon_Animation_Events : MonoBehaviour {

	[Tooltip("Choose which Marker Manager should be used with this Animation Support")]
	public BS_Marker_Manager MarkerManager;
	[Tooltip("We can also turn on and off the colliders of the BS_Shield. If you wish to use it, assign it here!")]
	public BS_Shield Shield;
	[Tooltip("You can use various Animation Event Calls to tweak your Damage Dealing mechanisms in your attack Animations, as well as enabling and disabling shields. The Events are: DisableMarkers(), EnableMarkers(),  ClearTargets(), CancelStagger(), SetDamage<1-9>(), Stagger(), DisableShieldCollider(), EnableShieldCollider(), SetWeapon<1-9>MarkerManager() . Read about them in the Read Me File to find out what each of them does!    [It's an Info bool. It does nothing, it just stores the Tooltip]")]
	public bool InfoAndAnimationEvents;

	[Space(15)]
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon1MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon2MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon3MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon4MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon5MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon6MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon7MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon8MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon9MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon10MarkerManger;
	[Tooltip("Should the Markers of the current Marker Manager be automatically Disabled upon switching to another Marker Manager? (It's a good thing to leade it True by default). ")]
	public bool DisableMarkersOnManagerSwitch = true;


	public void DisableShieldCollider()
	{
		Shield.DisableShieldCollider ();
	}

	public void EnableShieldCollider()
	{
		Shield.EnableShieldCollider ();
	}

	public void ClearTargets()
	{
		MarkerManager.ClearTargets ();
	}

	public void DisableMarkers ()
	{
		MarkerManager.DisableMarkers ();
	}
	public void  EnableMarkers()
	{
		MarkerManager.EnableMarkers ();
	}
	public void Stagger ()
	{
		MarkerManager.Stagger ();
	}
	public void  CancelStagger()
	{
		MarkerManager.CancelStagger();
	}




	public void  SetDamage1 ()
	{
		MarkerManager.SetDamage1 ();
	}
	public void  SetDamage2 ()
	{
		MarkerManager.SetDamage2 ();
	}
	public void  SetDamage3 ()
	{
		MarkerManager.SetDamage3 ();
	}
	public void  SetDamage4 ()
	{
		MarkerManager.SetDamage4 ();
	}
	public void  SetDamage5 ()
	{
		MarkerManager.SetDamage5 ();
	}
	public void  SetDamage6 ()
	{
		MarkerManager.SetDamage6 ();
	}
	public void  SetDamage7 ()
	{
		MarkerManager.SetDamage7 ();
	}
	public void  SetDamage8 ()
	{
		MarkerManager.SetDamage8 ();
	}
	public void  SetDamage9 ()
	{
		MarkerManager.SetDamage9 ();
	}



	public void  SetWeapon1MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
			MarkerManager = Weapon1MarkerManger;
	}
	public void  SetWeapon2MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon2MarkerManger;
	}
	public void  SetWeapon3MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon3MarkerManger;
	}
	public void  SetWeapon4MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon4MarkerManger;
	}
	public void  SetWeapon5MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon5MarkerManger;
	}
	public void  SetWeapon6MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon6MarkerManger;
	}
	public void  SetWeapon7MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon7MarkerManger;
	}
	public void  SetWeapon8MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon8MarkerManger;
	}
	public void  SetWeapon9MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon9MarkerManger;
	}
	public void  SetWeapon10MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon10MarkerManger;
	}




}
