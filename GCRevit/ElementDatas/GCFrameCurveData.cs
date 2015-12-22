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
            double yOffset = elem.LookupParameter(RevitFramingParameterUtil.YOffset).AsDouble();
            double zOffset = elem.LookupParameter(RevitFramingParameterUtil.ZOffset).AsDouble();
            SetUpStartAndEndPoints(elem, yOffset, zOffset);
        }

        void SetUpStartAndEndPoints(FamilyInstance elem, double yOffset, double zOffset) {
            Curve crv = ((LocationCurve)elem.Location).Curve;
            XYZ strPt = crv.GetEndPoint(0);
            XYZ endPt = crv.GetEndPoint(1);
            XYZ startPoint = new XYZ(strPt.X, strPt.Y + yOffset, strPt.Z + zOffset);
            XYZ endPoint = new XYZ(endPt.X, endPt.Y + yOffset, endPt.Z + zOffset);
            SetUpPointValues(startPoint, endPoint);
        }
        #endregion
    }
}
