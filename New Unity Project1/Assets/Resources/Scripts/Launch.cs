using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (loginUI_ == null)
            loginUI_ = Resources.Load("UI/LoginUI") as Canvas;
	}

    public Canvas loginUI_ = null;
}
