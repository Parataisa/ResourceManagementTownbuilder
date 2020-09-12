﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings.ResourceBuildings
    {
    class Farm : MonoBehaviour, IResourcBuilding
        {
        public List<string> ResouceToGather { get => resouces; set => SetResouces(resouces); }
        private readonly List<string> resouces = new List<string>();
        private void Start()
            {
            SetResouces(resouces);
            }
        private List<string> SetResouces(List<string> resoucesToGather)
            {
            resoucesToGather.Add("Wheat");
            return resouces;
            }
        }
    }
