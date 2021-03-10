using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string loadScene;
    public void ToTitle()
    {
        SceneManager.LoadScene(loadScene); 
    }
}
