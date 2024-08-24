using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour {    
    public void OnTryAgainButtonClick() {
        SceneManager.LoadScene(0);
    }
}