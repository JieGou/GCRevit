//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GCRevit.Elements;
using System;
using System.Collections.Generic;

namespace GCRevit.Collectors {

    public static class GCColumnCollector {

        public static IEnumerable<AGCColumnCurveDriven> CollectAllCurveDrivenColumns(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCColumnSlanted.IsColumnSlanted, 
                GCColumnVertical.IsColumnVertical 
            };
            List<GCElementCollector.CreateElement<AGCColumnCurveDriven>> createElem = new List<GCElementCollector.CreateElement<AGCColumnCurveDriven>>() { 
                GCColumnSlanted.CreateGCColumnSlanted, 
                GCColumnVertical.CreateGCColumnVertical 
            };
            return GCElementCollector.CollectAllElementsOfCategory<AGCColumnCurveDriven>(doc, BuiltInCategory.OST_StructuralColumns, isElem, createElem);
        }

        public static IEnumerable<AGCColumnCurveDriven> CollectSelectedCurveDrivenColumns(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCColumnSlanted.IsColumnSlanted, 
                GCColumnVertical.IsColumnVertical 
            };
            List<GCElementCollector.CreateElement<AGCColumnCurveDriven>> createElem = new List<GCElementCollector.CreateElement<AGCColumnCurveDriven>>() { 
                GCColumnSlanted.CreateGCColumnSlanted, 
                GCColumnVertical.CreateGCColumnVertical 
            };
            return GCElementCollector.CollectSelectedElements<AGCColumnCurveDriven>(doc, isElem, createElem);
        }

        public static IEnumerable<GCColumnInPlace> CollectAllInPlaceColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCColumnInPlace>(doc, BuiltInCategory.OST_StructuralColumns, GCColumnInPlace.IsColumnInPlace, GCColumnInPlace.CreateGCColumnInPlace);
        }

        public static IEnumerable<GCColumnInPlace> CollectSelectedInPlaceColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCColumnInPlace>(doc, GCColumnInPlace.IsColumnInPlace, GCColumnInPlace.CreateGCColumnInPlace);
        }

        public static IEnumerable<GCColumnSlanted> CollectAllSlantedColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCColumnSlanted>(doc, BuiltInCategory.OST_StructuralColumns, GCColumnSlanted.IsColumnSlanted, GCColumnSlanted.CreateGCColumnSlanted);
        }

        public static IEnumerable<GCColumnSlanted> CollectSelectedSlantedColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCColumnSlanted>(doc, GCColumnSlanted.IsColumnSlanted, GCColumnSlanted.CreateGCColumnSlanted);
        }

        public static IEnumerable<GCColumnVertical> CollectAllVerticalColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCColumnVertical>(doc, BuiltInCategory.OST_StructuralColumns, GCColumnVertical.IsColumnVertical, GCColumnVertical.CreateGCColumnVertical);
        }

        public static IEnumerable<GCColumnVertical> CollectSelectedVerticalColumns(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCColumnVertical>(doc, GCColumnVertical.IsColumnVertical, GCColumnVertical.CreateGCColumnVertical);
        }
    }
}
