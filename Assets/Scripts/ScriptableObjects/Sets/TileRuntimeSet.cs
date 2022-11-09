using Lascuela.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Sets/TileRuntimeSet")]
    public class TileRuntimeSet : RuntimeSet<ITile>    
    {
        private ITile _firstTilePreview;

        public void SetFirstTilePreview(ITile firstTilePreview)
        {
            _firstTilePreview = firstTilePreview;
        }

        public void ShowWallPreview(ITile secondTile)
        {
            List<ITile> tiles = new();
            foreach (var item in Items)
            {
                item.ClearWallPreview();
                item.SetRoomSelected(false);
            }

            if (_firstTilePreview.X <= secondTile.X && _firstTilePreview.Z <= secondTile.Z)
            {
                tiles = Items.FindAll(x =>
                x.X >= _firstTilePreview.X && x.X <= secondTile.X &&
                x.Z >= _firstTilePreview.Z && x.Z <= secondTile.Z);
            }

            if (_firstTilePreview.X <= secondTile.X && _firstTilePreview.Z > secondTile.Z)
            {
                tiles = Items.FindAll(x =>
                x.X >= _firstTilePreview.X && x.X <= secondTile.X &&
                x.Z <= _firstTilePreview.Z && x.Z >= secondTile.Z);
            }

            if (_firstTilePreview.X > secondTile.X && _firstTilePreview.Z <= secondTile.Z)
            {
                tiles = Items.FindAll(x =>
                x.X <= _firstTilePreview.X && x.X >= secondTile.X &&
                x.Z >= _firstTilePreview.Z && x.Z <= secondTile.Z);
            }

            if (_firstTilePreview.X > secondTile.X && _firstTilePreview.Z > secondTile.Z)
            {
                tiles = Items.FindAll(x =>
                x.X <= _firstTilePreview.X && x.X >= secondTile.X &&
                x.Z <= _firstTilePreview.Z && x.Z >= secondTile.Z);
            }

            foreach (var item in tiles)
            {
                item.SetRoomSelected(true);
            }
            foreach (var item in tiles)
            {
                item.ShowWallPreview();
            }
        }

        public void ShowPreview()
        {
            List<ITile> tiles = Items.FindAll(x => x.IsSelected);

            foreach (var item in tiles)
            {
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
    }
}