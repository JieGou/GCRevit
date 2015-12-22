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

    public static class GCFrameCollector {

        public static IEnumerable<AGCFrameCurveDriven> CollectAllCurveDrivenFraming(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCFrameBeam.IsBeam, 
                GCFrameBrace.IsBrace
            };
            List<GCElementCollector.CreateElement<AGCFrameCurveDriven>> createElem = new List<GCElementCollector.CreateElement<AGCFrameCurveDriven>>() { 
                GCFrameBeam.CreateGCFrameBeam, 
                GCFrameBrace.CreateGCFrameBrace 
            };
            return GCElementCollector.CollectAllElementsOfCategory<AGCFrameCurveDriven>(doc, BuiltInCategory.OST_StructuralFraming, isElem, createElem);
        }

        public static IEnumerable<AGCFrameCurveDriven> CollectSelectedCurveDrivenFraming(GCRevitDocument doc) {
            List<GCElementCollector.IsElement> isElem = new List<GCElementCollector.IsElement> { 
                GCFrameBeam.IsBeam, 
                GCFrameBrace.IsBrace 
            };
            List<GCElementCollector.CreateElement<AGCFrameCurveDriven>> createElem = new List<GCElementCollector.CreateElement<AGCFrameCurveDriven>>() { 
                GCFrameBeam.CreateGCFrameBeam, 
                GCFrameBrace.CreateGCFrameBrace 
            };
            return GCElementCollector.CollectSelectedElements<AGCFrameCurveDriven>(doc, isElem, createElem);
        }

        public static IEnumerable<GCFrameInPlace> CollectAllInPlaceFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFrameInPlace>(doc, BuiltInCategory.OST_StructuralFraming, GCFrameInPlace.IsFrameInPlace, GCFrameInPlace.CreateGCFrameInPlace);
        }

        public static IEnumerable<GCFrameInPlace> CollectSelectedInPlaceFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFrameInPlace>(doc, GCFrameInPlace.IsFrameInPlace, GCFrameInPlace.CreateGCFrameInPlace);
        }

        public static IEnumerable<GCFrameBeam> CollectAllBeamFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFrameBeam>(doc, BuiltInCategory.OST_StructuralFraming, GCFrameBeam.IsBeam, GCFrameBeam.CreateGCFrameBeam);
        }

        public static IEnumerable<GCFrameBeam> CollectSelectedBeamFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFrameBeam>(doc, GCFrameBeam.IsBeam, GCFrameBeam.CreateGCFrameBeam);
        }

        public static IEnumerable<GCFrameBrace> CollectAllBraceFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCFrameBrace>(doc, BuiltInCategory.OST_StructuralFraming, GCFrameBrace.IsBrace, GCFrameBrace.CreateGCFrameBrace);
        }

        public static IEnumerable<GCFrameBrace> CollectSelectedBraceFraming(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCFrameBrace>(doc, GCFrameBrace.IsBrace, GCFrameBrace.CreateGCFrameBrace);
        }
    }
}
