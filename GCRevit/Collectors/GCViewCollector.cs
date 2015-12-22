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
    
    public static class GCViewCollector {

        public static IEnumerable<AGCViewBase> GetAllViews(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCViewDrafting.IsViewDrafting, 
                GCViewLegend.IsViewLegend,
                GCViewLive.IsViewLive,
                GCViewSheet.IsViewSheet
            };
            List<GCElementCollector.CreateElement<AGCViewBase>> createElem = new List<GCElementCollector.CreateElement<AGCViewBase>>() { 
                GCViewDrafting.CreateGCViewDrafting, 
                GCViewLegend.CreateGCViewLegend,
                GCViewLive.CreateGCViewLive,
                GCViewSheet.CreateGCViewSheet
            };
            return GCElementCollector.CollectAllElementsOfCategory<AGCViewBase>(doc, BuiltInCategory.OST_Views, isElem, createElem);
        }

        public static IEnumerable<AGCViewBase> GetSelectedViews(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCViewDrafting.IsViewDrafting, 
                GCViewLegend.IsViewLegend,
                GCViewLive.IsViewLive,
                GCViewSheet.IsViewSheet
            };
            List<GCElementCollector.CreateElement<AGCViewBase>> createElem = new List<GCElementCollector.CreateElement<AGCViewBase>>() { 
                GCViewDrafting.CreateGCViewDrafting, 
                GCViewLegend.CreateGCViewLegend,
                GCViewLive.CreateGCViewLive,
                GCViewSheet.CreateGCViewSheet
            };
            return GCElementCollector.CollectSelectedElements<AGCViewBase>(doc, isElem, createElem);
        }

        public static IEnumerable<GCViewDrafting> CollectAllDraftingViews(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCViewDrafting>(doc, BuiltInCategory.OST_Views, GCViewDrafting.IsViewDrafting, GCViewDrafting.CreateGCViewDrafting);
        }

        public static IEnumerable<GCViewDrafting> CollectSelectedDraftingViews(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCViewDrafting>(doc, GCViewDrafting.IsViewDrafting, GCViewDrafting.CreateGCViewDrafting);
        }

        public static IEnumerable<GCViewLegend> CollectAllLegendViews(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCViewLegend>(doc, BuiltInCategory.OST_Views, GCViewLegend.IsViewLegend, GCViewLegend.CreateGCViewLegend);
        }

        public static IEnumerable<GCViewLegend> CollectSelectedLegendViews(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCViewLegend>(doc, GCViewLegend.IsViewLegend, GCViewLegend.CreateGCViewLegend);
        }

        public static IEnumerable<GCViewLive> CollectAllLiveViews(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCViewLive>(doc, BuiltInCategory.OST_Views, GCViewLive.IsViewLive, GCViewLive.CreateGCViewLive);
        }

        public static IEnumerable<GCViewLive> CollectSelectedLiveViews(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCViewLive>(doc, GCViewLive.IsViewLive, GCViewLive.CreateGCViewLive);
        }

        public static IEnumerable<GCViewSheet> CollectAllSheetViews(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCViewSheet>(doc, BuiltInCategory.OST_Views, GCViewSheet.IsViewSheet, GCViewSheet.CreateGCViewSheet);
        }

        public static IEnumerable<GCViewSheet> CollectSelectedSheetViews(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCViewSheet>(doc, GCViewSheet.IsViewSheet, GCViewSheet.CreateGCViewSheet);
        }
    }
}
