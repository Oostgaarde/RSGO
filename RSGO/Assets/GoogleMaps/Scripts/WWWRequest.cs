using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WWWRequest : MonoBehaviour {

    public string url = "http://192.168.56.1:2403/mining/2d3a14dc10bdbad0"; //"localhost:2403/mining"; //
    // Use this for initialization
    void Start () { 
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public virtual WWW GET(string url_in)
    {
        WWW www = new WWW(url + url_in);
        StartCoroutine(WaitForRequest(www));
     
        return www;
    }

    public virtual WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
        return www;
        
    }

    public virtual WWW PUT(string url, Mining gameObject)
    {
        string json = JsonUtility.ToJson(gameObject);
        byte[] body = Encoding.UTF8.GetBytes(json);
        Dictionary<string, string> headers = new Dictionary<string, string>();

        headers.Add("Content-Type", "application/json");
        headers.Add("X-HTTP-Method-Override", "PUT");

        WWW www = new WWW(url, body, headers);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public virtual IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log("id is: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
