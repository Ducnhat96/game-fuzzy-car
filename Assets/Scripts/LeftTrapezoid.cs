using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LeftTrapezoid {

	float a, b;
	public LeftTrapezoid(float pointA, float pointB){
		a = pointA;
		b = pointB;
	}
	public float getOutPut( float input){
		float res = 0;
		float right_Slope = 1 / (a - b);
		if(input < a){
			res = 1;
		}
		if(input >= b){
			res = 0;
		}
		if(input > a && input < b){
			res = (input - b) * right_Slope;
		}
		return res;
	}
}
