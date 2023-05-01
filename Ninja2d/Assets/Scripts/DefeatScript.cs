using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScript : MonoBehaviour
{
   public void Restart_Button()
    {
        Scene scene = SceneManager.GetActiveScene();
        int sceneIndex = scene.buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
