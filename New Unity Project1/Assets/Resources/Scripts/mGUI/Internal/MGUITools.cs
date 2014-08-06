using UnityEngine;
using System.Collections;

public class MGUITools {

    static public UISystem CreateUI(Transform trans, bool advanced3D, int layer) {
        UISystem uiSys = (trans != null) ? MGUITools.FindInParents<UISystem>(trans.gameObject) : null;
        if (uiSys == null && UISystem.list.Count > 0)
            uiSys = UISystem.list[0];

        if (uiSys == null) {
            GameObject go = MGUITools.AddChild(null, false);
            uiSys = go.AddComponent<UISystem>();

            // Automatically find the layers if none were specified
            if (layer == -1) layer = LayerMask.NameToLayer("UI");
            if (layer == -1) layer = LayerMask.NameToLayer("2D UI");

            go.layer = layer;

            if (advanced3D) {
                go.name = "UISystem (3D)";
            }
            else {
                go.name = "UISystem";
            }

            // Find other active cameras in the scene
            Camera[] cameras = MGUITools.FindActive<Camera>();

            float depth = -1f;
            bool colorCleared = false;
            int mask = (1 << uiSys.gameObject.layer);

            for (int i = 0; i < cameras.Length; ++i) {
                Camera c = cameras[i];

                // If the color is being cleared, we won't need to
                if (c.clearFlags == CameraClearFlags.Color ||
                    c.clearFlags == CameraClearFlags.Skybox)
                    colorCleared = true;

                // Choose the maximum depth
                depth = Mathf.Max(depth, c.depth);

                // Make sure this camera can't see the UI
                c.cullingMask = (c.cullingMask & (~mask));
            }

            Camera cam = MGUITools.AddChild<Camera>(uiSys.gameObject, false);
            cam.clearFlags = colorCleared ? CameraClearFlags.Depth : CameraClearFlags.Color;
            cam.backgroundColor = Color.grey;
            cam.cullingMask = mask;
            cam.depth = depth + 1f;

            if (advanced3D) {
                cam.nearClipPlane = 0.1f;
                cam.farClipPlane = 4f;
                cam.transform.localPosition = new Vector3(0f, 0f, -700f);
            }
            else {
                cam.orthographic = true;
                cam.orthographicSize = 1;
                cam.nearClipPlane = 0.1f;
                cam.farClipPlane = 100;
            }

#if UNITY_EDITOR
            UnityEditor.Selection.activeGameObject = uiSys.gameObject;
#endif
        }


        //Camera cam = MGUITools.AddChild<Camera>(go, false);
        //Canvas canvas = MGUITools.AddChild<Canvas>(go, false);
        return uiSys;
    }

    static public GameObject AddChild(GameObject parent) {
        return AddChild(parent, true);
    }

    static public GameObject AddChild(GameObject parent, bool undo) {
        GameObject go = new GameObject();
#if UNITY_EDITOR
        if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null) {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    static public T AddChild<T>(GameObject parent, bool undo) where T : Component {
        GameObject go = AddChild(parent, undo);
        go.name = GetTypeName<T>();
        return go.AddComponent<T>();
    }

    /// <summary>
    /// Helper function that returns the string name of the type.
    /// </summary>
    static public string GetTypeName<T>() {
        string s = typeof(T).ToString();
        if (s.StartsWith("UI")) s = s.Substring(2);
        else if (s.StartsWith("UnityEngine.")) s = s.Substring(12);
        return s;
    }

    /// <summary>
    /// Finds the specified component on the game object or one of its parents.
    /// </summary>

    static public T FindInParents<T>(GameObject go) where T : Component {
        if (go == null) return null;
#if UNITY_FLASH
		object comp = go.GetComponent<T>();
#else
        T comp = go.GetComponent<T>();
#endif
        if (comp == null) {
            Transform t = go.transform.parent;

            while (t != null && comp == null) {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
        }
#if UNITY_FLASH
		return (T)comp;
#else
        return comp;
#endif
    }

    /// <summary>
    /// Find all active objects of specified type.
    /// </summary>

    static public T[] FindActive<T>() where T : Component {
#if UNITY_3_5 || UNITY_4_0
		return GameObject.FindSceneObjectsOfType(typeof(T)) as T[];
#else
        return GameObject.FindObjectsOfType(typeof(T)) as T[];
#endif
    }
}
