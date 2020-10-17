using Assets.Scripts.Buildings.SocialBuildings;
using TMPro;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingUserInterface : GeneralUserInterface
        {
        public float BirthRate = 0f;
        public int People = 0;
        public int PeopleCapacity = 0;
        private SocialBuildingManagment currentGameObjectScript;

        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<SocialBuildingManagment>();
                    }
                if (currentGameObjectScript != selectedGameobject.GetComponent<SocialBuildingManagment>())
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<SocialBuildingManagment>();
                    }
                else
                    {
                    if (!ObjectName.Equals(currentGameObjectScript.SocialBuildingType))
                        {
                        ObjectName = currentGameObjectScript.SocialBuildingType;
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    if (!BirthRate.Equals(currentGameObjectScript.BirthRate))
                        {
                        BirthRate = currentGameObjectScript.BirthRate;
                        this.transform.Find("BirthRate").GetComponent<TextMeshProUGUI>().SetText(BirthRate.ToString());
                        }
                    if (!PeopleCapacity.Equals(currentGameObjectScript.PeopleCapacity))
                        {
                        PeopleCapacity = currentGameObjectScript.PeopleCapacity;
                        this.transform.Find("People").GetComponent<TextMeshProUGUI>().SetText(People.ToString() + "/" + PeopleCapacity.ToString());
                        }
                    if (!People.Equals(currentGameObjectScript.People))
                        {
                        People = currentGameObjectScript.People;
                        this.transform.Find("People").GetComponent<TextMeshProUGUI>().SetText(People.ToString() + "/" + PeopleCapacity.ToString());
                        }
                    }
                }
            }
        }
    }
