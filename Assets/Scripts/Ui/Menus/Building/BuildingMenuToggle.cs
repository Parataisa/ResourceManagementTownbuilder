using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuToggle : MonoBehaviour
{
    public GameObject panel;
    public GameObject buildingSystem;
    public void PanelToggel()
        {
        if (panel != null)
            {
            bool isActive = panel.activeSelf;
            buildingSystem.GetComponent<BuildingSystem>().UpdateItemsInDictionary();
            panel.GetComponent<BuildingMenuButtonManagment>().UpdateButtons();
            panel.SetActive(!isActive);
            }
        }
    
}
