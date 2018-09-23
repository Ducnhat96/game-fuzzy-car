using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Trapezoid{
	float a, b, c, d;
	public Trapezoid(float pointA, float pointB, float pointC, float pointD){
		a = pointA;
		b = pointB;
		c = pointC;
		d = pointD;
	}
	public float getOutPut( float input){
		float res = 0;
		float left_Slope = 1 / (b - a);
		float right_Slope = 1 / (c - d);
		if(input <= a || input >= d){
			res = 0;
		}
		if(input >= b && input <= c){
			res = 1;
		}
		if(input >= a && input <= b){
			res = (input - a) * left_Slope;
		}
		if(input >= c && input <= d){
			res = (input - d) * right_Slope;
		}

		return res;
	}

}
