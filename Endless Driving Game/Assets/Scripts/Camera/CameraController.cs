using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject firstPersonCamera; 
    public GameObject overheadCamera; 
    public int inputID; 

    void ShowOverheadView() {
        firstPersonCamera.SetActive(false); 
        overheadCamera.SetActive(true); 
    }

    void ShowFirstPersonView() {
        firstPersonCamera.SetActive(true); 
        overheadCamera.SetActive(false); 
    }

    void Update() {
        if (firstPersonCamera.activeInHierarchy) {
            if (Input.GetButtonDown("Switch" + inputID)) 
            {
                ShowOverheadView();
            }
        }
        else {
            if (Input.GetButtonDown("Switch" + inputID))
            {
                ShowFirstPersonView();
            }
        }
   
    }
}
