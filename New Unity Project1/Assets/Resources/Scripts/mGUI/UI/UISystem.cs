//----------------------------------------------
//            MGUI: Mechanist Games UI kit
//          Copyright ? 2014 Mechanist Games
//----------------------------------------------
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UISystem, 
/// </summary>
public class UISystem : MonoBehaviour {
    static public List<UISystem> list = new List<UISystem>();

    public T LoadWindowFromPrefab<T>(string _name) where T : UIBase {
        GameObject go = Resources.Load(_name) as GameObject;
        T t = (GameObject.Instantiate(go) as GameObject).AddComponent<T>();
        t.gameObject.transform.parent = gameObject.transform;
        Canvas canvas = t.gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.OverlayCamera;
        canvas.worldCamera = gameObject.GetComponentInChildren<Camera>();
        return t;
    }
}
