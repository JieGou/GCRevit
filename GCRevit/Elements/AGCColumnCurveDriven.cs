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

    public abstract class AGCColumnCurveDriven : AGCColumnBase {

        #region members
        protected GCColumnCurveData crv;
        #endregion

        #region constructors
        protected AGCColumnCurveDriven(FamilyInstance elem)
            : base(elem) {
            this.crv = new GCColumnCurveData(elem);
        }

        public static bool IsColumnCurveDriven(Element elem) {
            return
                AGCColumnBase.IsColumnBase(elem) &&
                TypicalColumnParametersExist(elem);
        }

        public static bool TypicalColumnParametersExist(Element elem) {
            foreach (var paramName in RevitColumnParameterUtil.StandardColumnParameterNames) {
                if (null == elem.LookupParameter(paramName)) {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region properties
        public GCColumnCurveData GCCurve {
            get { return this.crv; }
        }
        #endregion

        #region override translationmethods
        public override void Move(XYZ transVect) {
            base.Move(transVect);
            this.crv = new GCColumnCurveData(this.RevitInstance);
        }

        public override void MoveTo(XYZ newLoc) {
            base.MoveTo(newLoc);
            this.crv = new GCColumnCurveData(this.RevitInstance);
        }

        public override void Rotate(Line axis, double angle) {
            base.Rotate(axis, angle);
            this.crv = new GCColumnCurveData(this.RevitInstance);
        }
        #endregion
    }
}
