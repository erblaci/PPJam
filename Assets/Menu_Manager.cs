using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Menu_Manager : MonoBehaviour
{
    public bool isVideoStarted = false;
    public GameObject videoScreen;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        
    }

    public void PlayIntro()
    {
       // videoScreen.SetActive(true);
        StartCoroutine("WaitForVideo");
       
    }

    public IEnumerator waitForScreen()
    {
        yield return new WaitForSeconds(5f);
        
    }
   
}
