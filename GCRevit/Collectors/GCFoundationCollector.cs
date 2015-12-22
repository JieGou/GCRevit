//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Collectors {

    public static class GCFoundationCollector {

        public static IEnumerable<GCFoundationFloor> CollectAllFoundationFloors(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFoundationFloor>(doc, BuiltInCategory.OST_StructuralFoundation, GCFoundationFloor.IsFoundationFloor, GCFoundationFloor.CreateGCFoundationFloor);
        }

        public static IEnumerable<GCFoundationFloor> CollectSelectedFoundationFloors(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFoundationFloor>(doc, GCFoundationFloor.IsFoundationFloor, GCFoundationFloor.CreateGCFoundationFloor);
        }

        public static IEnumerable<GCFoundationInPlace> CollectAllFoundationInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFoundationInPlace>(doc, BuiltInCategory.OST_StructuralFoundation, GCFoundationInPlace.IsFoundationInPlace, GCFoundationInPlace.CreateGCFoundationInPlace);
        }

        public static IEnumerable<GCFoundationInPlace> CollectSelectedFoundationInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFoundationInPlace>(doc, GCFoundationInPlace.IsFoundationInPlace, GCFoundationInPlace.CreateGCFoundationInPlace);
        }

        public static IEnumerable<GCFoundationInstance> CollectAllFoundationInstance(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFoundationInstance>(doc, BuiltInCategory.OST_StructuralFoundation, GCFoundationInstance.IsFoundationInstance, GCFoundationInstance.CreateGCFoundationInstance);
        }

        public static IEnumerable<GCFoundationInstance> CollectSelectedFoundationInstance(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFoundationInstance>(doc, GCFoundationInstance.IsFoundationInstance, GCFoundationInstance.CreateGCFoundationInstance);
        }
    }
}
