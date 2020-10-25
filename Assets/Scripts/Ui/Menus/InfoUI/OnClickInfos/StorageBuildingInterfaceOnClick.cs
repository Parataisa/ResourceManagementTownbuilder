using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using Assets.Scripts.Buildings.StorageBuildings;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI.OnClickInfos
    {
    class StorageBuildingInterfaceOnClick : ResourceHandlingBuildingInterfaceOnClick
        {
        public GameObject ScrollViewContent;
        public GameObject ItemImagePrefab;
        private List<string> keyList = new List<string>();
        private List<string> storedResourceList = new List<string>();
        internal override int Layer => LayerClass.StorageBuildings;
        protected override void Start()
            {
            base.Start();
            AddingItems();
            }
        private void OnDisable()
            {
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            savedGameObject = null;
            SelectedGameobject = null;
            FindObjectOfType<ResourceBuildingPeopleManagment>().BuidlingOnClickUi = null;
            FindObjectOfType<ResourceBuildingPeopleManagment>().TextUpdateSet = false;
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (SelectedGameobject == null)
                    {
                    return;
                    }
                FindObjectOfType<ResourceBuildingPeopleManagment>().BuidlingOnClickUi = this;
                foreach (var item in SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources)
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
                if (storedResourceList.Count != ScrollViewContent.transform.childCount || SelectedGameobject != savedGameObject)
                    {
                    AddingItems();
                    }
                foreach (var Resource in storedResourceList)
                    {
                    if (SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources[Resource] != Convert.ToInt32(ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text))
                        {
                        ScrollViewContent.transform.Find(Resource).transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources[Resource].ToString());
                        }
                    }
                UpdateUiBuildingInformationen();
                }
            }
        private void AddingItems()
            {
            keyList.Clear();
            keyList.AddRange(SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources.Keys);
            storedResourceList.Clear();
            foreach (Transform child in ScrollViewContent.transform)
                {
                Destroy(child.gameObject);
                }
            foreach (var Resource in keyList)
                {
                if (SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources[Resource] != 0)
                    {
                    storedResourceList.Add(Resource);
                    continue;
                    }
                }
            foreach (var storedResource in storedResourceList)
                {
                var Item = Instantiate(ItemImagePrefab, ScrollViewContent.transform);
                Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(storedResource);
                Item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(SelectedGameobject.GetComponentInParent<StorageBuildingManagment>().StoredResources[storedResource].ToString());
                Item.name = storedResource;
                }
            }
        private void UpdateUiBuildingInformationen()
            {
            if (SelectedGameobject != savedGameObject)
                {
                ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                UserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
                }
            if (ObjectName.Length == 0)
                {
                ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                }
            }
        }
    }


