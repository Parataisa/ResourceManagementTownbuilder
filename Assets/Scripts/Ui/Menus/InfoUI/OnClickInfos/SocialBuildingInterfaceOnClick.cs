using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        private void Awake()
            {
            generalUi = FindObjectOfType<GeneralUserInterfaceManagment>();
            generalUi.OnClickInfoPanelToggled += GetGameObject;
            }
        private void Start()
            {
            savedeGameObject = selectedGameobject;
            generalUi.CurrentOnClickGameObject = selectedGameobject;
            }
        private void OnDisable()
            {
            savedeGameObject = null;
            selectedGameobject = null;
            }
        private void OnEnable()
            {
            generalUi.CurrentOnClickGameObject = selectedGameobject;
            }
        private void Update()
            {
            if (this.enabled)
                {
                if (selectedGameobject == null)
                    {
                    return;
                    }
                if (selectedGameobject != savedeGameObject)
                    {
                    savedeGameObject = selectedGameobject;
                    generalUi.CurrentOnClickGameObject = selectedGameobject;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    if (selectedGameobject != savedeGameObject)
                        {
                        ObjectName = GetObjectName(selectedGameobject.transform.parent.name);
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                        }
                    }
                }
            }
        private string GetObjectName(string name)
            {
            string[] BuildingNameArray = name.Split('-');
            return BuildingNameArray[1].Split('(')[0];
            }

        private void GetGameObject(GameObject gameObject)
            {
            selectedGameobject = gameObject;
            }
        }
    }