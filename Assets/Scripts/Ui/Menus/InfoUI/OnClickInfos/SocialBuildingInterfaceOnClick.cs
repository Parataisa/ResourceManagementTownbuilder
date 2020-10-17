using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class SocialBuildingInterfaceOnClick : OnClickInterfaceBase
        {
        private void OnDisable()
            {
            savedeGameObject = null;
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
                if (SelectedGameobject != savedeGameObject)
                    {
                    savedeGameObject = SelectedGameobject;
                    GeneralUserInterfaceManagment.CurrentOnClickGameObject = SelectedGameobject;
                    }
                if (ObjectName.Length == 0)
                    {
                    ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
                    this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(ObjectName);
                    if (SelectedGameobject != savedeGameObject)
                        {
                        ObjectName = GetObjectName(SelectedGameobject.transform.parent.name);
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
        }
    }