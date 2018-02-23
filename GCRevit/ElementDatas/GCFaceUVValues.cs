//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.ElementDatas {
    
    public class GCFaceUVValues : IDictionary<UV, IList<double>> {

        #region members
        Dictionary<UV, IList<double>> vals;
        Face face;
        #endregion

        #region constructors
        public GCFaceUVValues(Face face) {
            this.vals = new Dictionary<UV, IList<double>>();
            this.face = face;
        }
        #endregion

        #region properties
        public Face Face {
            get { return this.face; }
        }
        #endregion

        #region idictionary implemented properties
        public ICollection<UV> Keys {
            get { return this.vals.Keys; }
        }

        public ICollection<IList<double>> Values {
            get { return this.vals.Values; }
        }

        public IList<double> this[UV key] {
            get {
                var matchingKey = GetMatchingKey(key);
                if (null != matchingKey) {
                    return this.vals[matchingKey];
                } else {
                    throw new KeyNotFoundException();
                } 
            }
            set {
                var matchingKey = GetMatchingKey(key);
                if (null != matchingKey) {
                    this.vals[matchingKey] = value;
                } else {
                    throw new KeyNotFoundException();
                } 
            }
        }

        public int Count {
            get { return this.vals.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }
        #endregion

        #region idictionary implemented methods
        public void Add(UV key, IList<double> value) {
            var matchingKey = GetMatchingKey(key);
            if (null != matchingKey) {
                var currVals = this.vals[matchingKey];
                foreach (var newVal in value) {
                    currVals.Add(newVal);
                }
            } else {
                this.vals.Add(key, value);
            }
        }

        public void Add(KeyValuePair<UV, IList<double>> item) {
            this.Add(item.Key, item.Value);
        }

        public void Clear() {
            this.vals.Clear();
        }

        public bool Contains(KeyValuePair<UV, IList<double>> item) {
            var matchingKey = GetMatchingKey(item.Key);
            if (null != matchingKey) {
                return AreValuesEqual(this.vals[matchingKey], item.Value);
            }
            return false;
        }

        public bool ContainsKey(UV key) {
            return null != GetMatchingKey(key);
        }

        public void CopyTo(KeyValuePair<UV, IList<double>>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<UV, IList<double>>> GetEnumerator() {
            return this.vals.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.vals.GetEnumerator();
        }
        
        public bool Remove(UV key) {
            var matchingKey = GetMatchingKey(key);
            if (null != matchingKey) {
                return this.vals.Remove(matchingKey);
            } else {
                return false;
            }
        }

        public bool Remove(KeyValuePair<UV, IList<double>> item) {
            var matchingKey = GetMatchingKey(item.Key);
            if (null != matchingKey) {
                return this.vals.Remove(matchingKey);
            } else {
                return false;
            }
        }

        public bool TryGetValue(UV key, out IList<double> value) {
            var matchingKey = GetMatchingKey(key);
            if (null != matchingKey) {
                return this.vals.TryGetValue(matchingKey, out value);
            } else {
                value = null;
                return false;
            }
        }
        #endregion

        #region key compare methods
        UV GetMatchingKey(UV key) {
            foreach (var val in vals) {
                if (AreKeysEqual(val.Key, key)) {
                    return val.Key;
                }
            }
            //returning null because it is not a necessarily problem if the key is not found. 
            //a redesign to make this throw an exception is fine by me with proper handling in all methods above
            return null;
        }

        bool AreKeysEqual(UV key1, UV key2) {
            return key1.Equals(key2) || key1.IsAlmostEqualTo(key2);
        }

        bool AreValuesEqual(IList<double> vals1, IList<double> vals2) {
            if (vals1.Count == vals2.Count) {
                for (var i = 0; i < vals1.Count; i++) {
                    if (!AreDoublesAlmostEqual(vals1[i], vals2[i])) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        bool AreDoublesAlmostEqual(double v1, double v2) {
            return Math.Abs(v1 - v2) < GeometryUtil.DoubleComp;
        }
        #endregion
    }
}
