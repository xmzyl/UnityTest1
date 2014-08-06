using UnityEngine;
using System.Collections.Generic;

public class Launch : MonoBehaviour {

	// Use this for initialization
    private List<UIBase> uiList = new List<UIBase>();
    
    private UISystem uiSystem;
	void Start () {
        //创建UISystem
        uiSystem = MGUITools.CreateUI(null, false, -1);
        //创建登录界面
        uiSystem.LoadWindowFromPrefab<LoginWindow>("UI/LoginUI");
	}
	
	// Update is called once per frame
	void Update () {

	}
}
