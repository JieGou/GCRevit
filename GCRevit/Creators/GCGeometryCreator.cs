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
            Plane plane = CreatePlaneFromCurve(doc, crv);
            SketchPlane skPlane = SketchPlane.Create(doc.Document, plane);
            return doc.Document.Create.NewModelCurve(crv, skPlane);
        }
        #endregion

        #region plane methods
        public static Plane CreatePlaneFromCurve(GCRevitDocument doc, Curve crv) {
            if (crv is Line) {
                XYZ norm = CalculateArbitraryNormalDirectionOfCurveForLine(crv as Line);
                return doc.Application.Create.NewPlane(norm, crv.GetEndPoint(0));
            } else {
                CurveArray crvArr = doc.Application.Create.NewCurveArray();
                crvArr.Append(crv);
                crvArr.Append(Line.CreateBound(crv.GetEndPoint(1), crv.GetEndPoint(0)));
                return doc.Application.Create.NewPlane(crvArr);
            }
        }

        static XYZ CalculateArbitraryNormalDirectionOfCurveForLine(Line crv) {
            XYZ strPt = crv.GetEndPoint(0);
            XYZ endPt = crv.GetEndPoint(1);
            XYZ ptOnPlane = (Math.Abs(strPt.Y - endPt.Y) < GeometryUtil.DoubleComp ) ? 
                new XYZ(endPt.X, endPt.Y + 1.0, endPt.Z) :
                new XYZ(endPt.X + 1.0, endPt.Y, endPt.Z);
            XYZ ptOnPlaneDir = ptOnPlane - strPt;
            return (endPt - strPt).CrossProduct(ptOnPlaneDir);
        }
        #endregion
    }
}
