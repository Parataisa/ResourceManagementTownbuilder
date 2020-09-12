using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Mine : MonoBehaviour, IResourcBuilding
        {
        public List<string> ResouceToGather { get => resouces; set => SetResouces(resouces); }
        private readonly List<string> resouces = new List<string>();
        private void Start()
            {
            SetResouces(resouces);
            }
        private List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Coal");
            resoucesToGather.Add("Copper");
            resoucesToGather.Add("Iron");
            return resouces;
            }
        }
    }
