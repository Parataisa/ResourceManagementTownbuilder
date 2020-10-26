using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResourceHandlingBuildingInterfaceOnClick : OnClickInterfaceBase, IResourceHandling
        {
        [SerializeField] internal GameObject workingPeopleText;
        public GameObject WorkingPeopleText { get => workingPeopleText; set => workingPeopleText = value; }

        protected override void OnEnable()
            {
            base.OnEnable();
            SetWorkingPeopleText();
            }
        private void SetWorkingPeopleText()
            {
            FindObjectOfType<ResourceBuildingPeopleManagment>().WorkingPeopleText = this.GetComponent<IResourceHandling>().WorkingPeopleText;
            }

        }
    }
