using UnityEngine;
using System.Collections;

public class UIBase : MonoBehaviour {

    protected string name_;
    public string name {
        get { return name_; }
        set { name_ = value; }
    }
    public UIBase() {
        OnInit();
    }

    public virtual void OnInit() {

    }

    public virtual void OnOpen() {

    }

    public virtual void OnClose() {

    }

    public void Close() {
        OnClose();
    }

    public void Show() {
        OnOpen();
    }

    public void BringToTop() {

    }
}
