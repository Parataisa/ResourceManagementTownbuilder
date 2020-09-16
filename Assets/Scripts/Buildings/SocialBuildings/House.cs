using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buildings.SocialBuildings
    {
    class House : MonoBehaviour , ISocialBuildings
        {
        private Color color = new Color();
        public Color BuildingColor { get => color; }

        private Color GetBuildingsColor()
            {
            Color color = GetComponent<Renderer>().material.color;
            return color;
            }

        private void Start()
            {
            color = GetBuildingsColor();
            }
        }
    }
