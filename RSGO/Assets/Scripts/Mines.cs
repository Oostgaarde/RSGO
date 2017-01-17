using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mines : WWWRequest {
    public Mine[] allMines;
	// Use this for initialization
	void Start () {
        GET("/mining");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override IEnumerator WaitForRequest(WWW www)
    {
        yield return base.WaitForRequest(www);
        string text = www.text;
        text = "{\"Items\":" + text + "}";
        allMines = JsonHelper.FromJson<Mine>(text);
    }
}
