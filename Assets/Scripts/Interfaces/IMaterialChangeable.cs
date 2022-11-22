using System.Collections;
using UnityEngine;

namespace Lascuela.Scripts.Interfaces
{
    public interface IMaterialChangeable
    {
        public void ChangeWallsMaterial(MeshRenderer[] wallsRenderer, Material wallMaterial);
    }
}