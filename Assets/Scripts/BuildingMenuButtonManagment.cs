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
    private int NumberOfButtonsPerRow = 6;
    private void Start()
        {
        Invoke("CreateButtons", 0.1f);
        }
    public void CreateButtons()
        {
        buttonId = 0;
        int numberOfButtons = BuildingSystem.buildingDirectory.Count;
        int rows = GetNumberOfRows(numberOfButtons);
        int numberOfButtonsInLastRow = GetNumberOfButtonsLastRow(rows, NumberOfButtonsPerRow, numberOfButtons);
        int currentRow = 1;
        aktiveButtons = new GameObject[BuildingSystem.buildingDirectory.Count];
        for (int y = 1; y <= rows; y++)
            {
            for (int i = 0; i < 6; i++)
                {
                if (i == numberOfButtonsInLastRow && currentRow == rows)
                    {
                    break;
                    }
                string nameOfTheObject = BuildingSystem.buildingDirectory[buttonId];
                buttonPrefab.name = nameOfTheObject;
                var newButton = Instantiate(buttonPrefab, panel.transform);
                Vector3 localVector3 = new Vector3(120f + 220f * i, (1000 - 120) - 220f * (currentRow - 1), 0);
                newButton.GetComponent<RectTransform>().localPosition = localVector3;
                newButton.GetComponent<BuildingMenuToggle>().panel = this.panel;
                newButton.GetComponent<ButtonManagment>().buttonId = buttonId;
                newButton.GetComponent<ButtonManagment>().objectToBuild = nameOfTheObject;
                aktiveButtons[buttonId] = newButton;
                buttonId++;
                if (buttonId % 6 == 0)
                    {
                    currentRow++;
                    }
                }

            }
        }


    private int GetNumberOfRows(int NumberOfButtons)
        {
        if (NumberOfButtons / NumberOfButtonsPerRow == 0)
            {
            return 1;
            }
        else if (NumberOfButtons % NumberOfButtonsPerRow != 0)
            {
            return NumberOfButtons / NumberOfButtonsPerRow + 1;
            }
        else
            {
            return NumberOfButtons / NumberOfButtonsPerRow;
            }
        }
    private int GetNumberOfButtonsLastRow(int rows, int NumberOfButtonsPerRow, int numberOfButtons)
        {
        if (rows == 1)
            {
            return numberOfButtons;
            }
        else if (numberOfButtons % NumberOfButtonsPerRow != 0)
            {
            return numberOfButtons - NumberOfButtonsPerRow * (rows - 1);
            }
        else
            {
            return NumberOfButtonsPerRow;
            }
        }
    }
