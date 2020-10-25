using Assets.Scripts.Buildings;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

public class BuildingMenuToggle : MonoBehaviour
    {
    public GameObject panel;
    public BuildingSystem buildingSystem;
    private UserInterfaceManagment generalUserInterfaceManagment;
    private void Start()
        {
        generalUserInterfaceManagment = FindObjectOfType<UserInterfaceManagment>();
        }
    public void PanelToggel()
        {
        if (panel != null)
            {
            bool isActive = panel.activeSelf;
            generalUserInterfaceManagment.CloseOnClickUi(null);
            if (buildingSystem.CurrentPlaceableObject != null)
                {
                buildingSystem.ClearCurser();
                }
            panel.GetComponent<BuildingMenuButtonManagment>().UpdateButtons();
            panel.SetActive(!isActive);
            }
        }
    }
