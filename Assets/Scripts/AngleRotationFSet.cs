using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AngleRotationFSet {
	float w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12, w13, w14, w15;

	//Direction trapezoidal number functions
	public float angleVeryNegative (float theta)
	{
		Trapezoid aVeryNegative = new Trapezoid (-90, -70, -50, -30);
		return aVeryNegative.getOutPut (theta);
	}

	public float angleNegative (float theta)
	{
		Trapezoid aNegative = new Trapezoid (-50, -40, -20, -10);
		return aNegative.getOutPut (theta);
	}

	public float angleZeZo(float theta)
	{
		Trapezoid aZeZo = new Trapezoid (-20, -10, 10, 20);
		return aZeZo.getOutPut (theta);
	}

	public float anglePositive (float theta)
	{
		Trapezoid aPositive = new Trapezoid (10, 20, 40, 50);
		return aPositive.getOutPut (theta);
	}

	public float angleVeryPositive (float theta)
	{
		Trapezoid aVeryPositive = new Trapezoid (30, 50, 70, 90);
		return aVeryPositive.getOutPut (theta);
	}

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
	// Giải mờ góc quay
	public float rotationAngleDefuzzer()
	{
		float temp;
		temp = (w1 * -40f) + (w2 * -35f) + (w3 * -30f)+
			(w4 * (-50f)) + (w5 * (-49f)) + (w6 * (-48f)) + 
			(w7 * 60f) + (w8 * 55f) + (w9 * 50f) +
			(w10 * (50f)) + (w11 * (49f))+ (w12 * (48f)) +
			(w13 * (40f)) + (w14 * (35f))+ (w15 * (30f));

		if ((w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15) == 0)
		{
			return 0;
		}
		else
		{
			return temp / (w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15);
		}
	}
	// Giải mờ vận tốc
	public float speedDefuzzer()
	{
		float temp;
		temp = (w1 *1.5f) + (w2 * 2f) + (w3 * 3f) +
			(w4 * 2.3f) + (w5 * 2.7f) + (w6 * 3f) + 
			(w7 * 1.2f) + (w8 * 2.5f) + (w9 * 2.7f) +
			(w10 * 2.3f )+ (w11 * 2.7f) + (w12 * 3f) +
			(w13 * 1.5f) + (w14 * 2f) + (w15 * 3f);

		if ((w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15 ) == 0)
		{
			return 2f;
		}
		else
		{
			return temp / (w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10 + w11 + w12 + w13 + w14 + w15);
		}
	}
	public AngleRotationFSet(float obsAngle, float obsDistance)
	{
		w1 = Mathf.Min(angleVeryNegative(obsAngle) ,distNear(obsDistance));
		w2 = Mathf.Min(angleVeryNegative(obsAngle) , distMedium(obsDistance));
		w3 = Mathf.Min(angleVeryNegative(obsAngle) , distFar(obsDistance));
		w4 = Mathf.Min(angleNegative(obsAngle) , distNear(obsDistance));
		w5 = Mathf.Min(angleNegative(obsAngle) , distMedium(obsDistance));
		w6 = Mathf.Min(angleNegative(obsAngle) , distFar(obsDistance));
		w7 = Mathf.Min(angleZeZo(obsAngle) , distNear(obsDistance));
		w8 = Mathf.Min(angleZeZo(obsAngle) , distMedium(obsDistance));
		w9 = Mathf.Min(angleZeZo(obsAngle) ,distFar(obsDistance));
		w10 = Mathf.Min(anglePositive(obsAngle) ,distNear(obsDistance));
		w11 = Mathf.Min(anglePositive(obsAngle) , distMedium(obsDistance));
		w12 = Mathf.Min(angleVeryPositive(obsAngle) , distFar(obsDistance));
		w13 = Mathf.Min(angleVeryPositive(obsAngle) , distNear(obsDistance));
		w14 = Mathf.Min(angleVeryPositive(obsAngle) , distMedium(obsDistance));
		w15 = Mathf.Min(angleVeryPositive(obsAngle) , distFar(obsDistance));
	}
}
