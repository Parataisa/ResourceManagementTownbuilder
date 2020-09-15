using UnityEngine;

namespace Assets.Scripts.Ui.Menus.InfoUI
    {
    class GeneralUserInterfaceManagment : MonoBehaviour
        {
        public new Camera camera;
        public void Start()
            {
            camera = Camera.main;
            }
        public void Update()
            {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
                {
                if (hitInfo.transform.gameObject.layer == 8 || hitInfo.transform.gameObject.layer == 9 || hitInfo.transform.gameObject.layer == 10)
                    {
                    GameObject parent = hitInfo.transform.parent.gameObject;
                    if (parent.name.Contains("(ResoucePatch)-"))
                        {
                        ResoucePatchesUserInterface _ = new ResoucePatchesUserInterface();
                        }
                    else if (parent.name.Contains("(ResouceBuildingMain)-"))
                        {
                        ResouceBuildingUserInterface _ = new ResouceBuildingUserInterface();
                        }
                    else if (parent.name.Contains("(SocialBuildingMain)-"))
                        {
                        SocialBuildingUserInterface _ = new SocialBuildingUserInterface();
                        }
                    }
                }
            }
        }
    }
