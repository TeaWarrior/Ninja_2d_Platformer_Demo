using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Controll : MonoBehaviour
{
    
    [SerializeField] GameObject _defeatPanel;
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _inventoryPanel;
    [SerializeField] GameObject _equipmentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void DefeatPanelActivate(object sender, EventArgs e)
    {
        StartCoroutine(DeatthPanelCorotine());
    }

    IEnumerator DeatthPanelCorotine()
    {
        yield return new WaitForSeconds(1f);
        _defeatPanel.SetActive(true);
     
    }
  public   void Start_To_Tap_Button()
    {
        _startPanel.SetActive(false);
      
    }

    public void Close_Inventory_Button()
    {
        _inventoryPanel.SetActive(false);
    }
    public void Open_Inventory_Button()
    {
        _inventoryPanel.SetActive(true);
        _equipmentPanel.SetActive(true);
    }
   
    
}
