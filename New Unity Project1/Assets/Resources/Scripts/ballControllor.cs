using UnityEngine;
using System.Collections;

public class ballControllor : MonoBehaviour {

    GameObject sun1;
    GameObject sun2;

    bool flagUp = true;
    float initAngle = 180;

    float angleSpeed = 4;
    float eulerAngle = 0;
    void Awake() {
    }
	// Use this for initialization
	void Start () {
        sun1 = GameObject.FindGameObjectWithTag("sun");
        sun2 = GameObject.FindGameObjectWithTag("sun2");



        transform.parent = sun1.transform;
	}

    void FixedUpdate() {
        if (flagUp) {
            sun1.transform.Rotate(new Vector3(0, 0, 1), angleSpeed);
            eulerAngle += angleSpeed;
			if ((initAngle + eulerAngle) >= 360) {
                transform.parent = sun2.transform;
                flagUp = false;
                eulerAngle = 0;
                initAngle = 0;
                sun1.transform.Rotate(new Vector3(0, 0, 0));
                return;
            }
        }


        if (!flagUp) {
            sun2.transform.Rotate(new Vector3(0, 0, -1), angleSpeed);
            eulerAngle += angleSpeed;
			if (eulerAngle >= 360) {
                transform.parent = sun1.transform;
                flagUp = true;
                eulerAngle = 0;
                sun1.transform.Rotate(new Vector3(0, 0, 0));
                return;
            }
        }
    }
	
	void Update () {

	}
}
