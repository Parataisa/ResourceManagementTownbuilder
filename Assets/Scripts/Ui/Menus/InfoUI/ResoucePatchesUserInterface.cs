using ResourceGeneration.ResourceVariationen;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class ResoucePatchesUserInterface : GeneralUserInterface
        {
        private int PatchQuantity = 0;
        private int PatchSize = 0;
        private string ObjectName = "";
        private ResourceBase currentGameObjectScript;

        private void Update()
            {
            if (this.enabled)
                {
                if (!selectedGameobject.Equals(null))
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBase>();
                    }
                if (currentGameObjectScript != selectedGameobject.GetComponent<ResourceBase>())
                    {
                    currentGameObjectScript = selectedGameobject.GetComponent<ResourceBase>();
                    }
                else
                    {
                    if (!ObjectName.Equals(selectedGameobject.name))
                        {
                        ObjectName = selectedGameobject.name;
                        this.transform.Find("ObjectName").GetComponent<TextMeshProUGUI>().SetText(selectedGameobject.name);
                        }
                    if (!PatchSize.Equals(currentGameObjectScript.sizeOfTheResource))
                        {
                        PatchSize = selectedGameobject.GetComponent<ResourceBase>().sizeOfTheResource;
                        this.transform.Find("PatchSize").GetComponent<TextMeshProUGUI>().SetText(currentGameObjectScript.sizeOfTheResource.ToString());
                        }
                    if (!PatchQuantity.Equals(currentGameObjectScript.quantityOfTheResource))
                        {
                        PatchQuantity = currentGameObjectScript.quantityOfTheResource;
                        this.transform.Find("PatchQuantity").GetComponent<TextMeshProUGUI>().SetText(currentGameObjectScript.quantityOfTheResource.ToString());
                        }

                    }
                }
            }

        }
    }

