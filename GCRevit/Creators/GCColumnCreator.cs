//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using GCRevit.Elements;
using System;
using System.Collections.Generic;

namespace GCRevit.Creators {

    public static class GCColumnCreator {

        #region vertical column methods
        public static GCColumnVertical CreateVerticalColumn(GCRevitDocument doc, XYZ p1, double height, FamilySymbol sym, GCLevel lev) {
            var p2 = new XYZ(p1.X, p1.Y, p1.Z + height);
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, p1, p2, sym, lev, StructuralType.Column);
            return GCColumnVertical.CreateGCColumnVertical(inst);
        }

        public static GCColumnVertical CreateVerticalColumn(GCRevitDocument doc, XYZ p1, double height, FamilySymbol sym) {
            var p2 = new XYZ(p1.X, p1.Y, p1.Z + height);
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, p1, p2, sym, StructuralType.Column);
            return GCColumnVertical.CreateGCColumnVertical(inst);
        }
        #endregion

        #region slanted column methods
        public static GCColumnSlanted CreateSlantedColumn(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, GCLevel lev) {
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, p1, p2, sym, lev, StructuralType.Column);
            return GCColumnSlanted.CreateGCColumnSlanted(inst);
        }

        public static GCColumnSlanted CreateBrace(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym) {
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, p1, p2, sym, StructuralType.Column);
            return GCColumnSlanted.CreateGCColumnSlanted(inst);
        }

        public static GCColumnSlanted CreateBrace(GCRevitDocument doc, Curve crv, FamilySymbol sym, GCLevel lev) {
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, crv, sym, lev, StructuralType.Column);
            return GCColumnSlanted.CreateGCColumnSlanted(inst);
        }

        public static GCColumnSlanted CreateBrace(GCRevitDocument doc, Curve crv, FamilySymbol sym) {
            var inst = GCFrameCreator.CreateFrameByStructuralType(doc, crv, sym, StructuralType.Column);
            return GCColumnSlanted.CreateGCColumnSlanted(inst);
        }
        #endregion
    }
}
