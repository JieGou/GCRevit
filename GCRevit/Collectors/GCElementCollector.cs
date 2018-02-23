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

    public static class GCElementCollector {

        public delegate bool IsElement(Element el);
        public delegate T CreateElement<T>(Element elem);

        public static IEnumerable<T> CollectAllElementsOfCategory<T>(GCRevitDocument doc, BuiltInCategory categ, List<IsElement> IsElem, List<CreateElement<T>> CreateElement) {
            var coll = new FilteredElementCollector(doc.Document).OfCategory(categ);
            foreach (var el in coll.ToElements()) {
                for (var i = 0; i < IsElem.Count; i++) {
                    if (IsElem[i](el)) {
                        yield return CreateElement[i](el);
                    }
                }
            }
        }

        public static IEnumerable<T> CollectAllElementsOfCategory<T>(GCRevitDocument doc, BuiltInCategory categ, IsElement IsElem, CreateElement<T> CreateElement) {
            var coll = new FilteredElementCollector(doc.Document).OfCategory(categ);
            foreach (var el in coll.ToElements()) {
                if (IsElem(el)) {
                    yield return CreateElement(el);
                }
            }
        }

        public static IEnumerable<T> CollectAllElementsOfClass<T>(GCRevitDocument doc, Type type, IsElement IsElem, CreateElement<T> CreateElement) {
            var coll = new FilteredElementCollector(doc.Document).OfClass(type);
            foreach (var el in coll.ToElements()) {
                if (IsElem(el)) {
                    yield return CreateElement(el);
                }
            }
        }

        public static IEnumerable<T> CollectSelectedElements<T>(GCRevitDocument doc, IsElement IsElem, CreateElement<T> CreateElement) {
            foreach (var id in doc.UIDocument.Selection.GetElementIds()) {
                var el = doc.Document.GetElement(id);
                if (IsElem(el)) {
                    yield return CreateElement(el);
                }
            }
        }

        public static IEnumerable<T> CollectSelectedElements<T>(GCRevitDocument doc, List<IsElement> IsElem, List<CreateElement<T>> CreateElement) {
            foreach (var id in doc.UIDocument.Selection.GetElementIds()) {
                var el = doc.Document.GetElement(id);
                for (var i = 0; i < IsElem.Count; i++) {
                    if (IsElem[i](el)) {
                        yield return CreateElement[i](el);
                    }
                }
            }
        }
    }
}
