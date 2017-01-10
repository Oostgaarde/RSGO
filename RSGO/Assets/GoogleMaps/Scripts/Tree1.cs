using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : MonoBehaviour {

	public Vector3 PositionOnCube;
	public float longitude,latitude;
	public float longitudeToXFactor, latitudeToYFactor;
	public float timeInterval;
	float actualTime;
	float deltaLong;
	float deltaLat;
	public string DebugLog = "";
	// Use this for initialization
	void Start() {
		timeInterval = 30.0f;
		actualTime = timeInterval;
		longitudeToXFactor = 45.77916132f;
		latitudeToYFactor = 74.73283013f;

		GoogleMap map = GameObject.Find("Cube").GetComponent<GoogleMap>();
		longitude = map.markers [0].locations [0].longitude;
		latitude = map.markers [0].locations [0].latitude;

		this.gameObject.transform.position = new Vector3 (0.6f, 0.017f,-0.492f);
		//Debug.Log("position is: " + this.gameObject.transform.position.x 
	
	}
	
	// Update is called once per frame
	void Update () {
		actualTime -= Time.deltaTime;
		if(actualTime <= 0)
		{
			GoogleMap map = GameObject.Find("Cube").GetComponent<GoogleMap>();

			MainMenuControls main = GameObject.Find ("Cube").GetComponent<MainMenuControls>();
			deltaLong = main.deltaLon;
			deltaLat = main.deltaLat;

			this.gameObject.transform.position = new Vector3 (deltaLong * longitudeToXFactor, latitudeToYFactor,-0.492f);

			actualTime = timeInterval;

			DebugLog = "treeLong: "+ map.markers[0].locations[0].longitude +"treeLat: "+ map.markers[0].locations[0].latitude;
		}
	}
}
