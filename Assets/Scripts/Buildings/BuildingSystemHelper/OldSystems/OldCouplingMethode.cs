namespace Assets.Scripts.Buildings.BuildingSystemHelper.OldSystems
    {
    class OldCouplingMethode
        {
        // Old Building CouplingMethode 
        /* private bool CouplingBuildingsWithEatchOther(GameObject c, GameObject h)
             {
             Vector3 MouseCurserWorldPosition = new Vector3();
             Plane plane = new Plane(Vector3.up, 0);
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (plane.Raycast(ray, out float distance))
                 {
                 MouseCurserWorldPosition = ray.GetPoint(distance);
                 }
             if (BuildingSystemHitChecker.NorthHitCheck(h, MouseCurserWorldPosition))
                 {
                 if (SetBuildingCouplingPosition.MoveBuildingToTheNorth(c, h))
                     {
                     Debug.Log(h.transform.position);
                     BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                     objectPlacable = true;
                     return true;
                     }
                 else
                     {
                     objectPlacable = false;
                     BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                     }
                 }
             else if (BuildingSystemHitChecker.EastHitCheck(h, MouseCurserWorldPosition))
                 {
                 if (SetBuildingCouplingPosition.MoveBuildingToTheEast(c, h))
                     {
                     Debug.Log(h.transform.position);
                     BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                     objectPlacable = true;
                     return true;
                     }
                 else
                     {
                     objectPlacable = false;
                     BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                     }
                 }
             else if (BuildingSystemHitChecker.SouthHitCheck(h, MouseCurserWorldPosition))
                 {
                 if (SetBuildingCouplingPosition.MoveBuildingToTheSouth(c, h))
                     {
                     Debug.Log(h.transform.position);
                     BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                     objectPlacable = true;
                     return true;
                     }
                 else
                     {
                     objectPlacable = false;
                     BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                     }
                 }
             else if (BuildingSystemHitChecker.WestHitCheck(h, MouseCurserWorldPosition))
                 {
                 if (SetBuildingCouplingPosition.MoveBuildingToTheWest(c, h))
                     {
                     Debug.Log(h.transform.position);
                     BuildingColoringSystem.ResetBuildingToOrigionColor(currentPlaceableObject);
                     objectPlacable = true;
                     return true;
                     }
                 else
                     {
                     objectPlacable = false;
                     BuildingColoringSystem.SetColorForCollisions(currentPlaceableObject, Color.magenta);
                     }
                 }
             return false;
             }
        */
        }
    }

