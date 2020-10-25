using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.StorageBuildings;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.Ui.Menus.InfoUI.ShortInfoUi
    {
    class StorageBuildingUserInterface : GeneralUserInterface
        {
        public List<string> StortedResources;
        public int WorkingPeopleCapacity = 0;
        public int WorkingPeople;
        internal override int Layer => LayerClass.StorageBuildings;
        private StorageBuildingManagment currentGameObjectScript;

        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    currentGameObjectScript = selectedGameobject.GetComponentInParent<StorageBuildingManagment>();
                if (currentGameObjectScript != selectedGameobject.GetComponentInParent<StorageBuildingManagment>())
                    currentGameObjectScript = selectedGameobject.GetComponentInParent<StorageBuildingManagment>();
                else
                    {
                    if (!ObjectName.Equals(currentGameObjectScript.BuildingTyp.BuildingTyp))
                        {
                        ObjectName = currentGameObjectScript.BuildingTyp.BuildingTyp;
                        this.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    if (!StortedResources.Equals(currentGameObjectScript.StoredResources))
                        {
                        StortedResources.AddRange(currentGameObjectScript.StoredResources.Keys);
                        this.transform.Find("StortedResources").GetComponent<TextMeshProUGUI>().SetText(string.Join(" , ", StortedResources));
                        }
                    if (!WorkingPeopleCapacity.Equals(currentGameObjectScript.WorkingPeopleCapacity))
                        {
                        WorkingPeopleCapacity = currentGameObjectScript.WorkingPeopleCapacity;
                        }
                    if (!WorkingPeople.Equals(currentGameObjectScript.WorkingPeople))
                        {
                        WorkingPeople = currentGameObjectScript.WorkingPeople;
                        this.transform.Find("WorkingPeople").GetComponent<TextMeshProUGUI>().SetText(WorkingPeople.ToString() + "/" + WorkingPeopleCapacity.ToString());
                        }
                    }
                }
            }
        }
    }
