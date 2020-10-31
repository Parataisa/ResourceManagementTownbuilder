using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.SocialBuildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        internal override int Layer => LayerClass.SocialBuildings;
        [SerializeField] private Slider Progressbar;
        protected override void Start()
            {
            base.Start();
            UserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
            }
        private void OnDisable()
            {
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
                Progressbar.value = SelectedGameobject.GetComponentInParent<SocialBuildingManagment>().BirthProgress;
                if (SelectedGameobject != SavedGameObject)
                    {
                    SavedGameObject = SelectedGameobject;
                    UserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    if (SelectedGameobject != SavedGameObject)
                        {
                        ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    }
                }
            }
        }
    }