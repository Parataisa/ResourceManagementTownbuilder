using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class SocialBuildingBase : MonoBehaviour
        {
        public Vector3 BuildingPosition;

        private void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            }
        }
    }
