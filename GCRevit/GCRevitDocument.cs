//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GCRevit {

    public class GCRevitDocument {

        #region singleton member
        static Dictionary<string, GCRevitDocument> pathDocumentMap;
        #endregion

        #region members
        Application app;
        Document doc;
        UIApplication uiapp;
        UIDocument uidoc;
        #endregion

        #region constructors
        public static GCRevitDocument DocumentInstance(ExternalCommandData commandData) {
            return DocumentInstance(commandData.Application.ActiveUIDocument.Document);
        }

        public static GCRevitDocument DocumentInstance(Document doc) {
            if (null == pathDocumentMap) {
                pathDocumentMap = new Dictionary<string, GCRevitDocument>();
            }
            var path = doc.PathName;
            GCRevitDocument revDoc;
            if (pathDocumentMap.TryGetValue(path, out revDoc)) {
                return revDoc;
            } else {
                revDoc = new GCRevitDocument(doc);
                pathDocumentMap.Add(path, revDoc);
                return revDoc;
            }
        }

        GCRevitDocument(Document doc) {
            this.doc = doc;
            this.uidoc = new UIDocument(this.doc);
            this.app = this.doc.Application;
            this.uiapp = new UIApplication(this.app);
        }
        #endregion

        #region properties
        public Application Application {
            get { return this.app; }
        }

        public Document Document {
            get { return this.doc; }
        }

        public UIApplication UIApplication {
            get { return this.uiapp; }
        }

        public UIDocument UIDocument {
            get { return this.uidoc; }
        }
        #endregion

        #region parameter methods
        public void  CreateProjectParameter(string name, Category cat, ParameterType type, BuiltInParameterGroup group, bool inst) {
            var def = CreateProjectParameterDefinition(name, type);
            BindParameterDefinition(def, cat, group, inst);
        }

        ExternalDefinition CreateProjectParameterDefinition(string name, ParameterType type) {
            var origSharedParamFilePath = app.SharedParametersFilename;
            var tempFilePath = $"{Path.GetTempFileName()}.txt";
            using (File.Create(tempFilePath)) { }
            this.app.SharedParametersFilename = tempFilePath;
#if (REVIT2015)
            ExternalDefinitonCreationOptions opts = new ExternalDefinitonCreationOptions(name, type);
#else
            var opts = new ExternalDefinitionCreationOptions(name, type);
#endif
            opts.Visible = true;
            var def = app.OpenSharedParameterFile().Groups.Create("Temp Parameters").Definitions.Create(opts) as ExternalDefinition;
            app.SharedParametersFilename = origSharedParamFilePath;
            File.Delete(tempFilePath);
            return def;
        }

        void BindParameterDefinition(ExternalDefinition def, Category cat, BuiltInParameterGroup group, bool inst) {
            var catSet = new CategorySet();
            catSet.Insert(cat);
            var bind = (inst) ?
                this.app.Create.NewInstanceBinding(catSet) as Binding :
                this.app.Create.NewTypeBinding(catSet) as Binding;
            this.doc.ParameterBindings.Insert(def, bind, group);
        }
        #endregion

        #region collection methods
        public List<T> GetElementsOfType<T>() {
            return new FilteredElementCollector(this.doc)
                    .OfClass(typeof(T))
                    .Cast<T>()
                    .ToList();
        }

        public List<Element> GetElementsOfCateogry(BuiltInCategory cat) {
            return new FilteredElementCollector(this.doc)
                    .OfCategory(cat)
                    .ToList();
        }
        
        public List<T> GetElementsOfCateogryAndType<T>(BuiltInCategory cat) {
            return new FilteredElementCollector(this.doc)
                    .OfCategory(cat)
                    .OfClass(typeof(T))
                    .Cast<T>()
                    .ToList();
        }

        public Family GetFamiliesOfCateogryAndType(BuiltInCategory cat, string famName) {
            return new FilteredElementCollector(this.doc)
                    .OfClass(typeof(Family))
                    .OfCategory(cat)
                    .Cast<Family>()
                    .Where(s => s.Name.Equals(famName))
                    .FirstOrDefault();
        }

        public FamilySymbol GetFamilySymbolOfCateogryAndType(BuiltInCategory cat, string symName) {
            return new FilteredElementCollector(this.doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(cat)
                    .Cast<FamilySymbol>()
                    .Where(s => s.Name.Equals(symName))
                    .FirstOrDefault();
        }

        public FamilySymbol GetFamilySymbolOfCateogryFamilyAndType(BuiltInCategory cat, string famName, string symName) {
            return new FilteredElementCollector(this.doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(cat)
                    .Cast<FamilySymbol>()
                    .Where(s => s.Family.Name.Equals(symName))
                    .Where(s => s.Name.Equals(symName))
                    .FirstOrDefault();
        }
        #endregion

        #region wrapper methods
        public Element GetElement(ElementId id) {
            return this.doc.GetElement(id);
        }
        #endregion

        #region user prompted selection methods
        public Edge SelectEdgeFromDocument() {
            var edgeRef = this.uidoc.Selection.PickObject(ObjectType.Edge);
            return this.doc.GetElement(edgeRef.ElementId).GetGeometryObjectFromReference(edgeRef) as Edge;
        }

        public Element SelectElementFromDocument() {
            var elemRef = this.uidoc.Selection.PickObject(ObjectType.Element);
            return this.doc.GetElement(elemRef.ElementId);
        }

        public Face SelectFaceFromDocument() {
            var faceRef = this.uidoc.Selection.PickObject(ObjectType.Face);
            return this.doc.GetElement(faceRef.ElementId).GetGeometryObjectFromReference(faceRef) as Face;
        }
        #endregion
    }
}