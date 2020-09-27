﻿using Assets.Scripts.Ui.Menus.InfoUI;
using ResourceGeneration.ResourceVariationen;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingBase : MonoBehaviour
        {
        public List<ResouceQuantityTyps> GatherableResouces;
        public List<GameObject> GatherableResouceInArea = new List<GameObject>();
        private List<string> ListOfGatherableResources;
        public const float ResourceCollectingRadius = 10;
        public Vector3 BuildingPosition;
        public int selecedResource = 0;

        public class ResouceQuantityTyps
            {
            public GameObject resoucePatch;
            public int resouceQuantity;
            public string resouceName;
            }
        public void Start()
            {
            BuildingPosition = GetComponent<Transform>().position;
            ListOfGatherableResources = GetComponent<IResourcBuilding>().ResouceToGather;
            GatherableResouceInArea = GatherableResouceInArea.Count == 0 ? GetUsableResources(ListOfGatherableResources) : GatherableResouceInArea;
            GatherableResouces = GetResouceQuantityInArea(GatherableResouceInArea);
            if (!GatherableResouces.Count.Equals(0))
                {
                FindObjectOfType<ResourceBuildingsManagment>().UpdateResouces += UpdateResouces;
                }        
            }

        private void UpdateResouces(GameObject childBuilding)
            {
            if (GatherableResouceInArea.Count == 0 || GatherableResouceInArea[selecedResource] == null)
                {
                return;
                }
            var selectedResouceInArea = GetComponent<ResourceBuildingBase>().GatherableResouceInArea[selecedResource].GetComponent<ResourceBase>();
            if (selectedResouceInArea.QuantityOfTheResource <= 0)
                {
                GatherableResouceInArea.RemoveAt(selecedResource);
                FindObjectOfType<ResourceBuildingsManagment>().UpdateResouces -= UpdateResouces;
                }
            else
                {
                selectedResouceInArea.QuantityOfTheResource -= 1;
                selectedResouceInArea.ResourceQuantityCheck();
                childBuilding.transform.parent.GetComponent<ResourceBuildingsManagment>().GatheredResourcesOverall += 1;
                childBuilding.transform.parent.GetComponent<ResourceBuildingsManagment>().StortedResources += 1;
                }
            }

        public void SetSelecedResource(int x)
            {
            this.selecedResource = x;
            }

        private List<ResouceQuantityTyps> GetResouceQuantityInArea(List<GameObject> gatherableResouceInArea)
            {
            List<ResouceQuantityTyps> resouceQuantityInArea = new List<ResouceQuantityTyps>();
            foreach (var resoucePatch in gatherableResouceInArea)
                {
                var resouceScript = resoucePatch.GetComponent<ResourceBase>();
                ResouceQuantityTyps resouceQuantityTyps = new ResouceQuantityTyps
                    {
                    resouceName = resouceScript.ResourceName
                    };
                if (resouceQuantityInArea.Count == 0)
                    {
                    resouceQuantityTyps.resouceQuantity = resouceScript.QuantityOfTheResource;
                    resouceQuantityTyps.resoucePatch = resouceScript.gameObject;
                    resouceQuantityInArea.Add(resouceQuantityTyps);
                    }
                else
                    {
                    int x = 0;
                    foreach (var resouceInList in resouceQuantityInArea)
                        {
                        if (resouceInList.resouceName.Contains(resouceQuantityTyps.resouceName))
                            {
                            resouceInList.resouceQuantity += resouceScript.QuantityOfTheResource;
                            break;
                            }
                        else if (!resouceInList.resouceName.Contains(resouceQuantityTyps.resouceName) && !(x < resouceQuantityInArea.Count))
                            {
                            x++;
                            continue;
                            }
                        else
                            {
                            resouceQuantityTyps.resouceQuantity = resouceScript.QuantityOfTheResource;
                            resouceQuantityTyps.resoucePatch = resouceScript.gameObject;
                            resouceQuantityInArea.Add(resouceQuantityTyps);
                            break;
                            }
                        }
                    }
                }
            return resouceQuantityInArea;
            }

        private GameObject[] ScannForResources(float radius, Vector3 startpoint, int maxScannedResources)
            {
            Collider[] collidersInArea = new Collider[maxScannedResources];
            int collisions = Physics.OverlapSphereNonAlloc(startpoint, radius, collidersInArea);
            GameObject[] gameObjectArray = new GameObject[maxScannedResources];
            if (collisions <= 2)
                {
                return gameObjectArray;
                }
            int i = 0;
            foreach (Collider collider in collidersInArea)
                {
                if (collider == null)
                    {
                    return gameObjectArray;
                    }
                if (collider.gameObject.layer == 10)
                    {
                    GameObject parent = collider.transform.parent.gameObject;
                    if (gameObjectArray.Contains(parent))
                        continue;
                    else
                        {
                        if (i > maxScannedResources)
                            {
                            return gameObjectArray;
                            }
                        gameObjectArray[i] = parent;
                        i++;
                        }
                    }
                }
            return gameObjectArray;
            }
        public List<GameObject> GetUsableResources(List<string> resources)
            {
            List<GameObject> usableResources = new List<GameObject>();
            GameObject[] resourceInArea = ScannForResources(ResourceCollectingRadius, BuildingPosition, 100);
            if (resourceInArea[0] == null)
                {
                return usableResources;
                }
            foreach (GameObject savedResource in resourceInArea)
                {
                if (savedResource == null)
                    {
                    return usableResources;
                    }
                else if (resources.Contains(savedResource.GetComponent<ResourceBase>().ResourceName))
                    {
                    usableResources.Add(savedResource);
                    }
                }
            return usableResources;
            }
        }
    }

