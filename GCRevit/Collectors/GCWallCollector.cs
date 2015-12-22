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

    public static class GCWallCollector {

        public static IEnumerable<GCWall> CollectAllWalls(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCWall>(doc, BuiltInCategory.OST_Walls, GCWall.IsWall, GCWall.CreateGCWall);
        }

        public static IEnumerable<GCWall> CollectSelectedWalls(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCWall>(doc, GCWall.IsWall, GCWall.CreateGCWall);
        }

        public static IEnumerable<GCWallInPlace> CollectAllWallsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCWallInPlace>(doc, BuiltInCategory.OST_Walls, GCWallInPlace.IsWallInPlace, GCWallInPlace.CreateGCWallInPlace);
        }

        public static IEnumerable<GCWallInPlace> CollectSelectedWallsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCWallInPlace>(doc, GCWallInPlace.IsWallInPlace, GCWallInPlace.CreateGCWallInPlace);
        }

        public static IEnumerable<GCWallCurtain> CollectAllWallCurtains(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCWallCurtain>(doc, BuiltInCategory.OST_Curtain_Systems, GCWallCurtain.IsWallCurtain, GCWallCurtain.CreateGCWallCurtain);
        }

        public static IEnumerable<GCWallCurtain> CollectSelectedWallCurtains(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCWallCurtain>(doc, GCWallCurtain.IsWallCurtain, GCWallCurtain.CreateGCWallCurtain);
        }

        public static IEnumerable<GCWallCurtainPanel> CollectAllWallCurtainPanels(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCWallCurtainPanel>(doc, BuiltInCategory.OST_CurtainWallPanels, GCWallCurtainPanel.IsWallCurtainPanel, GCWallCurtainPanel.CreateGCWallCurtainPanel);
        }

        public static IEnumerable<GCWallCurtainPanel> CollectSelectedWallCurtainPanels(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCWallCurtainPanel>(doc, GCWallCurtainPanel.IsWallCurtainPanel, GCWallCurtainPanel.CreateGCWallCurtainPanel);
        }
    }
}
