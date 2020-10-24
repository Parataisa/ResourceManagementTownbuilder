using Assets.Scripts.Buildings.BuildingSystemHelper;
using TMPro;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        internal override int Layer => LayerClass.SocialBuildings;
        protected override void Start()
            {
            base.Start();
            GeneralUserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
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
                if (SelectedGameobject != SavedGameObject)
                    {
                    SavedGameObject = SelectedGameobject;
                    GeneralUserInterfaceManagment.CurrentSelectedGameObject = SelectedGameobject;
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