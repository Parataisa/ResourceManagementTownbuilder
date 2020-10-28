using Assets.Scripts.Buildings.BuildingSystemHelper;
using Assets.Scripts.Buildings.ResourceBuildings.ResourceBuildingSystems;
using Assets.Scripts.Buildings.StorageBuildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingTransportationSystem
    {
    class TransportUnitBehaviour : MonoBehaviour
        {
        IEnumerator currentMoveCoroutine;
        IEnumerator waitTimer;
        private Dictionary<string, int> transportedResource;
        private float moveSpeed = 4.5f;
        private int carryingCapacity = 5;
        private GameObject origenBuilding;
        private GameObject targetBuilding;
        private bool targetReacht = false;
        private bool haveResources = false;
        private bool noResourcesToGatherAtTheTime = false;
        private bool waitTimerIsRunning = false;
        private void Start()
            {
            waitTimer = WaitForResources();
            currentMoveCoroutine = GoingToTargetBuilding(targetBuilding);
            StartCoroutine(currentMoveCoroutine);
            }
        private void OnEnable()
            {
            GameObject[] SetSubBuildings = FindTargetAndOrigenSubBuildingsWithShortestWay(targetBuilding, origenBuilding);
            targetBuilding = SetSubBuildings[0];
            origenBuilding = SetSubBuildings[1];
            }

        private void Update()
            {
            //Target position
            if (transform.localPosition == targetBuilding.transform.GetChild(0).transform.localPosition)
                {
                targetReacht = true;
                StopCoroutine(currentMoveCoroutine);
                if (!waitTimerIsRunning)
                    {
                    CollectResources();
                    }
                }
            // Home building position
            if (transform.localPosition == origenBuilding.transform.GetChild(0).transform.localPosition)
                {
                targetReacht = false;
                StopCoroutine(currentMoveCoroutine);
                if (haveResources)
                    {
                    origenBuilding.GetComponent<StorageBuildingManagment>().AddResources(transportedResource);
                    }
                haveResources = false;
                transportedResource.Clear();
                targetBuilding = origenBuilding.GetComponent<StorageBuildingManagment>().TargetBuilding;
                currentMoveCoroutine = GoingToTargetBuilding(targetBuilding);
                StartCoroutine(currentMoveCoroutine);
                }
            }

        private void CollectResources()
            {
            transportedResource = new Dictionary<string, int>();
            var target = targetBuilding.GetComponent<ResourceHandlingBuildingBase>();
            int totalResources = 0;
            foreach (var resource in target.StoredResources)
                {
                totalResources += resource.Value;
                }
            if ((target.ResourceCollectionAtTheTime || totalResources == 0) && !noResourcesToGatherAtTheTime)
                {
                StartCoroutine(waitTimer);
                }
            else
                {
                if (noResourcesToGatherAtTheTime)
                    {
                    haveResources = false;
                    MoveBackToOrigenBuilding();
                    }
                else
                    {
                    string resourceToCollect = "";
                    target.ResourceCollectionAtTheTime = true;
                    foreach (var targetResource in target.StoredResources)
                        {
                        if (targetResource.Value == 0)
                            {
                            continue;
                            }
                        else
                            {
                            resourceToCollect = targetResource.Key;
                            break;
                            }
                        }
                    transportedResource.Add(resourceToCollect, 0);
                    int resourceAmountCollected = target.StoredResources[resourceToCollect] > carryingCapacity ? carryingCapacity : target.StoredResources[resourceToCollect];
                    target.StoredResources[resourceToCollect] -= resourceAmountCollected;
                    transportedResource[resourceToCollect] += resourceAmountCollected;
                    target.ResourceCollectionAtTheTime = false;
                    haveResources = true;
                    MoveBackToOrigenBuilding();
                    }
                }
            }

        private IEnumerator WaitForResources()
            {
            float timer = 0.0f;
            while (timer < 2f)
                {
                waitTimerIsRunning = true;
                timer += 0.2f;
                yield return new WaitForSeconds(0.2f);
                }
            waitTimerIsRunning = false;
            noResourcesToGatherAtTheTime = true;
            StopCoroutine(waitTimer);
            waitTimer = WaitForResources();
            }

        private void MoveBackToOrigenBuilding()
            {
            noResourcesToGatherAtTheTime = false;
            targetReacht = false;
            currentMoveCoroutine = GoingToTargetBuilding(origenBuilding);
            StartCoroutine(currentMoveCoroutine);
            }

        private IEnumerator GoingToTargetBuilding(GameObject target)
            {
            while (!targetReacht)
                {
                Vector3 targetPosition = target.transform.GetChild(0).position;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
                }
            }

        public void SetBuildingData(GameObject origen, GameObject target)
            {
            GetOrigenBuilding(origen);
            GetTargetBuilding(target);
            }
        private void GetOrigenBuilding(GameObject origen)
            {
            origenBuilding = origen;
            }
        private void GetTargetBuilding(GameObject target)
            {
            targetBuilding = target;
            }
        private GameObject[] FindTargetAndOrigenSubBuildingsWithShortestWay(GameObject targetMain, GameObject origenMain)
            {
            GameObject[] bestBuildings = new GameObject[2];
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = origenBuilding.transform.position;
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

