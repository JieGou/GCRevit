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
 
    public static class GCFloorCollector {

        public static IEnumerable<GCFloor> CollectAllFloors(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFloor>(doc, BuiltInCategory.OST_Floors, GCFloor.IsFloor, GCFloor.CreateGCFloor);
        }

        public static IEnumerable<GCFloor> CollectSelectedFloors(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFloor>(doc, GCFloor.IsFloor, GCFloor.CreateGCFloor);
        }

        public static IEnumerable<GCFloorInPlace> CollectAllFloorsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFloorInPlace>(doc, BuiltInCategory.OST_Floors, GCFloorInPlace.IsFloorInPlace, GCFloorInPlace.CreateGCFloorInPlace);
        }

        public static IEnumerable<GCFloorInPlace> CollectSelectedFloorsInPlace(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFloorInPlace>(doc, GCFloorInPlace.IsFloorInPlace, GCFloorInPlace.CreateGCFloorInPlace);
        }
    }
}