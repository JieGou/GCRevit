using Autodesk.Revit.DB;
using GCRevit.Elements;
using GCRevit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.ElementDatas {

    public class GCGridIntersection {

        #region members
        AGCGridBase grid1;
        AGCGridBase grid2;
        List<XYZ> intPts;
        #endregion

        #region properties
        public AGCGridBase Grid1 {
            get { return this.grid1; }
        }

        public AGCGridBase Grid2 {
            get { return this.grid2; }
        }

        public List<XYZ> IntersectionPoints {
            get { return this.intPts; }
        }
        #endregion

        #region constructors
        public GCGridIntersection(AGCGridBase grid1, AGCGridBase grid2, List<XYZ> intPts) {
            this.grid1 = grid1;
            this.grid2 = grid2;
            this.intPts = intPts;
        }
        #endregion

        #region static methods
        public static XYZ GetClosestGridIntersection(XYZ refPt, List<GCGridIntersection> gridInts, out GCGridIntersection closGridInt) {
            double closDist = Double.MaxValue;
            XYZ closIntPoint = null;
            closGridInt = null;
            foreach (GCGridIntersection currGridInt in gridInts) {
                foreach (XYZ currIntPoint in currGridInt.IntersectionPoints) {
                    double currDist = GeometryUtil.XYDistance(refPt, currIntPoint);
                    if (closDist > currDist) {
                        closDist = currDist;
                        closIntPoint = currIntPoint;
                        closGridInt = currGridInt;
                    }
                }
            }
            return closIntPoint;
        }

        public static List<GCGridIntersection> GetGridIntersections(List<AGCGridBase> grids) {
            List<string> addedGridCombinations = new List<string>();
            List<GCGridIntersection> gridInts = new List<GCGridIntersection>();
            foreach (AGCGridBase g1 in grids) {
                foreach (AGCGridBase g2 in grids) {
                    if (g1.RevitElementIdInt != g2.RevitElementIdInt && !addedGridCombinations.Contains(String.Format("{0}-{1}", g1.RevitElementIdInt, g2.RevitElementIdInt))) {
                        List<XYZ> ints = GetGridIntesections(g1, g2);
                        if (0 < ints.Count) {
                            gridInts.Add(new GCGridIntersection(g1, g2, ints));
                            addedGridCombinations.Add(String.Format("{0}-{1}", g1.RevitElementIdInt, g2.RevitElementIdInt));
                            addedGridCombinations.Add(String.Format("{0}-{1}", g2.RevitElementIdInt, g1.RevitElementIdInt));
                        }
                    }
                }
            }
            return gridInts;
        }
        
        public static List<XYZ> GetGridIntesections(AGCGridBase grid1, AGCGridBase grid2) {
            if (grid1 is GCGrid && grid2 is GCGrid) {
                return GridToGridIntersections(grid1 as GCGrid, grid2 as GCGrid);
            } else if (grid1 is GCGrid && grid2 is GCGridMulti) {
                return GridToGridIntersections(grid1 as GCGrid, grid2 as GCGridMulti);
            } else if (grid1 is GCGridMulti && grid2 is GCGrid) {
                return GridToGridIntersections(grid2 as GCGrid, grid1 as GCGridMulti);
            } else {
                return GridToGridIntersections(grid1 as GCGridMulti, grid2 as GCGridMulti);
            }
        }

        public static List<XYZ> GridToGridIntersections(GCGrid grid1, GCGrid grid2) {
            Curve g1crv = grid1.RevitGrid.Curve;
            Curve g2crv = grid2.RevitGrid.Curve;
            return GeometryUtil.IntersectionPointsOfCurves(g1crv, g2crv);
        }

        public static List<XYZ> GridToGridIntersections(GCGrid grid1, GCGridMulti grid2) {
            List<XYZ> ints = new List<XYZ>();
            GCRevitDocument doc = GCRevitDocument.DocumentInstance(grid2.RevitElement.Document);
            Curve g1crv = grid1.RevitGrid.Curve;
            foreach (ElementId gridId in grid2.RevitMultiGrid.GetGridIds()) {
                Curve g2crv = (doc.GetElement(gridId) as Grid).Curve;
                ints.AddRange(GeometryUtil.IntersectionPointsOfCurves(g1crv, g2crv));
            }
            return ints;
        }

        public static List<XYZ> GridToGridIntersections(GCGridMulti grid1, GCGridMulti grid2) {
            List<XYZ> ints = new List<XYZ>();
            GCRevitDocument doc = GCRevitDocument.DocumentInstance(grid1.RevitElement.Document);
            foreach (ElementId grid1Id in grid1.RevitMultiGrid.GetGridIds()) {
                foreach (ElementId grid2Id in grid2.RevitMultiGrid.GetGridIds()) {
                    if (grid1Id.IntegerValue != grid2Id.IntegerValue) {
                        Grid g1 = doc.GetElement(grid1Id) as Grid;
                        Grid g2 = doc.GetElement(grid2Id) as Grid;
                        ints.AddRange(GeometryUtil.IntersectionPointsOfCurves(g1.Curve, g2.Curve));
                    }
                }
            }
            return ints;
        }
        #endregion
    }
}
