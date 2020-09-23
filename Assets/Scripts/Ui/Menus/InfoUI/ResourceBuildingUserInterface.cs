using Assets.Scripts.Buildings.ResourceBuildings;
using TMPro;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceBuildingUserInterface : GeneralUserInterface
        {
        private string BuildingName = "";
        public int StortedResources = 0;
        public float ProduktionSpeed = 0f;
        public int WorkingPeopleCapacity = 0;
        public int WorkingPeople;
        public int GatheredResourcesOverall = 0;
        private ResourceBuildingsManagment currentGameObjectScript;

        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBuildingsManagment>();
                    }
                if (currentGameObjectScript != selectedGameobject.GetComponent<ResourceBuildingsManagment>())
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBuildingsManagment>();
                    }
                else
                    {
                    if (!BuildingName.Equals(currentGameObjectScript.ResourceBuildingType))
                        {
                        BuildingName = currentGameObjectScript.ResourceBuildingType;
                        this.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().SetText(BuildingName);
                        }
                    if (!StortedResources.Equals(currentGameObjectScript.StortedResources))
                        {
                        StortedResources = currentGameObjectScript.StortedResources;
                        this.transform.Find("StortedResources").GetComponent<TextMeshProUGUI>().SetText(StortedResources.ToString());
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
        }
    }
