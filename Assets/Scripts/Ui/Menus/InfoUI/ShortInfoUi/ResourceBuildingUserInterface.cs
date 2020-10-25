using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceBuildingUserInterface : GeneralUserInterface
        {
        private string ResourceWithTheHighestAmount = "";
        public int StortedResources = 0;
        public float ProduktionSpeed = 0f;
        public int WorkingPeopleCapacity = 0;
        public int WorkingPeople;
        public int GatheredResourcesOverall = 0;
        private ResourceBuildingsManagment currentGameObjectScript;
        internal override int Layer => LayerClass.ResourceBuildings;
        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    currentGameObjectScript = selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>();
                if (currentGameObjectScript != selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>())
                    currentGameObjectScript = selectedGameobject.GetComponentInParent<ResourceBuildingsManagment>();
                else
                    {
                    if (!ObjectName.Equals(currentGameObjectScript.BuildingTyp.BuildingTyp))
                        {
                        ObjectName = currentGameObjectScript.BuildingTyp.BuildingTyp;
                        this.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    if (!StortedResources.Equals(currentGameObjectScript.StoredResources))
                        {
                        StortedResources = currentGameObjectScript.StoredResources[currentGameObjectScript.BuildingTyp.ResourceToGather[GrabTheResourceWithTheLargesValue()]];
                        this.transform.Find("StortedResources").GetComponent<TextMeshProUGUI>().SetText(ResourceWithTheHighestAmount + ": " + StortedResources.ToString());
                        }
                    if (!ProduktionSpeed.Equals(currentGameObjectScript.ProduktionSpeed))
                        {
                        ProduktionSpeed = currentGameObjectScript.ProduktionSpeed;
                        this.transform.Find("ProduktionSpeed").GetComponent<TextMeshProUGUI>().SetText(ProduktionSpeed.ToString());
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
                    if (!GatheredResourcesOverall.Equals(currentGameObjectScript.GatheredResourcesOverall))
                        {
                        GatheredResourcesOverall = currentGameObjectScript.GatheredResourcesOverall;
                        this.transform.Find("GatheredResourcesOverall").GetComponent<TextMeshProUGUI>().SetText(GatheredResourcesOverall.ToString());
                        }
                    }
                }

            }

        private int GrabTheResourceWithTheLargesValue()
            {
            List<string> keyList = new List<string>(currentGameObjectScript.StoredResources.Keys);
            if (keyList.Count == 1)
                {
                this.ResourceWithTheHighestAmount = keyList[0];
                return 0;
                }
            int x = currentGameObjectScript.StoredResources[keyList[0]];
            int ResourceWithTheHighestValue = 0;
            for (int i = 0; i < keyList.Count; i++)
                {
                if (x < currentGameObjectScript.StoredResources[keyList[i]])
                    {
                    x = currentGameObjectScript.StoredResources[keyList[i]];
                    ResourceWithTheHighestValue = i;
                    if (i == keyList.Count)
                        {
                        this.ResourceWithTheHighestAmount = keyList[ResourceWithTheHighestValue];
                        return ResourceWithTheHighestValue;
                        }
                    }
                continue;
                }
            this.ResourceWithTheHighestAmount = keyList[ResourceWithTheHighestValue];
            return ResourceWithTheHighestValue;
            }
        }
    }


