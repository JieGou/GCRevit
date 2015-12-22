//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using System;

namespace GCRevit.ElementDatas {

    public abstract class AGCElementCurveData {

        #region members
        protected XYZ strPt;
        protected XYZ endPt;
        protected XYZ midPt;
        protected XYZ dirVt;
        #endregion

        #region constructors
        public AGCElementCurveData() {
            this.strPt = null;
            this.endPt = null;
        }

        public AGCElementCurveData(XYZ strPt, XYZ endPt) {
            SetUpPointValues(strPt, endPt);
        }

        protected void SetUpPointValues(XYZ strPt, XYZ endPt) {
            this.strPt = strPt;
            this.endPt = endPt;
        }
        #endregion

        #region properties
        public XYZ StartPoint {
            get { return this.strPt; }
        }

        public XYZ EndPoint {
            get { return this.endPt; }
        }

        public XYZ MidPoint {
            get {
                return new XYZ((this.strPt.X + this.endPt.X) / 2.0,
                               (this.strPt.Y + this.endPt.Y) / 2.0,
                               (this.strPt.Z + this.endPt.Z) / 2.0);
            }
        }

        public XYZ DirectionalVector {
            get { return this.endPt - this.strPt; }
        }

        public double EndToEndLength {
            get { return this.strPt.DistanceTo(this.endPt); }
        }
        #endregion
    }
}
