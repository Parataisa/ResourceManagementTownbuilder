using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public static class SetBuildingCouplingPosition
        {
        public static bool MoveBuildingToTheNorth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2);
            if (!IsPositionAvailable(c, h))
                {
                return false;
                }
            return true;
            }
        public static bool MoveBuildingToTheEast(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            if (!IsPositionAvailable(c, h))
                {
                return false;
                }
            return true;
            }
        public static bool MoveBuildingToTheSouth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2);
            if (!IsPositionAvailable(c, h))
                {
                return false;
                }
            return true;
            }
        public static bool MoveBuildingToTheWest(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            if (!IsPositionAvailable(c, h))
                {
                return false;
                }
            return true;
            }
        private static bool IsPositionAvailable(GameObject objectToPlace, GameObject parent)
            {
            List<Transform> ListOfPositionsOfBuildings = new List<Transform>();
            if (parent.transform.parent.transform.childCount == 0)
                return true;
            for (int i = 0; i < parent.transform.parent.transform.childCount; i++)
                {
                ListOfPositionsOfBuildings.Add(parent.transform.parent.transform.GetChild(i));
                }
            foreach (Transform Child in ListOfPositionsOfBuildings)
                {
                if (Child.position == objectToPlace.transform.position)
                    {
                    return false;
                    }
                }
            return true;
            }
        }
    }
