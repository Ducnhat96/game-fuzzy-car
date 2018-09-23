using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightFSet : MonoBehaviour {
	float w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12, w13, w14, w15;

	//Distance trapezoidal number functions
	public float distNear(float distance) 
	{
		LeftTrapezoid dNear = new LeftTrapezoid(5,10);

		return dNear.getOutPut(distance);
	}

	public float distMedium(float distance)
	{
		Trapezoid dMedium = new Trapezoid(5, 8, 12, 15);

		return dMedium.getOutPut(distance);
	}

	public float distFar(float distance)
	{
		RightTrapezoid dLarge = new RightTrapezoid(10, 15);

		return dLarge.getOutPut(distance);
	}

	//Status trapezoidal number functions
	public float Green(float time)
	{
		LeftTrapezoid green = new LeftTrapezoid(2,6);

		return green.getOutPut(time);
	}

	public float veryGreen (float time)
	{
		Trapezoid veryGreen = new Trapezoid (4, 7, 9, 12);
		return veryGreen.getOutPut (time);
	}

	public float Yellow (float time)
	{
		Trapezoid yellow = new Trapezoid (8, 11, 13, 16);
		return yellow.getOutPut (time);
	}

	public float Red (float time)
	{
		Trapezoid red = new Trapezoid (12, 15, 17, 20);
		return red.getOutPut (time);
	}

	public float veryRed(float time)
	{
		RightTrapezoid veryRed = new RightTrapezoid(18, 22);

		return veryRed.getOutPut(time);
	}

	// Giải mờ vận tốc
	public float speedToTrafficDefuzzer()
	{
		float temp;
		temp = (w1 * 0f) + (w2 * 1.5f) + (w3 * 2.5f) + 
			(w4 * 0f) + (w5 * 1.5f) + (w6 * 2.5f) + 
			(w7 * 0.5f) + (w8 * 1.5f) + (w9 * 2.5f) +
			(w10 * 2.5f )+ (w11 * 2.5f) + (w12 * 2.5f) +
			(w13 * 2.5f) + (w14 * 2.2f) + (w15 * 2f);

		if ((w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15 ) == 0)
		{
			return 0;
		}
		else
		{
			return temp / (w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15);
		}
	}
	public TrafficLightFSet(float trafficLightStatus, float distanceToTrafficLight)
	{
		w1 = Mathf.Min(veryRed(trafficLightStatus) ,distNear(distanceToTrafficLight));
		w2 = Mathf.Min(veryRed(trafficLightStatus) , distMedium(distanceToTrafficLight));
		w3 = Mathf.Min(veryRed(trafficLightStatus) , distFar(distanceToTrafficLight));
		w4 = Mathf.Min(Red(trafficLightStatus) , distNear(distanceToTrafficLight));
		w5 = Mathf.Min(Red(trafficLightStatus) , distMedium(distanceToTrafficLight));
		w6 = Mathf.Min(Red(trafficLightStatus) , distFar(distanceToTrafficLight));
		w7 = Mathf.Min(Yellow(trafficLightStatus) , distNear(distanceToTrafficLight));
		w8 = Mathf.Min(Yellow(trafficLightStatus) , distMedium(distanceToTrafficLight));
		w9 = Mathf.Min(Yellow(trafficLightStatus) ,distFar(distanceToTrafficLight));
		w10 = Mathf.Min(Green(trafficLightStatus) ,distNear(distanceToTrafficLight));
		w11 = Mathf.Min(Green(trafficLightStatus) , distMedium(distanceToTrafficLight));
		w12 = Mathf.Min(Green(trafficLightStatus) , distFar(distanceToTrafficLight));
		w13 = Mathf.Min(veryGreen(trafficLightStatus) , distNear(distanceToTrafficLight));
		w14 = Mathf.Min(veryGreen(trafficLightStatus) , distMedium(distanceToTrafficLight));
		w15 = Mathf.Min(veryGreen(trafficLightStatus) , distFar(distanceToTrafficLight));
	}

}
