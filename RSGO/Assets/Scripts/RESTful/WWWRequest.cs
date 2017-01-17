using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WWWRequest : MonoBehaviour {

    public string host = "http://192.168.178.14:2403"; 
    // Use this for initialization

    public virtual WWW GET(string path)
    {
        WWW www = new WWW(host + path);
        StartCoroutine(WaitForRequest(www));
     
        return www;
    }

    public virtual WWW POST(string path, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(host + path, form);
        StartCoroutine(WaitForRequest(www));
        return www;
        
    }

    public virtual WWW PUT(string path, string json)
    {
        //string json = JsonUtility.ToJson(gameObject);
        byte[] body = Encoding.UTF8.GetBytes(json);
        Dictionary<string, string> headers = new Dictionary<string, string>();

        headers.Add("Content-Type", "application/json");
        headers.Add("X-HTTP-Method-Override", "PUT");

        WWW www = new WWW(host + path, body, headers);
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
