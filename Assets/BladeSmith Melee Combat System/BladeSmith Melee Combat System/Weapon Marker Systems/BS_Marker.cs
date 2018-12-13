//The Marker Script
//All markers must be a child of Marker Menager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BS_Marker : MonoBehaviour 
{
	[HideInInspector]
	public List<Transform> _target = new List<Transform>();  //thing that was hit
	[HideInInspector]
	public Vector3 _tempPos; //Temporary position of the marker from the last frame
	[HideInInspector]
	public float _dist; //distance between temp and actual marker position
	[HideInInspector]
	public Vector3 _dir; //Direction of the above.
	[HideInInspector]
	public RaycastHit[] _hit; //What was hit in this frame?
	[Tooltip ("Choose which Layers should be affected by this marker's hit check.")]
	public LayerMask _layers;
	[Tooltip("Should the Debug Rays (white traces after marker) be shown in the editor view? Turning this off might give you additional 1 or 2 FPS if you're using hundreds of markers at once")]
	public bool DebugRays = true;
 

	
	void Start()
	{
		_tempPos = transform.position;
		if(GetComponent<Renderer>() != null)
		{
			GetComponent<Renderer>().enabled = false;
		}
	}
	
	public List<Transform> HitCheck() 
	{ 

		_target.Clear();
		_hit = null;
		_dir =  transform.position - _tempPos;				
		_dist = Vector3.Distance(transform.position, _tempPos);
		

		if (DebugRays) {
			////////// DEBUG RAYS
			Debug.DrawRay (_tempPos, _dir, Color.white, 0.3f);
			//////////
		}

		_hit = Physics.RaycastAll (_tempPos, _dir, _dist, _layers);

			for(int i = 0; i<_hit.Length; i++)
			{
			_target.Add (_hit[i].collider.transform);
			_tempPos = transform.position;
			}


			if(_target.Count != 0)
				{
			return _target;
				}
				else
				{
					_tempPos = transform.position;
					return null;
				}



		
	}

	
	
}