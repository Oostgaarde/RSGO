using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour {
    public string DebugLog;
    public float count = 0;
    public float PreLat, PreLon,lat, lon, deltaLat,deltaLon;
    public float timeInterval;
    float actualTime;
	// Use this for initialization
	void Start () {

        Input.location.Start();
		PreLat = Input.location.lastData.latitude;
		PreLon = Input.location.lastData.longitude;
        timeInterval = 30.0f;
        actualTime = timeInterval;
        UpdateLocation();
    }
	
	// Update is called once per frame
	void Update () {
        actualTime -= Time.deltaTime;
        if(actualTime <= 0)
        {
            UpdateLocation();
			deltaLat = PreLat - Input.location.lastData.latitude * -1f;
			deltaLon = PreLon - Input.location.lastData.longitude * -1f;
			PreLat = Input.location.lastData.latitude;
			PreLon = Input.location.lastData.longitude;
			actualTime = timeInterval;
        }
        

	}

    private void UpdateLocation()
    {
        if (Input.location.isEnabledByUser)
        {

            count++;
            DebugLog = "Lat: " + lat + ", Lon: " + lon + ", Count: " + count;
            GameObject.Find("Cube").GetComponent<GoogleMap>().UpdateMap(lat, lon);
        }
        else DebugLog = "Location not enabled.";
    }
}
