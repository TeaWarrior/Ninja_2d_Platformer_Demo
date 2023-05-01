using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalManager : MonoBehaviour
{


    #region Singeltone
    public static AdditionalManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one EqupmentManager");
            return;
        }
        instance = this;
    }
    #endregion

    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
