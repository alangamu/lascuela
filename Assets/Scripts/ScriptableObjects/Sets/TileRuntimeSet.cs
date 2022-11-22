using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Sets/TileRuntimeSet")]
    public class TileRuntimeSet : RuntimeSet<ITile>    
    {
        [SerializeField]
        private Material _disabledWallMaterial;
        [SerializeField]
        private RoomTypeVariable _activeRoomType;
        [SerializeField]
        private IntVariable _gridSizeX;
        [SerializeField]
        private IntVariable _gridSizeZ;

        private ITile _firstTilePreview;
        private ITile _secondTilePreview;

        public void SetFirstTilePreview(ITile firstTilePreview)
        {
            _firstTilePreview = firstTilePreview;
        }

        public void ShowRoomPreview(ITile secondTile)
        {
            List<ITile> tiles = new();
            _secondTilePreview = secondTile;

            foreach (var item in Items)
            {
                item.ClearWallPreview();
                item.SetRoomSelected(false);
            }

            if (_firstTilePreview.X <= _secondTilePreview.X && _firstTilePreview.Z <= _secondTilePreview.Z)
            {
                tiles = Items.FindAll(x =>
                x.X >= _firstTilePreview.X && x.X <= _secondTilePreview.X &&
                x.Z >= _firstTilePreview.Z && x.Z <= _secondTilePreview.Z);
            }

            if (_firstTilePreview.X <= _secondTilePreview.X && _firstTilePreview.Z > _secondTilePreview.Z)
            {
                tiles = Items.FindAll(x =>
                x.X >= _firstTilePreview.X && x.X <= _secondTilePreview.X &&
                x.Z <= _firstTilePreview.Z && x.Z >= _secondTilePreview.Z);
            }

            if (_firstTilePreview.X > _secondTilePreview.X && _firstTilePreview.Z <= _secondTilePreview.Z)
            {
                tiles = Items.FindAll(x =>
                x.X <= _firstTilePreview.X && x.X >= _secondTilePreview.X &&
                x.Z >= _firstTilePreview.Z && x.Z <= _secondTilePreview.Z);
            }

            if (_firstTilePreview.X > _secondTilePreview.X && _firstTilePreview.Z > _secondTilePreview.Z)
            {
                tiles = Items.FindAll(x =>
                x.X <= _firstTilePreview.X && x.X >= _secondTilePreview.X &&
                x.Z <= _firstTilePreview.Z && x.Z >= _secondTilePreview.Z);
            }

            Material wallMaterial = IsUnderMinSize(tiles) ? _disabledWallMaterial : _activeRoomType.Value.RoomMaterial;

            foreach (var item in tiles)
            {
                item.SetRoomSelected(true);
            }
            foreach (var item in tiles)
            {
                item.SetWallMaterial(wallMaterial);
                item.ShowWallPreview();
            }
        }

        public bool IsTopTileSelected(ITile tile)
        {
            ITile topTile = Items.Find(x => x.X == tile.X && x.Z == tile.Z + 1);
            if (topTile == null)
            {
                return false;
            }

            return topTile.IsSelected;
        }

        public bool IsBottomTileSelected(ITile tile)
        {
            ITile bottomTile = Items.Find(x => x.X == tile.X && x.Z == tile.Z - 1);
            if (bottomTile == null)
            {
                return false;
            }

            return bottomTile.IsSelected;
        }

        public bool IsLeftTileSelected(ITile tile)
        {
            ITile leftTile = Items.Find(x => x.X == tile.X - 1 && x.Z == tile.Z);
            if (leftTile == null)
            {
                return false;
            }

            return leftTile.IsSelected;
        }

        public bool IsRightTileSelected(ITile tile)
        {
            ITile rightTile = Items.Find(x => x.X == tile.X + 1 && x.Z == tile.Z);
            if (rightTile == null)
            {
                return false;
            }

            return rightTile.IsSelected;
        }

        private bool IsUnderMinSize(List<ITile> tiles)
        {
            return tiles.Count < _activeRoomType.Value.MinSizeX * _activeRoomType.Value.MinSizeZ;
        }
    }
}