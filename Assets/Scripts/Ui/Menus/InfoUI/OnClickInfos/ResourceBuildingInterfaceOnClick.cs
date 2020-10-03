using Assets.Scripts.Buildings.ResourceBuildings;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        private List<string> DropdownOptions = new List<string>();
        private TMP_Dropdown ResourcesDropdown;
        private void Awake()
            {
            generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
            generalUi.OnClickInfoPanelToggled += GetGameObject;
            ResourcesDropdown = this.transform.Find("ListOfResourcesDropdown").GetComponent<TMP_Dropdown>();
            }
        private void Start()
            {
            savedeGameObject = selectedGameobject;
            generalUi.CurrentOnClickGameObject = selectedGameobject;
            ResourcesDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    return;
                    }
                if (selectedGameobject != savedeGameObject)
                    {
                    savedeGameObject = selectedGameobject;
                    generalUi.CurrentOnClickGameObject = selectedGameobject;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    }
                if (selectedGameobject != savedeGameObject || DropdownOptions.Count.Equals(0))
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    DropdownOptions.Clear();
                    int i = 0;
                    int x = 0;
                    foreach (var child in selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea)
                        {
                        if (selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea[x] == null)
                            {
                            ResourcesDropdown.ClearOptions();
                            DropdownOptions.Clear();
                            x++;
                            continue;
                            }
                        x++;
                        i++;
                        DropdownOptions.Add(i + " " + GetObjectName(child.name));
                        }
                    ResourcesDropdown.ClearOptions();
                    ResourcesDropdown.AddOptions(DropdownOptions);
                    savedeGameObject = selectedGameobject;
                    }
                if (selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().selecedResource != ResourcesDropdown.value)
                    {
                    ResourcesDropdown.value = selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().selecedResource;
                    }
                }
            }
        void DropdownValueChanged()
            {
            selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().SetSelecedResource(ResourcesDropdown.value);
            }

        private string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        }
    }
