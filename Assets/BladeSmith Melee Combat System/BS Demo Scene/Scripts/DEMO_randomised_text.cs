using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DEMO_randomised_text : MonoBehaviour {

	public Text text;
	public string one;
	public string two;
	public string three;
	public string four;
	public float t;
	int i;
	// Use this for initialization



	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if(t > 15)
		{
			i +=1;
			t = 0;
			if(i == 4)
			{
				i =0;
			}
		}

		if(i==0)
		  {
			text.text = one;

		}
		if(i==1)
		{
			text.text = two;
		}
		if(i==2)
		{
			text.text = three; 
		}if(i==3)
		{
			text.text = four; 
		}

	}
}
