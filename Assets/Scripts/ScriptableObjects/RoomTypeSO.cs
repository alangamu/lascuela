using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class RoomTypeSO : ScriptableObject
    {
        public List<RecipeEntry> RecipeEntries;
        public Material RoomMaterial;
        public int MinSizeX;
        public int MinSizeZ;
    }
}