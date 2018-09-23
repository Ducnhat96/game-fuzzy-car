using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RightTrapezoid {

	float a, b;
	public RightTrapezoid(float pointA, float pointB){
		a = pointA;
		b = pointB;
	}
	public float getOutPut( float input){
		float res = 0;
		float left_Slope = 1 / (b - a);
		if(input <= a){
			res = 0;
		}
		if(input >= b){
			res = 1;
		}
		if(input > a && input < b){
			res = (input - a) * left_Slope;
		}
		return res;
	}
}
