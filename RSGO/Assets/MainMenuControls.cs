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
    public List<Mining> tempMines = new List<Mining>();
    private WWWRequest www = new WWWRequest();
    List<Mining> mines = new List<Mining>();
	// Use this for initialization
	void Start () {
        timeInterval = 30.0f;
        actualTime = timeInterval;
        StartCoroutine(UpdateLocation());
        //Mining[] mines = JsonUtility.FromJson<Mining[]>(www.get);

    }
	
	// Update is called once per frame
	void Update () {
        actualTime -= Time.deltaTime;
        if (actualTime <= 0)
        {
            StartCoroutine(UpdateLocation());
            actualTime = timeInterval;
            Debug.Log("Update main control");
            //tempMines.Clear();
            www.GET("Mining");

         
            foreach (Mining mine in tempMines)
            {
                Debug.Log("Mine id : " + mine.id);
            }
        }
    }

    private IEnumerator UpdateLocation()
    {
        if (!Input.location.isEnabledByUser) yield break;
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
