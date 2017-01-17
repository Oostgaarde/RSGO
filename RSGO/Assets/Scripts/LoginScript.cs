using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoginScript : WWWRequest {

    public InputField username;
    public InputField password;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        string usernameText = username.text;
        string passwordText = password.text;
        Dictionary<string, string> login = new Dictionary<string, string>();
        login.Add("username", usernameText);
        login.Add("password", passwordText);

        POST("/users/login", login);
    }

    public override IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            SceneManager.LoadScene("Demo",LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
