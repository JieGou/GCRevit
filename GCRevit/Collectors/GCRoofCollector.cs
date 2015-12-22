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

    public static class GCRoofCollector {

        public static IEnumerable<AGCRoofBase> CollectAllRoofSystems(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCRoofExtrusion.IsRoofExtrusion, 
                GCRoofFootPrint.IsRoofFootPrint 
            };
            List<GCElementCollector.CreateElement<AGCRoofBase>> createElem = new List<GCElementCollector.CreateElement<AGCRoofBase>>() { 
                GCRoofExtrusion.CreateGCRoofExtrusion, 
                GCRoofFootPrint.CreateGCRoofFootPrint 
            };
            return GCElementCollector.CollectAllElementsOfCategory<AGCRoofBase>(doc, BuiltInCategory.OST_Roofs, isElem, createElem);
        }

        public static IEnumerable<AGCRoofBase> CollectSelectedRoofSystems(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCRoofExtrusion.IsRoofExtrusion, 
                GCRoofFootPrint.IsRoofFootPrint 
            };
            List<GCElementCollector.CreateElement<AGCRoofBase>> createElem = new List<GCElementCollector.CreateElement<AGCRoofBase>>() { 
                GCRoofExtrusion.CreateGCRoofExtrusion, 
                GCRoofFootPrint.CreateGCRoofFootPrint 
            };
            return GCElementCollector.CollectSelectedElements<AGCRoofBase>(doc, isElem, createElem);
        }

        public static IEnumerable<GCRoofExtrusion> CollectAllRoofExtrusions(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCRoofExtrusion>(doc, BuiltInCategory.OST_Roofs, GCRoofExtrusion.IsRoofExtrusion, GCRoofExtrusion.CreateGCRoofExtrusion);
        }

        public static IEnumerable<GCRoofExtrusion> CollectSelectedRoofExtrustions(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCRoofExtrusion>(doc, GCRoofExtrusion.IsRoofExtrusion, GCRoofExtrusion.CreateGCRoofExtrusion);
        }

        public static IEnumerable<GCRoofFootPrint> CollectAllRoofFootPrints(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCRoofFootPrint>(doc, BuiltInCategory.OST_Roofs, GCRoofFootPrint.IsRoofFootPrint, GCRoofFootPrint.CreateGCRoofFootPrint);
        }

        public static IEnumerable<GCRoofFootPrint> CollectSelectedRoofFootPrints(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCRoofFootPrint>(doc, GCRoofFootPrint.IsRoofFootPrint, GCRoofFootPrint.CreateGCRoofFootPrint);
        }

        public static IEnumerable<GCRoofInPlace> CollectAllRoofsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCRoofInPlace>(doc, BuiltInCategory.OST_Roofs, GCRoofInPlace.IsRoofInPlace, GCRoofInPlace.CreateGCRoofInPlace);
        }

        public static IEnumerable<GCRoofInPlace> CollectSelectedRoofsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCRoofInPlace>(doc, GCRoofInPlace.IsRoofInPlace, GCRoofInPlace.CreateGCRoofInPlace);
        }
    }
}
