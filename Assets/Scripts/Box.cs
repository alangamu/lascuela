using UnityEngine;

namespace Lascuela.Scripts
{
    [System.Serializable]
    public class Box
    {
        public Vector3 baseMin;
        public Vector3 baseMax;

        public Vector3 Center
        {
            get
            {
                Vector3 center = baseMin + (baseMax - baseMin) * .5f;
                center.y = (baseMax - baseMin).magnitude * .5f;
                return center;
            }
        }

        public Vector3 Size
        {
            get
            {
                return new Vector3(Mathf.Abs(baseMax.x - baseMin.x), (baseMax - baseMin).magnitude, Mathf.Abs(baseMax.z - baseMin.z));
            }
        }

        public Vector3 Extents
        {
            get
            {
                return Size * .5f;
            }
        }
    }
}
