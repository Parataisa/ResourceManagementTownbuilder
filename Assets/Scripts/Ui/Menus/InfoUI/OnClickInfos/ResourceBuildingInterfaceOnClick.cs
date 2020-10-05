using Assets.Scripts.Buildings.ResourceBuildings;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        private List<string> DropdownOptions = new List<string>();
        private TMP_Dropdown ResourcesDropdown;
        public GameObject ScrollViewContent;
        public GameObject ItemImagePrefab;
        private List<string> keyList = new List<string>();
        private List<string> storedResourceList = new List<string>();
        private void Awake()
            {
            generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
            generalUi.OnClickInfoPanelToggled += GetGameObject;
            ResourcesDropdown = this.transform.Find("ListOfResourcesDropdown").GetComponent<TMP_Dropdown>();
            }
        private void OnDisable()
            {
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            }

        private void AddingItems()
            {
            keyList.Clear();
            keyList.AddRange(selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources.Keys);
            storedResourceList.Clear();
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            foreach (var Resource in keyList)
                {
                if (selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource] != 0)
                    {
                    storedResourceList.Add(Resource);
                    continue;
                    }
                }
            foreach (var storedResource in storedResourceList)
                {
                var Item = Instantiate(ItemImagePrefab, ScrollViewContent.transform);
                Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(storedResource);
                Item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[storedResource].ToString());
                Item.name = storedResource;
                }
            }

        private void Start()
            {
            savedeGameObject = selectedGameobject;
            generalUi.CurrentOnClickGameObject = selectedGameobject;
            AddingItems();
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
                foreach (var item in selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources)
                    {
                    if (item.Value != 0)
                        {
                        if (storedResourceList.Contains(item.Key))
                            {
                            continue;
                            }
                        else
                            {
                            storedResourceList.Add(item.Key);
                            }
                        }
                    }
                if (storedResourceList.Count != ScrollViewContent.transform.childCount || selectedGameobject != savedeGameObject)
                    {
                    AddingItems();
                    }
                foreach (var Resource in storedResourceList)
                    {
                    if (selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource] != Convert.ToInt32(ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text))
                        {
                        ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource].ToString());
                        }
                    }
                UpdateUiBuildingInformationen();
                }
            }



        private void UpdateUiBuildingInformationen()
            {
            if (selectedGameobject != savedeGameObject || DropdownOptions.Count.Equals(0))
                {
                ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                DropdownOptions.Clear();
                DropDownMenuHandler();
                }
            if (ObjectName.Length == 0)
                {
                ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                }
            if (selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().selecedResource != ResourcesDropdown.value)
                {
                ResourcesDropdown.value = selectedGameobject.transform.parent.transform.GetChild(0).GetComponent<ResourceBuildingAccountant>().selecedResource;
                }
            }

        private void DropDownMenuHandler()
            {
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
            generalUi.CurrentOnClickGameObject = selectedGameobject;
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
