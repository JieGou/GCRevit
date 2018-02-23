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

namespace GCRevit.ElementDatas {

    public class GCColumnCurveData : AGCElementCurveData {
        
        public GCColumnCurveData(FamilyInstance elem) {
            if (elem.IsSlantedColumn) {
                SetUpSlantedColumn(elem);
            } else {
                SetUpVerticalColumn(elem);
            }
        }

        void SetUpSlantedColumn(FamilyInstance elem) {
            var lc = (LocationCurve)elem.Location;
            var c = lc.Curve;
            var startPoint = (c.GetEndPoint(0).Z < c.GetEndPoint(1).Z) ? c.GetEndPoint(0) : c.GetEndPoint(1);
            var endPoint = (c.GetEndPoint(0).Z < c.GetEndPoint(1).Z) ? c.GetEndPoint(1) : c.GetEndPoint(0);
            SetUpPointValues(startPoint, endPoint);
        }

        void SetUpVerticalColumn(FamilyInstance elem) {
            var lp = (LocationPoint)elem.Location;
            var pt = lp.Point;
            var baseLevel = elem.Document.GetElement(elem.LookupParameter(RevitColumnParameterUtil.BaseLevel).AsElementId()) as Level;
            var topLevel = elem.Document.GetElement(elem.LookupParameter(RevitColumnParameterUtil.TopLevel).AsElementId()) as Level;
            var baseOffset = elem.LookupParameter(RevitColumnParameterUtil.BaseOffset).AsDouble();
            var topOffset = elem.LookupParameter(RevitColumnParameterUtil.TopOffset).AsDouble();
            var startPoint = new XYZ(pt.X, pt.Y, baseLevel.Elevation + baseOffset);
            var endPoint = new XYZ(pt.X, pt.Y, topLevel.Elevation + topOffset);
            SetUpPointValues(startPoint, endPoint);
        }
    }
}
