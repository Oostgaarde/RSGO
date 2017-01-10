using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugEditor : MonoBehaviour
{
    Text text;
    MainMenuControls map;
    GoogleMap google;
	Tree1 tree;
    // Use this for initialization
    void Start()
    {
        map = GameObject.Find("Cube").GetComponent<MainMenuControls>();
        google = GameObject.Find("Cube").GetComponent<GoogleMap>();
		tree = GameObject.Find ("Tree1").GetComponent<Tree1> ();
        text = gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
		text.text = map.DebugLog + google.DebugLog + tree.DebugLog;
    }
}
