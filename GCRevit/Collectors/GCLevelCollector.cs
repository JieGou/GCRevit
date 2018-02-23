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

namespace GCRevit.Collectors {

    public class GCLevelClosestElevationComparer : IComparer<GCLevel> {
        private double tarElev;

        public GCLevelClosestElevationComparer(double tarElev) {
            this.tarElev = tarElev;
        }

        public int Compare(GCLevel x, GCLevel y) {
            if (null == x && null == y) { return 0; }
            if (null == x) { return 1; }
            if (null == y) { return -1; }
            var xComp = Math.Abs(x.RevitLevel.Elevation - tarElev);
            var yComp = Math.Abs(y.RevitLevel.Elevation - tarElev);
            return xComp.CompareTo(yComp);
        }
    }

    public class GCLevelElevationComparer : IComparer<GCLevel> {

        public int Compare(GCLevel x, GCLevel y) {
            if (null == x && null == y) { return 0; }
            if (null == x) { return 1; }
            if (null == y) { return -1; }
            return x.RevitLevel.Elevation.CompareTo(y.RevitLevel.Elevation);
        }
    }

    public static class GCLevelCollector {

        public static IEnumerable<GCLevel> CollectAllLevels(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCLevel>(doc, BuiltInCategory.OST_Levels, GCLevel.IsLevel, GCLevel.CreateGCLevel);
        }

        public static IEnumerable<GCLevel> CollectSelectedLevels(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCLevel>(doc, GCLevel.IsLevel, GCLevel.CreateGCLevel);
        }

        public static GCLevel GetLevelClosestTo(GCRevitDocument doc, double elev) {
            var levs = Enumerable.ToList<GCLevel>(CollectAllLevels(doc));
            levs.Sort(new GCLevelClosestElevationComparer(elev));
            return levs[0];
        }
    }
}
