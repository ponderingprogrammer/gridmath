﻿using System.Collections.Generic;
using System.Linq;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// Utility class for operations on collections of GriBoundingBoxes
    /// </summary>
    public static class GridBoundingBoxes
    {
        public static List<List<int>> FindOverlappingBoxes(GridBoundingBox[] boxes)
        {
            var listOfOverlapLists = new List<List<int>>();
            
            var xOverlapGroups =
                GridIntervals.FindOverlappingIntervals(boxes.Select(box => box.XInterval).ToArray());
            var yOverlapGroups =
                GridIntervals.FindOverlappingIntervals(boxes.Select(box => box.YInterval).ToArray());
            
            // is there a list on x lists which has the same two or more indices that some y list
            foreach (var xOverlapList in xOverlapGroups)
            {
                foreach (var yOverlapList in yOverlapGroups)
                {
                    var commons = xOverlapList.Where(yOverlapList.Contains).ToArray();
                    if (commons.Length > 1)
                    {
                        var overlappingBoxes = new List<int>();
                        for (var i = 0; i < commons.Length; i++)
                        {
                            overlappingBoxes.Add(commons[i]);
                        }

                        listOfOverlapLists.Add(overlappingBoxes);
                    }
                }
            }

            return listOfOverlapLists;
        }

        public static GridCoordinatePair FindCenterOfMass(GridBoundingBox[] boxes)
        {
            var xCenter = GridIntervals.FindCenterOfMass(boxes.Select(b => b.XInterval).ToArray());
            var yCenter = GridIntervals.FindCenterOfMass(boxes.Select(b => b.YInterval).ToArray());
            return new GridCoordinatePair(xCenter, yCenter);
        }
    }
}