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
        public GCViewLive(View3D view)
            : base(view) { }

        public GCViewLive(ViewPlan view)
            : base(view) { }

        public GCViewLive(ViewSection view)
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
            int numOfMeasurments = GetMaxNumOfMeasurments(elems);
            SpatialFieldManager sfm = GetSpatialFieldManager(numOfMeasurments);
            AnalysisResultSchema resultSchema = new AnalysisResultSchema(schemeName, visualizationName);
            resultSchema.SetUnits(unitNames, unitMultipliers);
            int schemaIdx = sfm.RegisterResult(resultSchema);
            foreach (AGCElement elem in elems) {
                foreach (GCFaceUVValues faceVals in elem.FaceUVValues) {
                    if (null != faceVals) {
                        int primIdx = sfm.AddSpatialFieldPrimitive(faceVals.Face, Transform.Identity);
                        try {
                            List<UV> paramUVs = new List<UV>();
                            List<ValueAtPoint> paramUVValsAtPoint = new List<ValueAtPoint>();
                            foreach (KeyValuePair<UV, IList<double>> vals in faceVals) {
                                paramUVs.Add(vals.Key);
                                paramUVValsAtPoint.Add(new ValueAtPoint(vals.Value));
                            }
                            FieldDomainPointsByUV domPts = new FieldDomainPointsByUV(paramUVs);
                            FieldValues fieldVals = new FieldValues(paramUVValsAtPoint);
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
            SpatialFieldManager sfm = SpatialFieldManager.GetSpatialFieldManager(this.RevitView);
            if (null == sfm || sfm.NumberOfMeasurements != numOfMeasurments) {
                sfm = SpatialFieldManager.CreateSpatialFieldManager(this.RevitView, numOfMeasurments);
            }
            return sfm;
        }
        
        int GetMaxNumOfMeasurments(IEnumerable<AGCElement> elems) {
            int maxMeasurments = 0;
            foreach (AGCElement elem in elems) {
                foreach (GCFaceUVValues uvVals in elem.FaceUVValues) {
                    if (maxMeasurments < uvVals.Count) {
                        maxMeasurments = uvVals.Count;
                    }
                }
            }
            return maxMeasurments;
        }

        public void CreateAndSetAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            AnalysisDisplayStyle analysisDisplayStyle = GetAnalysisDisplayStyle(displayStyleName, colors);
            this.RevitView.AnalysisDisplayStyleId = analysisDisplayStyle.Id;
        }

        AnalysisDisplayStyle GetAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            FilteredElementCollector dispStyleColl = new FilteredElementCollector(this.elem.Document).OfClass(typeof(AnalysisDisplayStyle));
            foreach (AnalysisDisplayStyle style in dispStyleColl.ToElements()) {
                if (style.Name.Equals(displayStyleName)) {
                    return style;
                }
            }
            return CreateAnalysisDisplayStyle(displayStyleName, colors);
        }

        AnalysisDisplayStyle CreateAnalysisDisplayStyle(string displayStyleName, List<Color> colors) {
            AnalysisDisplayColoredSurfaceSettings coloredSurfaceSettings = new AnalysisDisplayColoredSurfaceSettings();
            AnalysisDisplayColorSettings colorSettings = CreateAnalysisDisplayColorSettings(colors);
            AnalysisDisplayLegendSettings legendSettings = new AnalysisDisplayLegendSettings();
            return AnalysisDisplayStyle.CreateAnalysisDisplayStyle(this.elem.Document, displayStyleName, coloredSurfaceSettings, colorSettings, legendSettings);
        }

        AnalysisDisplayColorSettings CreateAnalysisDisplayColorSettings(List<Color> colors) {
            AnalysisDisplayColorSettings colorSettings = new AnalysisDisplayColorSettings();
            colorSettings.MinColor = colors[0];
            List<AnalysisDisplayColorEntry> interColors = new List<AnalysisDisplayColorEntry>();
            for (int i = 1; i < colors.Count - 1; i++) {
                interColors.Add(new AnalysisDisplayColorEntry(colors[i]));
            }                
            colorSettings.SetIntermediateColors(interColors);
            colorSettings.MaxColor = colors[colors.Count - 1];
            return colorSettings;
        }
        #endregion
    }
}
