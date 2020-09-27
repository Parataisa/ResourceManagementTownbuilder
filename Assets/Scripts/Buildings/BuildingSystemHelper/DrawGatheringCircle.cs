using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Buildings.BuildingSystemHelper
    {
    public static class DrawGatheringCircle
        {
        public static void DrawCircle(GameObject container, float radius, float lineWidth = 0.2f)
            {
            var segments = 360;
            if (container.GetComponent<LineRenderer>() == null)
                {
                container.AddComponent<LineRenderer>();
                }
            else
                {
                var line = container.GetComponent<LineRenderer>();
                line.useWorldSpace = false;
                line.startWidth = lineWidth;
                line.endWidth = lineWidth;
                line.positionCount = segments + 1;

                var pointCount = segments + 1;
                var points = new Vector3[pointCount];

                for (int i = 0; i < pointCount; i++)
                    {
                    var rad = Mathf.Deg2Rad * (i * 360f / segments);
                    points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
                    }
                line.SetPositions(points);
                }
            }
        }
    }
