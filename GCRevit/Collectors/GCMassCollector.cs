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
    
    public static class GCMassCollector {

        public static IEnumerable<GCMass> CollectAllLevels(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCMass>(doc, BuiltInCategory.OST_Mass, GCMass.IsMass, GCMass.CreateGCMass);
        }

        public static IEnumerable<GCMass> CollectSelectedLevels(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCMass>(doc, GCMass.IsMass, GCMass.CreateGCMass);
        }
    }
}
