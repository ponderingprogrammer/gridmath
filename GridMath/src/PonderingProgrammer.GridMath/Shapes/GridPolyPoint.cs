﻿using System;
using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridPolyPoint : AbstractGridShape
    {
        public GridPolyPoint(IEnumerable<GridCoordinatePair> coordinates)
        {
            Coords = new List<GridCoordinatePair>(coordinates);
            Update();
        }

        public void AddPoint(GridCoordinatePair coordinates)
        {
            Coords.Add(coordinates);
            Update();
        }

        public void RemovePoint(GridCoordinatePair coordiantes)
        {
            Coords.Remove(coordiantes);
            Update();
        }

        public override void Translate(int x, int y)
        {
            for (var i = 0; i < Coords.Count; i++)
            {
                Coords[i] = Coords[i].Translation(x, y);
            }
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            throw new System.NotImplementedException();
        }

        public override void Flip(GridAxis axis)
        {
            throw new System.NotImplementedException();
        }

        protected override void Update()
        {
            var minX = int.MaxValue;
            var minY = int.MaxValue;
            var maxX = int.MinValue;
            var maxY = int.MinValue;
            foreach (var coords in Coordinates)
            {
                if (coords.X > maxX) maxX = coords.X;
                if (coords.Y > maxY) maxY = coords.Y;
                if (coords.X < minX) minX = coords.X;
                if (coords.Y < minY) minY = coords.Y;
            }
            BBox = GridBoundingBox.FromMinMax(minX, minY, maxX, maxY);
        }
    }
}