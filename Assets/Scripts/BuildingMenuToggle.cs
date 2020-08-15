using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuToggle : MonoBehaviour
{
    public GameObject panel;
    public void PanelToggel()
        {
        if (panel != null)
            {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
            }
        }
}
