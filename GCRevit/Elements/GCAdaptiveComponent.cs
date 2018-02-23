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

namespace GCRevit.Elements {

    public class GCAdaptiveComponent : GCInstance {

        #region constructors
        protected GCAdaptiveComponent(FamilyInstance elem)
            : base(elem) { }

        public static GCAdaptiveComponent CreateGCAdaptiveComponent(Element elem) {
            return new GCAdaptiveComponent(elem as FamilyInstance);
        }

        public static bool IsAdaptiveComponent(Element elem) {
            return
                GCInstance.IsInstance(elem) &&
                AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance(elem as FamilyInstance);
        }
        #endregion

        #region methods
        public override void Move(XYZ transVect) {
            foreach (var id in AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(this.RevitInstance)) {
                MovePointById(id, transVect);
            }
        }

        public bool MovePoint(int idx, XYZ trans) {
            var ptId = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(this.RevitInstance)[idx];
            return MovePointById(ptId, trans);
        }

        internal bool MovePointById(ElementId id, XYZ trans) {
            var pt = this.RevitElement.Document.GetElement(id) as ReferencePoint;
            ElementTransformUtils.MoveElement(this.RevitElement.Document, id, trans);
            return pt.Location.Move(trans);
        }
        #endregion
    }
}
