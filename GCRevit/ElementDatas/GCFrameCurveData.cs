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

    public class GCFrameCurveData : AGCElementCurveData {

        #region constructors
        public GCFrameCurveData(FamilyInstance elem) {
            var yOffset = elem.LookupParameter(RevitFramingParameterUtil.YOffset).AsDouble();
            var zOffset = elem.LookupParameter(RevitFramingParameterUtil.ZOffset).AsDouble();
            SetUpStartAndEndPoints(elem, yOffset, zOffset);
        }

        private void SetUpStartAndEndPoints(FamilyInstance elem, double yOffset, double zOffset) {
            var crv = ((LocationCurve)elem.Location).Curve;
            var strPt = crv.GetEndPoint(0);
            var endPt = crv.GetEndPoint(1);
            var startPoint = new XYZ(strPt.X, strPt.Y + yOffset, strPt.Z + zOffset);
            var endPoint = new XYZ(endPt.X, endPt.Y + yOffset, endPt.Z + zOffset);
            SetUpPointValues(startPoint, endPoint);
        }
        #endregion
    }
}
