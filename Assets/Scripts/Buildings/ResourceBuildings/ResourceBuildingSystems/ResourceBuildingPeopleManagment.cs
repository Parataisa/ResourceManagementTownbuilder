using Assets.Scripts.AvailableResouceManagment;
using Assets.Scripts.Ui.Menus.InfoUI;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems
    {
    class ResourceBuildingPeopleManagment : MonoBehaviour
        {
        [SerializeField] private AvailableManpower Manpower;
        [SerializeField] private ResourceBuildingInterfaceOnClick ResourceBuidlingOnClickUi;
        [SerializeField] private GameObject WorkingPeopleText;
        private ResourceBuildingsManagment selectedBuilding;
        private int selectedBuildingCapacity = 0;

        private void Start()
            {
            var generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
            generalUi.OnClickInfoPanelTextUpdate += SetWorkingPeopleText;
            }
        public void IncreaseManpower()
            {
            GetSelectedBuilding();
            if (selectedBuilding.WorkingPeople < selectedBuildingCapacity)
                {
                selectedBuilding.WorkingPeople += 1;
                selectedBuilding.UpdateWorkingPeople();
                SetWorkingPeopleText();
                }
            }
        public void DecreaseManpower()
            {
            GetSelectedBuilding();
            if (selectedBuilding.WorkingPeople > 0)
                {
                selectedBuilding.WorkingPeople -= 1;
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
