using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class RoomObjectSO: ScriptableObject
    {
        public List<RoomTypeSO> RoomTypes;
        public RoomObjectTypeSO RoomObjectType;
        public GameObject RoomObjectPrefab;
        public string RoomObjectName;
        public Sprite RoomObjectSprite;
        public bool IsRotatingWithWall;
        public bool IsHidingWall;
    }
}