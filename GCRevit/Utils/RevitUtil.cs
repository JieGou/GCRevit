//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Utils {

    public static class GeometryUtil {

        public static double DoubleComp = 1E-7;

        public static double XYDistance(XYZ p1, XYZ p2) {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2.0) - Math.Pow(p2.Y - p1.Y, 2.0));
        }

        public static List<XYZ> IntersectionPointsOfCurves(Curve c1, Curve c2) {
            List<XYZ> ints = new List<XYZ>();
            IntersectionResultArray ira;
            SetComparisonResult intResult = c1.Intersect(c2, out ira);
            if (SetComparisonResult.Overlap == intResult && 0 < ira.Size) {
                foreach (IntersectionResult ir in ira) {
                    ints.Add(ir.XYZPoint);
                }
            }
            return ints;
        }
    }

    public static class RevitCategoryUtil {

        public static string ConnectionCategoryName = "Structural Connections";
        public static string FloorCategoryName = "Floors";
        public static string FoundationCategoryName = "Structural Foundations";
        public static string LevelCategoryName = "Levels";
        public static string GridCategoryName = "Grids";
        public static string MassCategoryName = "Mass";
        public static string MultiGridCategoryName = "Multi-segmented Grid";
        public static string StructuralFramingCategoryName = "Structural Framing";
        public static string StructuralColumnCategoryName = "Structural Columns";
        public static string RoofCategoryName = "Roofs";
        public static string ViewCategoryName = "Views";
        public static string WallCategoryName = "Walls";
    }

    public static class RevitColumnParameterUtil {

        #region standard names
        public static string BaseLevel = "Base Level";
        public static string TopLevel = "Top Level";
        public static string BaseOffset = "Base Offset";
        public static string TopOffset = "Top Offset";
        public static string ColumnStyle = "Column Style";
        #endregion

        public static List<string> StandardColumnParameterNames {
            get {
                List<string> datum = new List<string>();
                datum.Add(BaseLevel);
                datum.Add(TopLevel);
                datum.Add(BaseOffset);
                datum.Add(TopOffset);
                datum.Add(ColumnStyle);
                return datum;
            }
        }
    }

    public static class RevitFloorParameterUtil {
        #region standard names
        public static string Level = "Level";
        public static string HeightOffset = "Height Offset From Level";
        #endregion

        public static List<string> StandardFloorParameterNames {
            get {
                List<string> datum = new List<string>();
                datum.Add(Level);
                datum.Add(HeightOffset);
                return datum;
            }
        }
    }

    public static class RevitFoundationParameterUtil {

        #region standard names
        public static string Level = "Level";
        public static string Offset = "Offset";
        public static string ElevationAtBottom = "Elevation at Bottom";
        #endregion

        public static List<string> StandardFoundationParameterNames {
            get {
                List<string> datum = new List<string>();
                datum.Add(Level);
                datum.Add(Offset);
                datum.Add(ElevationAtBottom);
                return datum;
            }
        }
    }

    public static class RevitFramingParameterUtil {

        #region standard names
        public static string ReferenceLevel = "Reference Level";
        public static string StartOffset = "Start Level Offset";
        public static string EndOffset = "End Level Offset";
        public static string YJustification = "y Justification";
        public static string YOffset = "y Offset Value";
        public static string ZJustification = "z Justification";
        public static string ZOffset = "z Offset Value";
        #endregion

        public static List<string> StandardFramingParameterNames {
            get {
                List<string> datum = new List<string>();
                datum.Add(ReferenceLevel);
                datum.Add(StartOffset);
                datum.Add(EndOffset);
                datum.Add(YJustification);
                datum.Add(YOffset);
                datum.Add(ZJustification);
                datum.Add(ZOffset);
                return datum;
            }
        }
    }
}
