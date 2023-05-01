using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MenuScript : MonoBehaviour
{
  public void Button_Start_Play()
    {
        EditorSceneManager.LoadScene(1);
    }
    public void Button_Exit()
    {
        Application.Quit();
    }
}
