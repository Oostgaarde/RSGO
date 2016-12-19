using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour {
    public string DebugLog;
    public float count = 0;
    public float lat, lon;
    public float timeInterval;
    float actualTime;
	// Use this for initialization
	void Start () {
        Input.location.Start();
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
            actualTime = timeInterval;
        }
        

	}

    private void UpdateLocation()
    {
        if (Input.location.isEnabledByUser)
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            count++;
            DebugLog = "Lat: " + lat + ", Lon: " + lon + ", Count: " + count;
            GameObject.Find("Cube").GetComponent<GoogleMap>().UpdateMap(lat, lon);
        }
        else DebugLog = "Location not enabled.";
    }
}
