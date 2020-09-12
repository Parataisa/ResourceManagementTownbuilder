using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Lumberjack : MonoBehaviour, IResourcBuilding
        {
        public List<string> ResouceToGather { get => resouces; set => SetResouces(resouces); }
        private readonly List<string> resouces = new List<string>();
        private void Start()
            {
            SetResouces(resouces);
            }
        private List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Tree");
            return resouces;
            }
        }
    }
