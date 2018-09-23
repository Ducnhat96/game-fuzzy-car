using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	public Color lineColor;
	public Camera cam;
	public float turnSpeed = 5f;
	public NavMeshAgent agent;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	//public WheelCollider wheelRL;
	//public WheelCollider wheelRR;
	public Vector3 centerOfMass;
	[Header("Sensors")]
	public float sensorLenght = 15f;
	public float sensorSideWayLeght = 30f;
	public Vector3 frontSensorPosition = new Vector3(0f, 0.17f, 0.5f);
	public float frontSideSensorPosition = 0.28f;
	public float frontSensorAngle = 15f;
	private bool avoiding = false;
	public TrafficLight trafficLight;
	public float disToBorderLeft = 0;
	public float disToBorderRight = 0;
	public Transform prefab;
	public KeyCode createKey = KeyCode.A;
	void Start(){
		GetComponent<NavMeshAgent> ().speed = 1f;
		GetComponent<Rigidbody> ().centerOfMass = centerOfMass;
		GetComponent<Rigidbody> ().isKinematic = false;
	}
	void FixedUpdate(){
		Sensors ();
		Drive ();
		LerpToSteerAngle ();
		AvoidBorder ();
	}
	// Tìm đường đi ngắn nhất từ điểm đầu đến điểm cuối
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				agent.SetDestination (hit.point);
			}
		}
		if (Input.GetKeyDown(createKey)) {
			CreateObject();
		}
	}
	void CreateObject () {
		Instantiate(prefab,new Vector3(transform.position.x - 40f ,transform.position.y + 0.2f,Random.Range(transform.position.z - 1,transform.position.z +1)),Quaternion.identity);
	}
	private void Sensors(){

		RaycastHit hit;
		Vector3 sensorStartPos = transform.position;
		sensorStartPos += transform.forward * frontSensorPosition.z;
		sensorStartPos += transform.up * frontSensorPosition.y;
		Vector3 sensorTrafficLight;
		sensorTrafficLight = sensorStartPos - 2 * transform.up * frontSensorPosition.y;
		GetComponent<Rigidbody> ().isKinematic = false;

		// Xu ly voi den giao thong
		if (Physics.Raycast (sensorTrafficLight, transform.forward, out hit, sensorLenght)) {
			if (hit.collider.CompareTag ("trafficlight")) {
				float distanceTraffic = hit.distance;
				avoiding = true;
				if(distanceTraffic < 10 ){
					GetComponent<Rigidbody> ().isKinematic = true;
				}
				Debug.DrawLine (sensorTrafficLight, hit.point);
				float statusTraffic = hit.collider.gameObject.GetComponentInChildren<TrafficLight> ().lapTime;
				TrafficLightFSet traff = new TrafficLightFSet (statusTraffic, distanceTraffic);
				float speedCar = traff.speedToTrafficDefuzzer();
				print ("khoảng cách đến đèn " + distanceTraffic +"==> vận tốc " + speedCar);
				GetComponent<NavMeshAgent> ().speed = speedCar;
			}

			else {
				GetComponent<NavMeshAgent> ().speed = 2.5f;
			}
		}


		// Do lech so voi le duong
		// left sideway sensor
		if (Physics.Raycast (transform.position, -transform.right, out hit, sensorSideWayLeght)) {
			if(hit.collider.CompareTag("path") && !hit.collider.CompareTag("light") ){
				Debug.DrawLine (transform.position, hit.point);
				disToBorderLeft = hit.distance;
			}
		}
		//right sideway sensor
		if (Physics.Raycast (transform.position, transform.right, out hit, sensorSideWayLeght)) {
			if(hit.collider.CompareTag("path") && !hit.collider.CompareTag("light") ){
				Debug.DrawLine (transform.position, hit.point);
				disToBorderRight = hit.distance;
			}

		}

		//front center sensor

		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLenght)) {
			if(!hit.collider.CompareTag("path")){
				GetComponent<Rigidbody> ().isKinematic = false;
				Debug.DrawLine (sensorStartPos, hit.point);
				avoiding = true;
				Vector3 targetDir = hit.transform.position - sensorStartPos;
				Vector3 forward = transform.forward;
				float angle = Vector3.SignedAngle (targetDir, forward, Vector3.up);
				float distance = hit.distance;

				AngleRotationFSet famm = new AngleRotationFSet (angle, distance);
				float adjAngle = famm.rotationAngleDefuzzer ();
				float newspeed = famm.speedDefuzzer ();

				print ("Hey guys! góc : " + angle + " và khoảng cách đến chướng ngại vật : " + distance +" ==> bạn cần quay xe góc : " + adjAngle+" và vận tốc xe : "+ newspeed);
				wheelFL.steerAngle = adjAngle ;
				wheelFR.steerAngle = adjAngle;
				//wheelRL.steerAngle = adjAngle;
				//wheelRR.steerAngle = adjAngle;
				GetComponent<NavMeshAgent> ().speed = newspeed;
				Debug.Log (avoiding);
			}

		}
		else {
			//GetComponent<Rigidbody> ().isKinematic = true;
		}
		//front right sensor
		sensorStartPos += transform.right * frontSideSensorPosition;
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLenght)) {
			if(!hit.collider.CompareTag("path")){
				GetComponent<Rigidbody> ().isKinematic = false;
				Debug.DrawLine (sensorStartPos, hit.point);
				avoiding = true;

				Vector3 targetDir = hit.transform.position - sensorStartPos;
				Vector3 forward = transform.forward;
				float angle = Vector3.SignedAngle (targetDir, forward, Vector3.up);
				float distance = hit.distance;

				AngleRotationFSet famm = new AngleRotationFSet (angle, distance);
				float adjAngle = famm.rotationAngleDefuzzer ();
				float newspeed = famm.speedDefuzzer ();

				print ("Hey guys! góc sensor phải 1: " + angle + " và khoảng cách đến chướng ngại vật : " + distance +" ==> bạn cần quay xe góc : " + adjAngle+" và vận tốc xe : "+ newspeed);
				wheelFL.steerAngle = adjAngle;
				wheelFR.steerAngle = adjAngle;
				//wheelRL.steerAngle = adjAngle;
				//wheelRR.steerAngle = adjAngle;
				GetComponent<NavMeshAgent> ().speed = newspeed;
				Debug.Log (avoiding);
			}

		}

		//front right angle sensor

		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(frontSensorAngle,transform.up)*transform.forward, out hit, sensorLenght)) {
			if (!hit.collider.CompareTag ("path")) {
				GetComponent<Rigidbody> ().isKinematic = false;
				Debug.DrawLine (sensorStartPos, hit.point);
				avoiding = true;


				Vector3 targetDir = hit.transform.position - sensorStartPos;
				Vector3 forward = transform.forward;
				float angle = Vector3.SignedAngle (targetDir, forward, Vector3.up);
				float distance = hit.distance;

				AngleRotationFSet famm = new AngleRotationFSet (angle, distance);
				float adjAngle = famm.rotationAngleDefuzzer ();
				float newspeed = famm.speedDefuzzer ();

				print ("Hey guys! góc phải: " + angle + " và khoảng cách đến chướng ngại vật : " + distance +" ==> bạn cần quay xe góc : " + adjAngle+" và vận tốc xe : "+ newspeed);
				wheelFL.steerAngle = adjAngle ;
				wheelFR.steerAngle = adjAngle;
				//wheelRL.steerAngle = adjAngle;
				//wheelRR.steerAngle = adjAngle;
				GetComponent<NavMeshAgent> ().speed = newspeed;
				Debug.Log (avoiding);
			}

		}
		else {
			//GetComponent<Rigidbody> ().isKinematic = true;
		}

		//front left sensor
		sensorStartPos -= 2*transform.right *frontSideSensorPosition;
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLenght)) {
			if(!hit.collider.CompareTag("path")){
				GetComponent<Rigidbody> ().isKinematic = false;
				avoiding = true;
				Debug.DrawLine (sensorStartPos, hit.point);

				Vector3 targetDir = hit.transform.position - sensorStartPos;
				Vector3 forward = transform.forward;
				float angle = Vector3.SignedAngle (targetDir, forward, Vector3.up);
				float distance = hit.distance;

				AngleRotationFSet famm = new AngleRotationFSet (angle, distance);
				float adjAngle = famm.rotationAngleDefuzzer ();
				float newspeed = famm.speedDefuzzer ();

				print ("Hey guys! góc sensor trái 1: " + angle + " và khoảng cách đến chướng ngại vật : " + distance +" ==> bạn cần quay xe góc : " + adjAngle+" và vận tốc xe : "+ newspeed);

				wheelFL.steerAngle = adjAngle;
				wheelFR.steerAngle = adjAngle;
				//wheelRL.steerAngle = adjAngle;
				//wheelRR.steerAngle = adjAngle;
				GetComponent<NavMeshAgent> ().speed = newspeed;
				Debug.Log (avoiding);
			}
		}

		//front left angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle,transform.up)*transform.forward, out hit, sensorLenght)) {
			if(!hit.collider.CompareTag("path")){
				GetComponent<Rigidbody> ().isKinematic = false;
				Debug.DrawLine (sensorStartPos, hit.point);
				avoiding = true;

				Vector3 targetDir = hit.transform.position - sensorStartPos;
				Vector3 forward = transform.forward;
				float angle = Vector3.SignedAngle (targetDir, forward, Vector3.up);
				float distance = hit.distance;

				AngleRotationFSet famm = new AngleRotationFSet (angle, distance);
				float adjAngle = famm.rotationAngleDefuzzer ();
				float newspeed = famm.speedDefuzzer ();

				print ("Hey guys! góc trái: " + angle + " và khoảng cách đến chướng ngại vật : " + distance +" ==> bạn cần quay xe góc : " + adjAngle+" và vận tốc xe : "+ newspeed);

				wheelFL.steerAngle = adjAngle;
				wheelFR.steerAngle = adjAngle;
				//wheelRL.steerAngle = adjAngle;
				//wheelRR.steerAngle = adjAngle;
				GetComponent<NavMeshAgent> ().speed = newspeed;
				Debug.Log (avoiding);
			}

		}
		else {
			//GetComponent<Rigidbody> ().isKinematic = true;
		}
	}
	private void Drive(){
		wheelFL.motorTorque = 50f;
		wheelFR.motorTorque = 50f;
	}
	private void LerpToSteerAngle ()
	{
		wheelFL.steerAngle = Mathf.Lerp (wheelFL.steerAngle, frontSensorAngle, Time.deltaTime * turnSpeed);
		wheelFR.steerAngle = Mathf.Lerp (wheelFR.steerAngle, frontSensorAngle, Time.deltaTime * turnSpeed);
	}
	private void AvoidBorder(){
		if(!avoiding){
			if(wheelFL.steerAngle  < 0) {
				wheelFL.steerAngle = 40;
				wheelFR.steerAngle = 40;
				//wheelRL.steerAngle = 40;
				//wheelRL.steerAngle = 40;
			}
			else if(wheelFL.steerAngle  > 0) {
				wheelFL.steerAngle = -40;
				wheelFR.steerAngle = -40;
				//wheelRL.steerAngle = -40;
				//wheelRL.steerAngle = -40;
			}
		}
		if (disToBorderLeft > 2 * disToBorderRight) {
			wheelFL.steerAngle = -30 + Random.Range(0,1);
			wheelFR.steerAngle = -30 + Random.Range(0,1);
			//wheelRL.steerAngle = -30 + Random.Range(0,1);
			//wheelRR.steerAngle = -30 + Random.Range(0,1);
			Debug.Log (wheelFR.steerAngle);
		}
		if (disToBorderRight > 2 * disToBorderLeft) {
			wheelFL.steerAngle = 30 + Random.Range(0,1);
			wheelFR.steerAngle = 30+ Random.Range(0,1);
			//wheelRL.steerAngle = 30 + Random.Range(0,1);
			//wheelRR.steerAngle = 30 + Random.Range(0,1);
			Debug.Log (wheelFR.steerAngle);
		}
	}
}
