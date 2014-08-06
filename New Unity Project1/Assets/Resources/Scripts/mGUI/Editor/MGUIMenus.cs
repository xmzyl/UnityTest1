//----------------------------------------------
//            MGUI: Mechanist Games UI kit
//          Copyright ? 2014 Mechanist Games
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;

public class MGUIMenus
{

    #region Menus
    [MenuItem("MGUI/Create/UISystem", false, 1)]
    static void CreateUISystem() {
        UISystem uiSys = MGUITools.CreateUI(null, false, -1);
        Selection.activeGameObject = uiSys.gameObject;
    }

    [MenuItem("MGUI/Create/UISystem", true, 1)]
    static bool Create2Da() { return UISystem.list.Count == 0; }
    #endregion
}
