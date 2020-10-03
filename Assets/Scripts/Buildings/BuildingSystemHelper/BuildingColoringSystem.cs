using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public static class BuildingColoringSystem
        {

        public static void SetColorForCollisions(GameObject currentPlaceableObject, Color color)
            {
            currentPlaceableObject.GetComponent<Renderer>().material.color = color;
            }
        public static void ResetBuildingToOrigionColor(GameObject currentPlaceableObject)
            {
            currentPlaceableObject.GetComponent<Renderer>().material.color = currentPlaceableObject.GetComponent<IBuildings>().BuildingColor;
            }
        }
    }
