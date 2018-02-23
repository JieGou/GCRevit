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

namespace GCRevit.Creators {

    public static class GCViewCreator {

        #region members
        public static string StandardSectionViewFamilyTypeName = "Building Section";
        #endregion

        #region creators
        public static GCViewLive CreateSectionAtFaceParameter(GCRevitDocument doc, Face face, UV param, double viewCropDim, double viewDepth) {
            var viewTypeId = GetSectionType(doc, StandardSectionViewFamilyTypeName).Id;
            var boundBox = GenerateBoundingBoxFromFaceAndParameter(face, param, viewCropDim, viewDepth);
            return GCViewLive.CreateGCViewLive(ViewSection.CreateSection(doc.Document, viewTypeId, boundBox));
        }

        public static BoundingBoxXYZ GenerateBoundingBoxFromFaceAndParameter(Face face, UV param, double viewCropDim, double viewDepth) {
            var faceDeriv = face.ComputeDerivatives(param);
            var faceNormal = face.ComputeNormal(param);
            var facePoint = face.Evaluate(param);
            return GenerateBoundingBoxFromFaceData(facePoint, faceNormal, faceDeriv, viewCropDim, viewDepth);
        }

        public static BoundingBoxXYZ GenerateBoundingBoxFromFaceData(XYZ facePoint, XYZ faceNormal, Transform faceDeriv, double viewCropDim, double viewDepth) {
            var bb = new BoundingBoxXYZ {
                Enabled = true,
                Max = new XYZ(viewCropDim, viewCropDim, 0),
                Min = new XYZ(-viewCropDim, -viewCropDim, -viewDepth),
                Transform = SectionTransform(facePoint, faceNormal.Negate(), faceDeriv.BasisY, faceDeriv.BasisX.Negate())
            };
            return bb;
        }

        public static Transform SectionTransform(XYZ origin, XYZ viewDir, XYZ upDir, XYZ rightDir) {
            var tran = Transform.Identity;
            tran.Origin = origin;
            tran.BasisX = rightDir;
            tran.BasisY = upDir;
            tran.BasisZ = viewDir;
            return tran;
        }
        #endregion

        #region view family types
        public static List<ViewFamilyType> GetLoadedViewFamilyTypes(GCRevitDocument doc) {
            var viewTypes = new List<ViewFamilyType>();
            var fec = new FilteredElementCollector(doc.Document);
            fec.OfClass(typeof(ViewFamilyType));
            foreach (var e in fec.ToElements()) {
                var ft = e as ViewFamilyType;
                if (null != ft) {
                    viewTypes.Add(ft);
                }
            }
            return viewTypes;
        }

        public static ViewFamilyType GetSectionType(GCRevitDocument doc, string name) {
            foreach (var ft in GetLoadedViewFamilyTypes(doc)) {
                if (ViewFamily.Section == ft.ViewFamily && ft.Name.Equals(name)) {
                    return ft;
                }
            }
            return null;
        }
        #endregion

    }
}
