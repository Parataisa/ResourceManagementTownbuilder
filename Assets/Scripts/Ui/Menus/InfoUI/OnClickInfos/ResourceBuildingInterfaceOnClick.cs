using Assets.Scripts.Buildings.BuildingSystemHelper;
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
        public event Action OnClickInfoPanelTextUpdate;
        internal override int Layer => LayerClass.ResourceBuildings;

        protected override void Start()
            {
            base.Start();
            AddingItems();
            ResourcesDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
            }
        protected override void OnEnable()
            {
            base.OnEnable();
            ResourcesDropdown = this.transform.Find("ListOfResourcesDropdown").GetComponent<TMP_Dropdown>();
            }
        private void OnDisable()
            {
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            SavedGameObject = null;
            SelectedGameobject = null;
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (SelectedGameobject == null)
                    {
                    return;
                    }
                foreach (var item in SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources)
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
                if (storedResourceList.Count != ScrollViewContent.transform.childCount || SelectedGameobject != SavedGameObject)
                    {
                    AddingItems();
                    }
                foreach (var Resource in storedResourceList)
                    {
                    if (SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource] != Convert.ToInt32(ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text))
                        {
                        ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource].ToString());
                        }
                    }
                UpdateUiBuildingInformationen();
                }
            }
        private void AddingItems()
            {
            keyList.Clear();
            keyList.AddRange(SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources.Keys);
            storedResourceList.Clear();
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            foreach (var Resource in keyList)
                {
                if (SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[Resource] != 0)
                    {
                    storedResourceList.Add(Resource);
                    continue;
                    }
                }
            foreach (var storedResource in storedResourceList)
                {
                var Item = Instantiate(ItemImagePrefab, ScrollViewContent.transform);
                Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(storedResource);
                Item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(SelectedGameobject.GetComponentInParent<ResourceBuildingsManagment>().StoredResources[storedResource].ToString());
                Item.name = storedResource;
                }
            }
        private void UpdateUiBuildingInformationen()
            {
            if (SelectedGameobject != SavedGameObject || DropdownOptions.Count.Equals(0))
                {
                ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                DropdownOptions.Clear();
                DropDownMenuHandler();
                OnClickInfoPanelTextUpdate?.Invoke();
                }
            if (ObjectName.Length == 0)
                {
                ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                OnClickInfoPanelTextUpdate?.Invoke();
                }
            if (SelectedGameobject.transform.parent.GetComponent<ResourceBuildingAccountant>().SelecedResource != ResourcesDropdown.value)
                {
                ResourcesDropdown.value = SelectedGameobject.transform.parent.GetComponent<ResourceBuildingAccountant>().SelecedResource;
                OnClickInfoPanelTextUpdate?.Invoke();
                }
            }

        private void DropDownMenuHandler()
            {
            int i = 0;
            int x = 0;
            foreach (var child in SelectedGameobject.transform.parent.GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea)
                {
                if (SelectedGameobject.transform.parent.GetComponent<ResourceBuildingAccountant>().GatherableResouceInArea[x] == null)
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
            SavedGameObject = SelectedGameobject;
            GeneralUserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
            }

        void DropdownValueChanged()
            {
            SelectedGameobject.transform.parent.GetComponent<ResourceBuildingAccountant>().SetSelecedResource(ResourcesDropdown.value);
            }
        }
    }
