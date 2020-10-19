using Assets.Scripts.Buildings;
using UnityEngine;

public class BuildingMenuToggle : MonoBehaviour
    {
    public GameObject panel;
    public BuildingSystem buildingSystem;
    public void PanelToggel()
        {
        if (panel != null)
            {
            bool isActive = panel.activeSelf;
            if (buildingSystem.CurrentPlaceableObject != null)
                {
                buildingSystem.ClearCurser();
                }
            panel.GetComponent<BuildingMenuButtonManagment>().UpdateButtons();
            panel.SetActive(!isActive);
            }
        }

    }
