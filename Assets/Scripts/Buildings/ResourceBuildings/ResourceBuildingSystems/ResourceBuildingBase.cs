using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class ResourceBuildingBase : BuildingBase, IResourcBuilding
        {
        internal readonly List<string> resouces = new List<string>();
        public List<string> ResourceToGather { get => resouces; set => SetResouces(resouces); }

        protected override void Start()
            {
            base.Start();
            SetResouces(resouces);
            }
        virtual internal List<string> SetResouces(List<string> resoucesToGather)
            {
            return resouces;
            }
        }
    }
