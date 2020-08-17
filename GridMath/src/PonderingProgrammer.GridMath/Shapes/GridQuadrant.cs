﻿using System;
using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridQuadrant : AbstractGridShape
    {
        private GridCoordinatePair _origin;
        private int _radius;
        private Grid8Direction _direction;

        public GridCoordinatePair Origin
        {
            get => _origin;
            set
            {
                _origin = value;
                UpdateBoundsAndContainedCoords();
            }
        }

        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                UpdateBoundsAndContainedCoords();
            }
        }

        public GridQuadrant(GridCoordinatePair origin, int radius, Grid8Direction direction)
        {
            _origin = origin;
            _radius = radius;
            _direction = direction;
            UpdateBoundsAndContainedCoords();
        }

        public override void Translate(int x, int y)
        {
            Origin = _origin.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            Directions.Rotate(_direction, rotation);
        }

        public override void Flip(GridAxis axis)
        {
            throw new System.NotImplementedException();
        }
        
        protected sealed override void UpdateBoundsAndContainedCoords()
        {
            _boundingBox = GridBoundingBox.FromMinMax(_origin.X - _radius, _origin.Y - _radius, _origin.X + _radius,
                _origin.Y + _radius);
            _containedCoordinates = new List<GridCoordinatePair>();

            GridPolarCoordinates start;
            GridPolarCoordinates end;
            switch (_direction)
            {
                case Grid8Direction.TopRight:
                    start = new GridPolarCoordinates(0, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.5, _radius);
                    break;
                case Grid8Direction.Top:
                    start = new GridPolarCoordinates(Math.PI * 0.25, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.75, _radius);
                    break;
                case Grid8Direction.TopLeft:
                    start = new GridPolarCoordinates(Math.PI * 0.5, _radius);
                    end = new GridPolarCoordinates(Math.PI, _radius);
                    break;
                case Grid8Direction.Left:
                    start = new GridPolarCoordinates(Math.PI * 0.75, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.25, _radius);
                    break;
                case Grid8Direction.BottomLeft:
                    start = new GridPolarCoordinates(Math.PI, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.5, _radius);
                    break;
                case Grid8Direction.Bottom:
                    start = new GridPolarCoordinates(Math.PI * 1.25, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.75, _radius);
                    break;
                case Grid8Direction.BottomRight:
                    start = new GridPolarCoordinates(Math.PI * 1.5, _radius);
                    end = new GridPolarCoordinates(Math.PI * 2, _radius);
                    break;
                case Grid8Direction.Right:
                    start = new GridPolarCoordinates(Math.PI * 1.75, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.25, _radius);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var y = BoundingBox.MinY; y < BoundingBox.MaxYExcl; y++)
            {
                for (var x = BoundingBox.MinX; x < BoundingBox.MaxXExcl; x++)
                {
                    if (_origin.EuclideanDistance(x, y) > _radius) continue;

                    var polar = GridPolarCoordinates.FromGridCartesian(x - _origin.X, y - _origin.Y);

                    if (end.Theta > start.Theta)
                    {
                        if (polar.Theta < start.Theta || polar.Theta > end.Theta) continue;
                    }
                    else
                    {
                        if (polar.Theta < start.Theta && polar.Theta > end.Theta) continue;
                    }
                    
                    _containedCoordinates.Add(new GridCoordinatePair(x, y));
                }
            }
        }
    }
}