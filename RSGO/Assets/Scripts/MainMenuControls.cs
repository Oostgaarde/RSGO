using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        actualTime = timeInterval;
        StartCoroutine(UpdateLocation());
    }
	
	// Update is called once per frame
	void Update () {
        actualTime -= Time.deltaTime;
        if (actualTime <= 0)
        {
            StartCoroutine(UpdateLocation());
            actualTime = timeInterval;
        }
    }

    private IEnumerator UpdateLocation()
    {

        if (!Input.location.isEnabledByUser)
        {
            GameObject.Find("Cube").GetComponent<GoogleMap>().UpdateMap(51.915998f, 4.549559f);
            yield break;
        }
        Input.location.Start();
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            DebugLog = "Location not enabled.";
            yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            count++;
            DebugLog = "Lat: " + lat + ", Lon: " + lon + ", Count: " + count;
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            GameObject.Find("Cube").GetComponent<GoogleMap>().UpdateMap(lat, lon);
        }
      
    }
}
