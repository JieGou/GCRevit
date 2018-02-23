//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using GCRevit.ElementDatas;
using GCRevit.Exceptions;
using GCRevit.Utils;
using System;
using System.Collections.Generic;

namespace GCRevit.Elements {

    public class GCViewLive : AGCViewportView {

        #region constructors
        private GCViewLive(View3D view)
            : base(view) { }

        private GCViewLive(ViewPlan view)
            : base(view) { }

        private GCViewLive(ViewSection view)
            : base(view) { }

        public static GCViewLive CreateGCViewLive(Element elem) {
            if (elem is View3D) {
                return new GCViewLive(elem as View3D);
            } else if (elem is ViewPlan) {
                return new GCViewLive(elem as ViewPlan);
            } else if (elem is ViewSection) {
                return new GCViewLive(elem as ViewSection);
            } else {
                throw new GCElementCreationException("Element is not a View3D or ViewPlan or a ViewSection");
            }
        }

        public static bool IsViewLive(Element elem) {
            return 
                AGCViewBase.IsViewBase(elem) &&
                (elem is View3D || elem is ViewPlan || elem is ViewSection);
        }
        #endregion

        #region analysis visualization= methods
        public void LoadAnalysisVisualizationValues(IEnumerable<AGCElement> elems, string visualizationName, string schemeName, IList<string> unitNames, IList<double> unitMultipliers) {
            var numOfMeasurments = GetMaxNumOfMeasurments(elems);
            var sfm = GetSpatialFieldManager(numOfMeasurments);
            var resultSchema = new AnalysisResultSchema(schemeName, visualizationName);
            resultSchema.SetUnits(unitNames, unitMultipliers);
            var schemaIdx = sfm.RegisterResult(resultSchema);
            foreach (var elem in elems) {
                foreach (var faceVals in elem.FaceUVValues) {
                    if (null != faceVals) {
                        var primIdx = sfm.AddSpatialFieldPrimitive(faceVals.Face, Transform.Identity);
                        try {
                            var paramUVs = new List<UV>();
                            var paramUVValsAtPoint = new List<ValueAtPoint>();
                            foreach (var vals in faceVals) {
                                paramUVs.Add(vals.Key);
                                paramUVValsAtPoint.Add(new ValueAtPoint(vals.Value));
                            }
                            var domPts = new FieldDomainPointsByUV(paramUVs);
                            var fieldVals = new FieldValues(paramUVValsAtPoint);
                            sfm.UpdateSpatialFieldPrimitive(primIdx, domPts, fieldVals, schemaIdx);
                        } catch (Exception x) {
                            sfm.RemoveSpatialFieldPrimitive(primIdx);
                            GCLogger.AppendLine("Failed to add face values: {0}, {1}", x.Message, x.StackTrace);
                        }
                    }
                }
            }
        }

        public SpatialFieldManager GetSpatialFieldManager(int numOfMeasurments) {
            var sfm = SpatialFieldManager.GetSpatialFieldManager(this.RevitView);
            if (null == sfm || sfm.NumberOfMeasurements != numOfMeasurments) {
                sfm = SpatialFieldManager.CreateSpatialFieldManager(this.RevitView, numOfMeasurments);
            }
            return sfm;
        }
        
        int GetMaxNumOfMeasurments(IEnumerable<AGCElement> elems) {
            var maxMeasurments = 0;
            foreach (var elem in elems) {
                foreach (var uvVals in elem.FaceUVValues) {
                    if (maxMeasurments < uvVals.Count) {
                        maxMeasurments = uvVals.Count;
                    }
                }
            }
            return maxMeasurments;
        }

        public void CreateAndSetAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            var analysisDisplayStyle = GetAnalysisDisplayStyle(displayStyleName, colors);
            this.RevitView.AnalysisDisplayStyleId = analysisDisplayStyle.Id;
        }

        AnalysisDisplayStyle GetAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            var dispStyleColl = new FilteredElementCollector(this.elem.Document).OfClass(typeof(AnalysisDisplayStyle));
            foreach (AnalysisDisplayStyle style in dispStyleColl.ToElements()) {
                if (style.Name.Equals(displayStyleName)) {
                    return style;
                }
            }
            return CreateAnalysisDisplayStyle(displayStyleName, colors);
        }

        AnalysisDisplayStyle CreateAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            var coloredSurfaceSettings = new AnalysisDisplayColoredSurfaceSettings();
            var colorSettings = CreateAnalysisDisplayColorSettings(colors);
            var legendSettings = new AnalysisDisplayLegendSettings();
            return AnalysisDisplayStyle.CreateAnalysisDisplayStyle(this.elem.Document, displayStyleName, coloredSurfaceSettings, colorSettings, legendSettings);
        }

        AnalysisDisplayColorSettings CreateAnalysisDisplayColorSettings(List<Color> colors) {
            var colorSettings = new AnalysisDisplayColorSettings();
            colorSettings.MinColor = colors[0];
            var interColors = new List<AnalysisDisplayColorEntry>();
            for (var i = 1; i < colors.Count - 1; i++) {
                interColors.Add(new AnalysisDisplayColorEntry(colors[i]));
            }                
            colorSettings.SetIntermediateColors(interColors);
            colorSettings.MaxColor = colors[colors.Count - 1];
            return colorSettings;
        }
        #endregion
    }
}
