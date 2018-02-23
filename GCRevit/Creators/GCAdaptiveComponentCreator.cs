//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Elements;
using GCRevit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Creators {

    public static class GCAdaptiveComponentCreator {

        public static GCAdaptiveComponent CreateComponentByPoints(GCRevitDocument doc, FamilySymbol sym, List<XYZ> locPts) {
            using (var subTrans = new SubTransaction(doc.Document)) {
                if (TransactionStatus.Started == subTrans.Start()) {
                    var inst = AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(doc.Document, sym);
                    var comp = new GCAdaptiveComponent(inst);
                    var ids = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(inst);
                    if (ids.Count == locPts.Count) {
                        for (var i = 0; i < ids.Count; i++) {
                            var pt = doc.Document.GetElement(ids[i]);
                            var trans = locPts[i] - (pt.Location as LocationPoint).Point;
                            comp.MovePointById(ids[i], trans);
                        }
                        subTrans.Commit();
                        return comp;
                    } else {
                        subTrans.RollBack();
                        throw new GCElementCreationException("The given point count does not match the instance point count");
                    }
                } else {
                    throw new GCElementCreationException("Could not start subtransaction for adaptive component creation");
                }
            }
        }
    }
}