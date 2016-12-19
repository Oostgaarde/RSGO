using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour {
    Text text;
    MainMenuControls map;
	// Use this for initialization
	void Start () {
        map = GameObject.Find("Cube").GetComponent<MainMenuControls>();
        text = gameObject.GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Lat: " + map.lat + ", Lon: " + map.lon;
    }
}
