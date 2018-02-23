//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Creators {
    
    public static class GCGeometryCreator {

        #region model curve methods
        public static ModelCurve CreateModelCurveFromCurve(GCRevitDocument doc, Curve crv) {
            var plane = CreatePlaneFromCurve(doc, crv);
            var skPlane = SketchPlane.Create(doc.Document, plane);
            return doc.Document.Create.NewModelCurve(crv, skPlane);
        }
        #endregion

        #region plane methods
        public static Plane CreatePlaneFromCurve(GCRevitDocument doc, Curve crv) {
            if (crv is Line) {
                var norm = CalculateArbitraryNormalDirectionOfCurveForLine(crv as Line);
#if (REVIT2015 || REVIT2016 || REVIT2017)
                return doc.Application.Create.NewPlane(norm, crv.GetEndPoint(0));
#else
                return Plane.CreateByNormalAndOrigin(norm, crv.GetEndPoint(0));
#endif
            } else {
                var crvArr = doc.Application.Create.NewCurveArray();
                crvArr.Append(crv);
                crvArr.Append(Line.CreateBound(crv.GetEndPoint(1), crv.GetEndPoint(0)));
#if (REVIT2015 || REVIT2016 || REVIT2017)
                return doc.Application.Create.NewPlane(crvArr);
#else
                var crv1 = crvArr.get_Item(0);
                var crv2 = crvArr.get_Item(crvArr.Size - 1);
                var isStrPtSame = crv1.GetEndPoint(0).IsAlmostEqualTo(crv1.GetEndPoint(1));
                XYZ p0, p1, p2;
                if (isStrPtSame) {
                    p0 = crv1.GetEndPoint(0);
                    p1 = crv1.GetEndPoint(1);
                    p2 = crv2.GetEndPoint(1);
                } else {
                    p0 = crv1.GetEndPoint(1);
                    p1 = crv1.GetEndPoint(0);
                    p2 = (crv1.GetEndPoint(1).IsAlmostEqualTo(crv2.GetEndPoint(0))) ? crv2.GetEndPoint(1) : crv2.GetEndPoint(0);
                }
                return Plane.CreateByThreePoints(p0, p1, p2);
#endif
            }
        }

        static XYZ CalculateArbitraryNormalDirectionOfCurveForLine(Line crv) {
            var strPt = crv.GetEndPoint(0);
            var endPt = crv.GetEndPoint(1);
            var ptOnPlane = (Math.Abs(strPt.Y - endPt.Y) < GeometryUtil.DoubleComp ) ? 
                new XYZ(endPt.X, endPt.Y + 1.0, endPt.Z) :
                new XYZ(endPt.X + 1.0, endPt.Y, endPt.Z);
            var ptOnPlaneDir = ptOnPlane - strPt;
            return (endPt - strPt).CrossProduct(ptOnPlaneDir);
        }
#endregion
    }
}
