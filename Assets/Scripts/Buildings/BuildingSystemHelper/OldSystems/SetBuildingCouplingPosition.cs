namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    /*
    public static class SetBuildingCouplingPosition
        {
        public static bool MoveBuildingToTheNorth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2);
            if (!IsPositionAvailable(c, h))
                {
                Debug.Log("Not Placable North");
                return false;
                }
            Debug.Log("Placable North");
            return true;
            }
        public static bool MoveBuildingToTheEast(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x + h.transform.lossyScale.x / 2 + c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            if (!IsPositionAvailable(c, h))
                {
                Debug.Log("Not Placable East");
                return false;
                }
            Debug.Log("Placable East");
            return true;
            }
        public static bool MoveBuildingToTheSouth(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x, c.transform.position.y, h.transform.position.z - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2);
            if (!IsPositionAvailable(c, h))
                {
                Debug.Log("Not Placable South");
                return false;
                }
            Debug.Log("Placable South");
            return true;
            }
        public static bool MoveBuildingToTheWest(GameObject c, GameObject h)
            {
            c.transform.position = new Vector3(h.transform.position.x - h.transform.lossyScale.x / 2 - c.transform.lossyScale.x / 2, c.transform.position.y, h.transform.position.z);
            if (!IsPositionAvailable(c, h))
                {
                Debug.Log("Not Placable West");
                return false;
                }
            Debug.Log("Placable West");
            return true;
            }
        private static bool IsPositionAvailable(GameObject objectToPlace, GameObject hitObject)
            {
            List<Vector3> listOfNeighbourPositions = hitObject.GetComponent<IBuildings>().NeighbourPosititions;
            if (listOfNeighbourPositions.Count == 0)
                {
                return true;
                }
            foreach (var neighbour in listOfNeighbourPositions)
                {
                if (neighbour == objectToPlace.transform.position)
                    {
                    return false;
                    }
                return true;
                }
            return true;
            }
        } */
    }
