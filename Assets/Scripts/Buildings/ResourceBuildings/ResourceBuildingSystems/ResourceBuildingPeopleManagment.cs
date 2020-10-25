using Assets.Scripts.AvailableResouceManagment;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems
    {
    class ResourceBuildingPeopleManagment : MonoBehaviour
        {
        public GameObject WorkingPeopleText;
        public OnClickInterfaceBase BuidlingOnClickUi;
        public ResourceHandlingBuildingBase selectedBuilding;
        private int selectedBuildingCapacity = 0;
        public bool TextUpdateSet;

        private void Update()
            {
            if (BuidlingOnClickUi != null && !TextUpdateSet)
                {
                BuidlingOnClickUi.OnClickInfoPanelTextUpdate += SetWorkingPeopleText;
                TextUpdateSet = true;
                }
            }
        public void IncreaseManpower()
            {
            GetSelectedBuilding();
            if (AvailableManpower.AvailablePeople > AvailableManpower.BusyPeople)
                {
                if (selectedBuilding.WorkingPeople < selectedBuildingCapacity)
                    {
                    selectedBuilding.WorkingPeople += 1;
                    AvailableManpower.BusyPeople += 1;
                    selectedBuilding.UpdateWorkingPeople();
                    SetWorkingPeopleText();
                    }
                }
            }
        public void DecreaseManpower()
            {
            GetSelectedBuilding();
            if (selectedBuilding.WorkingPeople > 0)
                {
                selectedBuilding.WorkingPeople -= 1;
                AvailableManpower.BusyPeople -= 1;
                selectedBuilding.UpdateWorkingPeople();
                SetWorkingPeopleText();
                }
            }

        private void GetSelectedBuilding()
            {
            selectedBuilding = BuidlingOnClickUi.SelectedGameobject.transform.parent
                .GetComponent<ResourceHandlingBuildingBase>();
            selectedBuildingCapacity = selectedBuilding.WorkingPeopleCapacity;
            }
        public void SetWorkingPeopleText()
            {
            GetSelectedBuilding();
            WorkingPeopleText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Working People: " + selectedBuilding.WorkingPeople.ToString() + "/" + selectedBuildingCapacity.ToString());
            }
        }
    }
