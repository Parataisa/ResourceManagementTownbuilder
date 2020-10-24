using Assets.Scripts.AvailableResouceManagment;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems
    {
    class ResourceBuildingPeopleManagment : MonoBehaviour
        {
        [SerializeField] private ResourceBuildingInterfaceOnClick ResourceBuidlingOnClickUi;
        [SerializeField] private GameObject WorkingPeopleText;
        public ResourceBuildingsManagment selectedBuilding;
        private int selectedBuildingCapacity = 0;

        private void Start()
            {
            var generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
            ResourceBuidlingOnClickUi.OnClickInfoPanelTextUpdate += SetWorkingPeopleText;
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
            selectedBuilding = ResourceBuidlingOnClickUi.SelectedGameobject.transform.parent
                .GetComponent<ResourceBuildingsManagment>();
            selectedBuildingCapacity = selectedBuilding.WorkingPeopleCapacity;
            }
        private void SetWorkingPeopleText()
            {
            GetSelectedBuilding();
            WorkingPeopleText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Working People: " + selectedBuilding.WorkingPeople.ToString() + "/" + selectedBuildingCapacity.ToString());
            }
        }
    }
