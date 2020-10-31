using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.BuildingTransportationSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.StorageBuildings.StorageBuildingSystems
    {
    class StorageBuildingTransportSystem : MonoBehaviour
        {
        private StorageBuildingManagment buildingManagment;
        [SerializeField] private GameObject workerPrefab;
        private int sendWorker = 0;
        private void Start()
            {
            buildingManagment = this.gameObject.GetComponent<StorageBuildingManagment>();
            workerPrefab = (GameObject)Resources.Load("GameObjects/ResourceTransport/TransportWorker");
            }

        private void Update()
            {
            if (buildingManagment.TargetBuilding == null)
                return;
            else if (buildingManagment.WorkingPeople < 1)
                return;
            else if (sendWorker < buildingManagment.WorkingPeople && !buildingManagment.WarehouseFull)
                {
                SendPeopleToCollectResourcesFromBuilding();
                }
            }

        private void SendPeopleToCollectResourcesFromBuilding()
            {
            sendWorker++;
            GameObject[] subBuildingPosition = FindTargetAndOrigenSubBuildingsWithShortestWay(buildingManagment.gameObject, buildingManagment.TargetBuilding);
            var newWorker = Instantiate(workerPrefab, this.gameObject.transform);
            newWorker.transform.position = subBuildingPosition[1].transform.position;
            newWorker.GetComponent<TransportUnitBehaviour>().SetBuildingData(subBuildingPosition);
            }
        public GameObject[] OnWorkerReturn(Dictionary<string, int> resources, bool haveResources)
            {
            if (haveResources)
                {
                buildingManagment.AddResources(resources);
                }
            GameObject[] subBuildingPosition = FindTargetAndOrigenSubBuildingsWithShortestWay(buildingManagment.gameObject, buildingManagment.TargetBuilding);
            return subBuildingPosition;
            }
        public bool DestoryWorkingPeople()
            {
            if (sendWorker < buildingManagment.WorkingPeople)
                {
                return false;
                }
            else if (sendWorker > buildingManagment.WorkingPeople)
                {
                --sendWorker;
                return true;
                }
            return false;
            }

        private GameObject[] FindTargetAndOrigenSubBuildingsWithShortestWay(GameObject origenMain, GameObject targetMain)
            {
            GameObject[] bestBuildings = new GameObject[2];
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = origenMain.transform.position;
            foreach (var target in targetMain.GetComponent<IBuildingManagment>().ListOfChildren)
                {
                Vector3 directionToTarget = target.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                    {
                    closestDistanceSqr = dSqrToTarget;
                    bestBuildings[0] = target;
                    }
                }
            foreach (var nearestHomeBuilding in origenMain.GetComponent<IBuildingManagment>().ListOfChildren)
                {
                Vector3 directionToTarget = nearestHomeBuilding.transform.position - bestBuildings[0].transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                    {
                    closestDistanceSqr = dSqrToTarget;
                    bestBuildings[1] = nearestHomeBuilding;
                    }
                }
            return bestBuildings;
            }
        }
    }
