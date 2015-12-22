//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Exceptions;
using System;
using System.Collections.Generic;

namespace GCRevit.Elements {

    public abstract class AGCInstance : AGCElement {

        #region constructor
        protected AGCInstance(FamilyInstance elem)
            : base(elem) { }

        public static bool IsInstance(Element elem) {
            return elem is FamilyInstance;
        }
        #endregion

        #region properties
        public FamilyInstance RevitInstance {
            get { return this.elem as FamilyInstance; }
        }
        #endregion

        #region solid methods
        public override IEnumerable<Solid> GeometrySolids(Options opts) {
            foreach (GeometryObject go in this.elem.get_Geometry(opts)) {
                Solid gs = go as Solid;
                GeometryInstance gi = go as GeometryInstance;
                if (null != gs && 0 < gs.Faces.Size) {
                    yield return gs;
                } else if (null != gi) {
                    foreach (GeometryObject gio in gi.GetInstanceGeometry()) {
                        Solid gis = gio as Solid;
                        if (null != gis && 0 < gis.Faces.Size) {
                            yield return gis;
                        }
                    }
                }
            }
        }
        #endregion

        #region symbol methods
        public virtual void ChangeType(FamilySymbol sym) {
            this.RevitInstance.Symbol = sym;
        }
        #endregion
    }
}
