using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public GameObject QuestPanel;
    public string questText;
    

    #region Singeltone
    public static QuestManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one QuesttManager");
            return;
        }
        instance = this;
    }
    #endregion
    

    public void ShowQuest()
    {

    }
}
