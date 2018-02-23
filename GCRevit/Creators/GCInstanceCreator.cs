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

namespace GCRevit.Creators {

    public static class GCInstanceCreator {

        public static GCInstance CreateLineDetailInstance(GCRevitDocument doc, XYZ p1, FamilySymbol sym, View view) {
            if (!sym.IsActive) {
                sym.Activate();
            }
            return GCInstance.CreateGCInstance(doc.Document.Create.NewFamilyInstance(p1, sym, view));
        }

        public static GCInstance CreateLineDetailInstance(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, View view) {
            if (!sym.IsActive) {
                sym.Activate();
            }
            var line = Line.CreateBound(p1, p2);
            return GCInstance.CreateGCInstance(doc.Document.Create.NewFamilyInstance(line, sym, view));
        }

        public static GCInstance CreateLineDetailInstance(GCRevitDocument doc, Line line, FamilySymbol sym, View view) {
            if (!sym.IsActive) {
                sym.Activate();
            }
            return GCInstance.CreateGCInstance(doc.Document.Create.NewFamilyInstance(line, sym, view));
        }
    }
}
