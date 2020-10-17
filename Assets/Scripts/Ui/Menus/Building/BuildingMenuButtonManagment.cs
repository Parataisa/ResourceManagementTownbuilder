using Assets.Scripts.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuButtonManagment : MonoBehaviour
    {
    public GameObject buttonPrefab;
    public GameObject panel;
    public BuildingSystem buildingSystem;
    public GameObject[] aktiveButtons;

    private int buttonId;

    public void CreateButtons()
        {
        Vector2 buttonDimension = GetButtonDimension(buttonPrefab);
        int panelWidth = (int)panel.GetComponent<RectTransform>().rect.width;
        int panelHeight = (int)panel.GetComponent<RectTransform>().rect.height;
        int NumberOfButtonsPerRow = (int)panelWidth / ((int)buttonDimension.x + 20);
        // ToDo: Implement some sort of scrolling when the max number of rows is reached.
        int NumberOfMaxRows = (int)panelHeight / ((int)buttonDimension.y + 20);
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
                var newButton = Instantiate(buttonPrefab, panel.transform) as GameObject;
                Vector3 localVector3 = new Vector3((buttonDimension.x / 2 + 20) + (buttonDimension.x + 20) * i, (1000 - (buttonDimension.y / 2) - 20) - (buttonDimension.y + 20) * (currentRow - 1), 0);
                AddingButtonParameter(nameOfTheObject, newButton, localVector3);
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
            Invoke(nameof(CreateButtons), 0.1f);
            }
        else if (panel.transform.childCount == aktiveButtons.Length)
            {
            return;
            }
           else
            { 
            int i = 0;
            foreach (Transform child in panel.transform)
                {
                Destroy(child.gameObject);
                Destroy(aktiveButtons[i].gameObject);
                i++;
                }
            Invoke(nameof(CreateButtons), 0.1f);
            }
        }
    private void AddingButtonParameter(string nameOfTheObject, GameObject newButton, Vector3 localVector3)
        {
        newButton.GetComponent<RectTransform>().localPosition = localVector3;
        SetButtonBuildingNameAndTyp(newButton, nameOfTheObject);
        newButton.GetComponent<BuildingMenuToggle>().panel = panel;
        newButton.GetComponent<BuildingMenuToggle>().buildingSystem = buildingSystem;
        newButton.GetComponent<BuildingButton>().buttonId = buttonId;
        newButton.GetComponent<BuildingButton>().objectToBuild = nameOfTheObject;
        newButton.GetComponent<BuildingButton>().buildingPrefab = buildingSystem.placeableObjectPrefabs[buttonId];
        newButton.GetComponent<Button>().onClick.AddListener(delegate { buildingSystem.OnButtonClick(newButton.GetComponent<BuildingButton>().buttonId); });
        }

    private void SetButtonBuildingNameAndTyp(GameObject newButton, string nameOfTheObject)
        {
        string[] splitName = nameOfTheObject.Split('-');
        newButton.transform.Find("BuildingType").GetComponent<TextMeshProUGUI>().SetText(splitName[0]);
        newButton.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().SetText(splitName[1]);
        }

    private Vector2 GetButtonDimension(GameObject buttonPrefab)
        {
        Vector2 vector2 = new Vector2
            {
            x = (int)buttonPrefab.GetComponent<RectTransform>().rect.width,
            y = (int)buttonPrefab.GetComponent<RectTransform>().rect.height
            };
        return vector2;
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
