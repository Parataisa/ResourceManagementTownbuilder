using Assets.Scripts.Buildings.BuildingTransportationSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
            var newWorker = Instantiate(workerPrefab, this.gameObject.transform);
            newWorker.transform.position = this.gameObject.transform.GetChild(0).transform.localPosition + Vector3.forward + Vector3.right;
            newWorker.GetComponent<TransportUnitBehaviour>().SetBuildingData(buildingManagment.gameObject, buildingManagment.TargetBuilding);
            }
        public void OnWorkerReturn(Dictionary<string,int> resources)
            {
            sendWorker--;
            buildingManagment.AddResources(resources);
            }
        }
    }
