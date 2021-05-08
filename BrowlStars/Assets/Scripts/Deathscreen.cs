using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Deathscreen : MonoBehaviour
{
   
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Main Scene");
        Time.timeScale = 1;
       
    }
}
