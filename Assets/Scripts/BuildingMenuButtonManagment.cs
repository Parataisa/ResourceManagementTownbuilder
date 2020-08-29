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
    private void Start()
        {
        Invoke("CreateButtons", 0.1f);
        }
    public void CreateButtons()
        {
        buttonId = 0;
        int numberOfButtons = BuildingSystem.buildingDirectory.Count;
        int rows = numberOfButtons / 6;
        int numberOfButtonsInLastRow = numberOfButtons % rows;
        int currentRow = 0;
        aktiveButtons = new GameObject[BuildingSystem.buildingDirectory.Count];
        for (int y = 0; y <= rows; y++)
            {
            for (int i = 0; i <= 5; i++)
                {
                string nameOfTheObject = BuildingSystem.buildingDirectory[i];
                buttonPrefab.name = nameOfTheObject;
                var newButton = Instantiate(buttonPrefab, panel.transform);
                Vector3 localVector3 = new Vector3(120f + 220f * i, (1000 - 120) - 220f * currentRow, 0);
                newButton.GetComponent<RectTransform>().localPosition = localVector3;
                newButton.GetComponent<BuildingMenuToggle>().panel = this.panel;
                newButton.GetComponent<ButtonManagment>().buttonId = buttonId;
                newButton.GetComponent<ButtonManagment>().objectToBuild = nameOfTheObject;
                aktiveButtons[i] = newButton;
                buttonId++;
                if (currentRow == rows)
                    {
                    i =-numberOfButtonsInLastRow + 6;
                    }
                }
                if (buttonId % 6 == 0)
                    {
                    currentRow++;
                    }

            }
        }
    }