using Assets.Scripts.Buildings.ResourceBuildings;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceBuildingInterfaceOnClick : MonoBehaviour
        {
        public GameObject selectedGameobject;
        internal GameObject savedeGameObject;
        internal string ObjectName = "";
        private List<string> DropdownOptions = new List<string>();
        private TMP_Dropdown Dropdown;
        private void Awake()
            {
            FindObjectOfType<GeneralUserInterfaceManagment>().OnClickInfoPanelToggled += GetGameObject;
            Dropdown = this.transform.Find("ListOfChildrenDropdown").GetComponent<TMP_Dropdown>();
            }
        private void Start()
            {
            savedeGameObject = selectedGameobject;
            Dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    return;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    }
                if (selectedGameobject != savedeGameObject || DropdownOptions.Count.Equals(0))
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    DropdownOptions.Clear();
                    int i = 0;
                    int x = 0;
                    foreach (var child in selectedGameobject.transform.GetChild(0).GetComponent<ResourceBuildingBase>().GatherableResouceInArea)
                        {
                        if (selectedGameobject.transform.GetChild(0).GetComponent<ResourceBuildingBase>().GatherableResouceInArea[x] == null)
                            {
                            Dropdown.ClearOptions();
                            DropdownOptions.Clear();
                            x++;
                            continue;
                            }
                        x++;
                        i++;
                        DropdownOptions.Add(i + " " + GetObjectName(child.name));
                        }
                    Dropdown.ClearOptions();
                    Dropdown.AddOptions(DropdownOptions);
                    savedeGameObject = selectedGameobject;
                    }
                if (selectedGameobject.transform.GetChild(0).GetComponent<ResourceBuildingBase>().selecedResource != Dropdown.value)
                    {
                    Dropdown.value = selectedGameobject.transform.GetChild(0).GetComponent<ResourceBuildingBase>().selecedResource;
                    }
                }
            }
        void DropdownValueChanged()
            {
            selectedGameobject.transform.GetChild(0).GetComponent<ResourceBuildingBase>().SetSelecedResource(Dropdown.value); 
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
