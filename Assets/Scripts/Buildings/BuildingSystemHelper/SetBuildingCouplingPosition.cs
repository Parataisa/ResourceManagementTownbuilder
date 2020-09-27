using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public static class SetBuildingCouplingPosition
        {
        public static void MoveBuildingToTheWest(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            }
        public static void MoveBuildingToTheSouth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2);
            }
        public static void MoveBuildingToTheEast(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            }
        public static void MoveBuildingToTheNorth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2);
            }
        }
    }
