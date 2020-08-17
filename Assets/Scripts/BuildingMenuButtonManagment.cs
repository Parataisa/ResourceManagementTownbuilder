using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingMenuButtonManagment : MonoBehaviour
    {
    public GameObject buttonPrefab;
    public GameObject panel;
    public GameObject[] aktiveButtons;
    public int buttonId;
    private Quaternion quaternion;
    private void Start()
        {
        Invoke("CreateButtons", 0.1f );
        }
    public void CreateButtons()
        {
        buttonId = 0;
        aktiveButtons = new GameObject[BuildingSystem.buildingDirectory.Count];
        for (int i = 0; i < BuildingSystem.buildingDirectory.Count; i++)
            {
            string nameOfTheObject = BuildingSystem.buildingDirectory[i];
            Vector3 localPosition = new Vector3(101.5f * (i + 1)  , this.transform.parent.localPosition.y + 171f);
            Instantiate(buttonPrefab, localPosition , quaternion , panel.transform);
            buttonPrefab.GetComponent<BuildingMenuToggle>().panel = this.panel;
            buttonPrefab.GetComponent<ButtonManagment>().buttonId = buttonId;
            buttonPrefab.GetComponent<ButtonManagment>().objectToBuild = nameOfTheObject;
            buttonPrefab.name = nameOfTheObject;
            aktiveButtons[i] = buttonPrefab;
            buttonId++;
            }
        }
    }
