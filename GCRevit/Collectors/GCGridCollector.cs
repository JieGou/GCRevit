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

namespace GCRevit.Collectors {

    public static class GCGridCollector {

        public static IEnumerable<AGCGridBase> CollectAllGridsAndMultiGrids(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCGrid.IsGrid, 
                GCGridMulti.IsGridMulti
            };
            List<GCElementCollector.CreateElement<AGCGridBase>> createElem = new List<GCElementCollector.CreateElement<AGCGridBase>>() { 
                GCGrid.CreateGCGrid, 
                GCGridMulti.CreateGCGridMulti
            };
            return GCElementCollector.CollectAllElementsOfCategory<AGCGridBase>(doc, BuiltInCategory.OST_Grids, isElem, createElem);
        }

        public static IEnumerable<AGCGridBase> CollectSelectedGridsAndMultiGrids(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCGrid.IsGrid, 
                GCGridMulti.IsGridMulti
            };
            List<GCElementCollector.CreateElement<AGCGridBase>> createElem = new List<GCElementCollector.CreateElement<AGCGridBase>>() { 
                GCGrid.CreateGCGrid, 
                GCGridMulti.CreateGCGridMulti
            };
            return GCElementCollector.CollectSelectedElements<AGCGridBase>(doc, isElem, createElem);
        }

        public static IEnumerable<GCGrid> CollectAllGrids(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCGrid>(doc, BuiltInCategory.OST_Grids, GCGrid.IsGrid, GCGrid.CreateGCGrid);
        }

        public static IEnumerable<GCGrid> CollectSelectedGrids(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCGrid>(doc, GCGrid.IsGrid, GCGrid.CreateGCGrid);
        }

        public static IEnumerable<GCGridMulti> CollectAllMultiGrids(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCGridMulti>(doc, BuiltInCategory.OST_Grids, GCGridMulti.IsGridMulti, GCGridMulti.CreateGCGridMulti);
        }

        public static IEnumerable<GCGridMulti> CollectSelectedMultiGrids(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCGridMulti>(doc, GCGridMulti.IsGridMulti, GCGridMulti.CreateGCGridMulti);
        }
    }
}
