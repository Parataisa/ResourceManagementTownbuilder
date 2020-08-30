using System;
using System.ComponentModel;
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
        UpdateButtons();
        }

    public void CreateButtons()
        {
        int buttonWidth = (int)buttonPrefab.GetComponent<RectTransform>().rect.width;
        int buttonHeight = (int)buttonPrefab.GetComponent<RectTransform>().rect.height;
        int panelWidth = (int)panel.GetComponent<RectTransform>().rect.width;
        int panelHeight = (int)panel.GetComponent<RectTransform>().rect.height;
        int NumberOfButtonsPerRow = (int)panelWidth / (buttonWidth + 20);
        // ToDo: Implement some sort of scrolling when the max number of rows is reached.
        int NumberOfMaxRows = (int)panelHeight / (buttonHeight + 20);

        buttonId = 0;
        int numberOfButtons = BuildingSystem.buildingDirectory.Count;
        int rows = GetNumberOfRows(numberOfButtons, NumberOfButtonsPerRow);
        int numberOfButtonsInLastRow = GetNumberOfButtonsLastRow(rows, NumberOfButtonsPerRow, numberOfButtons);
        int currentRow = 1;
        aktiveButtons = new GameObject[BuildingSystem.buildingDirectory.Count];
        for (int y = 1; y <= rows; y++)
            {
            for (int i = 0; i < NumberOfButtonsPerRow; i++)
                {
                if (i == numberOfButtonsInLastRow && currentRow == rows)
                    {
                    break;
                    }
                string nameOfTheObject = BuildingSystem.buildingDirectory[buttonId];
                buttonPrefab.name = nameOfTheObject;
                var newButton = Instantiate(buttonPrefab, panel.transform);
                Vector3 localVector3 = new Vector3((buttonWidth / 2 + 20) + (buttonWidth + 20) * i, (1000 - (buttonHeight/2) - 20) - (buttonHeight + 20) * (currentRow - 1), 0);
                newButton.GetComponentInChildren<Text>().text = nameOfTheObject;
                newButton.GetComponent<RectTransform>().localPosition = localVector3;
                newButton.GetComponent<BuildingMenuToggle>().panel = panel;
                newButton.GetComponent<ButtonManagment>().buttonId = buttonId;
                newButton.GetComponent<ButtonManagment>().objectToBuild = nameOfTheObject;
                aktiveButtons[buttonId] = newButton;
                buttonId++;
                if (buttonId % NumberOfButtonsPerRow == 0)
                    {
                    currentRow++;
                    }
                }

            }
        }
    public void UpdateButtons()
        {
        if (panel.transform.childCount == 0)
            {
            Invoke("CreateButtons", 0.1f);
            }
        else
            {
            foreach (Transform child in panel.transform)
                {
                Destroy(child.gameObject);
                }
            Invoke("CreateButtons", 0.1f);
            }
        }

    private int GetNumberOfRows(int NumberOfButtons, int NumberOfButtonsPerRow)
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
