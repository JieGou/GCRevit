//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.ElementDatas;
using GCRevit.Exceptions;
using System;
using System.Collections.Generic;

namespace GCRevit.Elements {

    public abstract class AGCElement {

        #region members
        protected Element elem;
        protected List<GCFaceUVValues> faceUVVals;
        #endregion

        #region constructors
        protected AGCElement(Element elem) {
            this.elem = elem;
        }

        public virtual void InitializeFaceUVValues() {
            this.faceUVVals = new List<GCFaceUVValues>();
            foreach (var f in this.GeometryFaces()) {
                this.faceUVVals.Add(new GCFaceUVValues(f));
            }
        }
        #endregion

        #region properties
        public Element RevitElement {
            get { return this.elem; }
        }

        public List<GCFaceUVValues> FaceUVValues {
            get { return this.faceUVVals; }
        }
        #endregion

        #region wrapper properties
        public ElementId RevitElementId {
            get { return this.elem.Id; }
        }

        public int RevitElementIdInt {
            get { return this.elem.Id.IntegerValue; }
        }
        #endregion

        #region parameter methods
        public bool DoesParameterExist(string name, out Parameter p) {
            p = this.elem.LookupParameter(name);
            return null == p;
        }

        public Parameter GetParameter(object val) {
            if (val is BuiltInParameter) {
                return this.elem.get_Parameter((BuiltInParameter)val);
            } else if (val is Definition) {
                return this.elem.get_Parameter((Definition)val);
            } else if (val is Guid) {
                return this.elem.get_Parameter((Guid)val);
            } else if (val is string) {
                return this.elem.LookupParameter(val as string);
            } else {
                return null;
            }
        }

        public bool SetParameter(object param, object val) {
            return this.SetParameter(this.GetParameter(param), val);
        }

        public bool SetParameter(Parameter param, object val) {
            switch (param.StorageType) {
                case StorageType.Double:
                    return this.SetDoubleParameter(param, val);
                case StorageType.ElementId:
                    return this.SetElementIdParameter(param, val);
                case StorageType.Integer:
                    return this.SetIntegerParameter(param, val);
                case StorageType.String:
                    return param.Set(val.ToString());
                default:
                    throw new GCElementParameterException(param.StorageType, "Parameter is unsettable.");
            }
        }

        bool SetDoubleParameter(Parameter param, object val) {
            if (val is double || val is int) {
                return param.Set((double)val);
            } else {
                double dVal;
                if (Double.TryParse(val.ToString(), out dVal)) {
                    return param.Set(dVal);
                } else {
                    throw new GCElementParameterException(param.StorageType);
                }
            }
        }

        bool SetElementIdParameter(Parameter param, object val) {
            if (val is ElementId) {
                return param.Set(val as ElementId);
            } else if (val is int) {
                return param.Set(new ElementId((int)val));
            } else {
                throw new GCElementParameterException(param.StorageType);
            }
        }

        bool SetIntegerParameter(Parameter param, object val) {
            if (val is int) {
                return param.Set((int)val);
            } else {
                int iVal;
                if (Int32.TryParse(val.ToString(), out iVal)) {
                    return param.Set(iVal);
                } else {
                    throw new GCElementParameterException(param.StorageType);
                }
            }
        }
        #endregion

        #region translation methods
        public virtual void Move(XYZ transVect) {
            this.elem.Location.Move(transVect);
        }

        public virtual void MoveTo(XYZ newLoc) {
            var elemPt = (this.elem.Location is LocationPoint) ?
                (this.elem.Location as LocationPoint).Point :
                (this.elem.Location as LocationCurve).Curve.Evaluate(0.5, true);
            var vec = new XYZ(newLoc.X - elemPt.X, newLoc.Y - elemPt.Y, newLoc.Z - elemPt.Z);
            this.Move(vec);
        }

        public virtual void Rotate(Line axis, double angle) {
            this.elem.Location.Rotate(axis, angle);
        }
        #endregion

        #region solid methods
        public virtual IEnumerable<Solid> GeometrySolids() {
            return this.GeometrySolids(ViewDetailLevel.Fine);
        }

        public virtual IEnumerable<Solid> GeometrySolids(View view) {
            var opts = new Options();
            opts.View = view;
            opts.DetailLevel = view.DetailLevel;
            return this.GeometrySolids(opts);
        }

        public virtual IEnumerable<Solid> GeometrySolids(ViewDetailLevel detLev) {
            var opts = new Options();
            opts.DetailLevel = detLev;
            return this.GeometrySolids(opts);
        }

        public virtual IEnumerable<Solid> GeometrySolids(Options opts) {
            foreach (var go in this.elem.get_Geometry(opts)) {
                var gs = go as Solid;
                if (null != gs && 0 < gs.Faces.Size) {
                    yield return gs;
                }
            }
        }

        public double TotalVolume() {
            var vol = 0.0;
            foreach (var s in this.GeometrySolids()) {
                vol += s.Volume;
            }
            return vol;
        }

        public double TotalSurfaceArea() {
            var area = 0.0;
            foreach (var f in this.GeometryFaces()) {
                area += f.Area;
            }
            return area;
        }
        #endregion

        #region face methods
        public virtual IEnumerable<Face> GeometryFaces() {
            return this.GeometryFaces(this.GeometrySolids());
        }

        public virtual IEnumerable<Face> GeometryFaces(IEnumerable<Solid> sols) {
            foreach (var sol in sols) {
                foreach (Face f in sol.Faces) {
                    if (null != f) {
                        yield return f;
                    }
                }
            }
        }

        public virtual IEnumerable<Face> GeometryFaces(Solid sol) {
            foreach (Face f in sol.Faces) {
                if (null != f) {
                    yield return f;
                }
            }
        }
        #endregion

        #region edge methods
        public virtual IEnumerable<Edge> GeometryEdges(bool keepDuplicates) {
            return this.GeometryEdges(this.GeometryFaces(), keepDuplicates);
        }

        public virtual IEnumerable<Edge> GeometryEdges(IEnumerable<Face> faces, bool keepDuplicates) {
            var edges = new List<Edge>();
            foreach (var f in faces) {
                foreach (EdgeArray ea in f.EdgeLoops) {
                    foreach (Edge e in ea) {
                        if (keepDuplicates) {
                            edges.Add(e);
                        } else {
                            if (!edges.Contains(e)) {
                                edges.Add(e);
                            }
                        }
                    }
                }
            }
            return edges;
        }
        #endregion
    }
}