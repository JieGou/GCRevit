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
            ElementId viewTypeId = GetSectionType(doc, StandardSectionViewFamilyTypeName).Id;
            BoundingBoxXYZ boundBox = GenerateBoundingBoxFromFaceAndParameter(face, param, viewCropDim, viewDepth);
            return GCViewLive.CreateGCViewLive(ViewSection.CreateSection(doc.Document, viewTypeId, boundBox));
        }

        public static BoundingBoxXYZ GenerateBoundingBoxFromFaceAndParameter(Face face, UV param, double viewCropDim, double viewDepth) {
            Transform faceDeriv = face.ComputeDerivatives(param);
            XYZ faceNormal = face.ComputeNormal(param);
            XYZ facePoint = face.Evaluate(param);
            return GenerateBoundingBoxFromFaceData(facePoint, faceNormal, faceDeriv, viewCropDim, viewDepth);
        }

        public static BoundingBoxXYZ GenerateBoundingBoxFromFaceData(XYZ facePoint, XYZ faceNormal, Transform faceDeriv, double viewCropDim, double viewDepth) {
            BoundingBoxXYZ bb = new BoundingBoxXYZ();
            bb.Enabled = true;
            bb.Max = new XYZ(viewCropDim, viewCropDim, 0);
            bb.Min = new XYZ(-viewCropDim, -viewCropDim, -viewDepth);
            bb.Transform = SectionTransform(facePoint, faceNormal.Negate(), faceDeriv.BasisY, faceDeriv.BasisX.Negate());
            return bb;
        }

        public static Transform SectionTransform(XYZ origin, XYZ viewDir, XYZ upDir, XYZ rightDir) {
            Transform tran = Transform.Identity;
            tran.Origin = origin;
            tran.BasisX = rightDir;
            tran.BasisY = upDir;
            tran.BasisZ = viewDir;
            return tran;
        }
        #endregion

        #region view family types
        public static List<ViewFamilyType> GetLoadedViewFamilyTypes(GCRevitDocument doc) {
            List<ViewFamilyType> viewTypes = new List<ViewFamilyType>();
            FilteredElementCollector fec = new FilteredElementCollector(doc.Document);
            fec.OfClass(typeof(ViewFamilyType));
            foreach (Element e in fec.ToElements()) {
                ViewFamilyType ft = e as ViewFamilyType;
                if (null != ft) {
                    viewTypes.Add(ft);
                }
            }
            return viewTypes;
        }

        public static ViewFamilyType GetSectionType(GCRevitDocument doc, string name) {
            foreach (ViewFamilyType ft in GetLoadedViewFamilyTypes(doc)) {
                if (ViewFamily.Section == ft.ViewFamily && ft.Name.Equals(name)) {
                    return ft;
                }
            }
            return null;
        }
        #endregion

    }
}
