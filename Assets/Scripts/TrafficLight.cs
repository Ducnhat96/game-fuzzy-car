using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight: MonoBehaviour {
	public GameObject LightDL;
	public GameObject Reddl;
	public GameObject Yellowdl;
	public GameObject Greendl;
	public Light Red;
	public Light Yellow;
	public Light Green;

	public float GreencoolDown = 10f;
	public float GreenTimer = 0;
	public float RedcoolDown = 10f;
	public float RedTimer = 0;
	public float YellowcoolDown = 4f;
	public float YellowTimer = 0;

	public int index = 0 ;
	public float lapTime = 0;

	void Awake()
	{
		Red = Reddl.GetComponentInChildren<Light> ();
		Yellow = Yellowdl.GetComponentInChildren<Light>();
		Green = Greendl.GetComponentInChildren<Light> ();
	}
	void FixedUpdate()
	{
			lapTime += Time.deltaTime;
			if(lapTime >= 24f) {
				lapTime = 0;
			}
			if (index == 0) {
			    RedTimer = 0;
				green ();
			}
		    
			if (index == 1) {
			    GreenTimer = 0;
				yellow ();
			}
			if (index == 2) {
			    YellowTimer = 0;
				red ();
			}
		    
	}
	 void green()
	{
		
		if (GreenTimer > 0){
			GreenTimer -= Time.deltaTime;
	
		}
		if (GreenTimer < 0){
			index = 1;
			GreenTimer = 0;

		}
		if (GreenTimer == 0)
		{
			//Debug.Log ("Đèn xanh đang chạy");
			Red.enabled = false;
			Yellow.enabled = false;
			Green.enabled = true; 
			GreenTimer = GreencoolDown;

		}
	}
	void yellow()
	{
		
		if (YellowTimer > 0){
			YellowTimer -= Time.deltaTime;

		}
		if (YellowTimer < 0){
			index = 2;
			YellowTimer = 0;
		}
		if (YellowTimer == 0)
		{
			//Debug.Log ("Đèn vàng đang chạy");
			Green.enabled = false;  
			Red.enabled = false;
			Yellow.enabled = true;
			YellowTimer = YellowcoolDown;

		}

	}
	void red()
	{
		if (RedTimer > 0){
			RedTimer -= Time.deltaTime;

		}
		if (RedTimer < 0){
			index = 0;
			RedTimer = 0;

		}
		if (RedTimer == 0)
		{
			//Debug.Log ("Đèn đỏ đang chạy");
			Yellow.enabled = false;
			Green.enabled = false;  
			Red.enabled = true;
			RedTimer = RedcoolDown;
		}
	}
		
	}

