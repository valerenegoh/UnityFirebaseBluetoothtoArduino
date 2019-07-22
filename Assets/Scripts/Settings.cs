using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour{
    
	public bool showAtStart = true;
    public GameObject overlay;
    
    void Start(){
        if (showAtStart) {
            ShowLaunchScreen();
        }else {
            StartMain();
        }
    }

    public void StartMain(){
        overlay.SetActive (false);
        showAtStart = false;
    }

    public void ShowLaunchScreen(){
        overlay.SetActive (true);
    }
}