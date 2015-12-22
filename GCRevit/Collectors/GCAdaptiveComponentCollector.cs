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

    public static class GCAdaptiveComponentCollector {

        public static IEnumerable<GCAdaptiveComponent> CollectAllAdaptiveComponents(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCAdaptiveComponent>(doc, BuiltInCategory.OST_GenericModel, GCAdaptiveComponent.IsAdaptiveComponent, GCAdaptiveComponent.CreateGCAdaptiveComponent);
        }

        public static IEnumerable<GCAdaptiveComponent> CollectSelectedAdaptiveComponents(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCAdaptiveComponent>(doc, GCAdaptiveComponent.IsAdaptiveComponent, GCAdaptiveComponent.CreateGCAdaptiveComponent);
        }
    }
}
