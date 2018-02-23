//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.ElementDatas;
using GCRevit.Exceptions;
using GCRevit.Utils;
using System;

namespace GCRevit.Elements {

    public abstract class AGCFrameCurveDriven : AGCFrameBase {

        #region members

        private GCFrameCurveData crv;
        #endregion

        #region constructors
        protected AGCFrameCurveDriven(FamilyInstance elem)
            : base(elem) {
            this.crv = new GCFrameCurveData(elem);
        }

        public static bool IsFrameCurveDriven(Element elem) {
            return
                AGCFrameBase.IsFrame(elem) &&
                TypicalFramingParametersExist(elem);
        }

        public static bool TypicalFramingParametersExist(Element elem) {
            foreach (var paramName in RevitFramingParameterUtil.StandardFramingParameterNames) {
                if (null == elem.LookupParameter(paramName)) {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region properties
        public GCFrameCurveData GCCurve {
            get { return this.crv; }
        }
        #endregion

        #region translation methods
        public void MoveStartPoint(XYZ newStr) {
            var locCrv = (this.RevitInstance.Location) as LocationCurve;
            if (locCrv.Curve is Line) {
                SetLocationCurve(Line.CreateBound(newStr, this.crv.EndPoint));
            }
        }

        public void MoveEndPoint(XYZ newEnd) {
            var locCrv = (this.RevitInstance.Location) as LocationCurve;
            if (locCrv.Curve is Line) {
                SetLocationCurve(Line.CreateBound(this.crv.StartPoint, newEnd));
            }
        }

        public void SetLocationCurve(Curve crv) {
            ((this.RevitInstance.Location) as LocationCurve).Curve = crv;
            this.crv = new GCFrameCurveData(this.RevitInstance);
        }
        #endregion

        #region override translation methods
        public override void Move(XYZ transVect) {
            base.Move(transVect);
            this.crv = new GCFrameCurveData(this.RevitInstance);
        }

        public override void MoveTo(XYZ newLoc) {
            base.MoveTo(newLoc);
            this.crv = new GCFrameCurveData(this.RevitInstance);
        }

        public override void Rotate(Line axis, double angle) {
            base.Rotate(axis, angle);
            this.crv = new GCFrameCurveData(this.RevitInstance);
        }
        #endregion
    }
}
