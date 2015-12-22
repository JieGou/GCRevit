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

    public static class GCInstanceCollector {

        public static IEnumerable<GCInstance> CollectAllInstances(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfClass<GCInstance>(doc, typeof(FamilyInstance), GCInstance.IsInstance, GCInstance.CreateGCInstance);
        }

        public static IEnumerable<GCInstance> CollectSelectedInstances(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCInstance>(doc, GCInstance.IsInstance, GCInstance.CreateGCInstance);
        }
    }
}
