using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mines : WWWRequest {
    public List<Mining> allMines = new List<Mining>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override IEnumerator WaitForRequest(WWW www)
    {
        return base.WaitForRequest(www);

    }
}
