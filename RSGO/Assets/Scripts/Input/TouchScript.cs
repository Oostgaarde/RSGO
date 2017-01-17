using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
    public GUIStyle guiStyle = new GUIStyle();
	void Update ()
    {   

	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100), "Touch"))
        {
            Camera.current.backgroundColor = Color.red;
        }

        foreach (Touch touch in Input.touches)
        {
            guiStyle.fontSize = 60;
            string message = "";
            message += "ID: " + touch.fingerId + "\n";
            message += "Phase: " + touch.phase.ToString() + "\n";
            message += "TapCount: " + touch.tapCount + "\n";
            message += "Pos X: " + touch.position.x + "\n";
            message += "Pos Y: " + touch.position.y + "\n";
            
            int num = touch.fingerId;
            GUI.Label(new Rect(0 + 520 * num, 0, 480, 400), message, guiStyle); 
            
        }
    }
}
