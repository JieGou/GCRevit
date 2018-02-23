//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using GCRevit.Collectors;
using GCRevit.Elements;
using System;
using System.Collections.Generic;

namespace GCRevit.Creators {

    public static class GCFrameCreator {

        #region beam methods
        public static GCFrameBeam CreateBeam(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, GCLevel lev) {
            var inst = CreateFrameByStructuralType(doc, p1, p2, sym, lev, StructuralType.Beam);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBeam(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym) {
            var inst = CreateFrameByStructuralType(doc, p1, p2, sym, StructuralType.Beam);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBeam(GCRevitDocument doc, Curve crv, FamilySymbol sym, GCLevel lev) {
            var inst = CreateFrameByStructuralType(doc, crv, sym, lev, StructuralType.Beam);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBeam(GCRevitDocument doc, Curve crv, FamilySymbol sym) {
            var inst = CreateFrameByStructuralType(doc, crv, sym, StructuralType.Beam);
            return new GCFrameBeam(inst);
        }
        #endregion

        #region brace methods
        public static GCFrameBeam CreateBrace(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, GCLevel lev) {
            var inst = CreateFrameByStructuralType(doc, p1, p2, sym, lev, StructuralType.Brace);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBrace(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym) {
            var inst = CreateFrameByStructuralType(doc, p1, p2, sym, StructuralType.Brace);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBrace(GCRevitDocument doc, Curve crv, FamilySymbol sym, GCLevel lev) {
            var inst = CreateFrameByStructuralType(doc, crv, sym, lev, StructuralType.Brace);
            return new GCFrameBeam(inst);
        }

        public static GCFrameBeam CreateBrace(GCRevitDocument doc, Curve crv, FamilySymbol sym) {
            var inst = CreateFrameByStructuralType(doc, crv, sym, StructuralType.Brace);
            return new GCFrameBeam(inst);
        }
        #endregion

        #region common methods
        internal static FamilyInstance CreateFrameByStructuralType(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, GCLevel lev, StructuralType type) {
            var line = Line.CreateBound(p1, p2);
            return CreateFrameByStructuralType(doc, line, sym, type);
        }

        internal static FamilyInstance CreateFrameByStructuralType(GCRevitDocument doc, XYZ p1, XYZ p2, FamilySymbol sym, StructuralType type) {
            var line = Line.CreateBound(p1, p2);
            var lev = GCLevelCollector.GetLevelClosestTo(doc, line.GetEndPoint(0).Z);
            return CreateFrameByStructuralType(doc, line, sym, type);
        }

        internal static FamilyInstance CreateFrameByStructuralType(GCRevitDocument doc, Curve crv, FamilySymbol sym, GCLevel lev, StructuralType type) {
            if (!sym.IsActive) {
                sym.Activate();
            }
            return doc.Document.Create.NewFamilyInstance(crv, sym, lev.RevitLevel, type);
        }

        internal static FamilyInstance CreateFrameByStructuralType(GCRevitDocument doc, Curve crv, FamilySymbol sym, StructuralType type) {
            if (!sym.IsActive) {
                sym.Activate();
            }
            var lev = GCLevelCollector.GetLevelClosestTo(doc, crv.GetEndPoint(0).Z);
            return doc.Document.Create.NewFamilyInstance(crv, sym, lev.RevitLevel, type);
        }
        #endregion
    }
}
