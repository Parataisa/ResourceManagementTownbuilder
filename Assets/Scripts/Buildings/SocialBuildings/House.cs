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
        public string BuildingTyp => this.GetType().Name;
        public Vector3 BuildingPosition => this.gameObject.transform.position;
        public Vector3 BuildingSize => this.gameObject.transform.lossyScale;

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
