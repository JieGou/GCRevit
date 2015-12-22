//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Exceptions;
using System;

namespace GCRevit.Elements {

    public class GCRoofExtrusion : AGCRoofBase {

        #region constructors
        public GCRoofExtrusion(ExtrusionRoof elem)
            : base(elem) { }

        public static GCRoofExtrusion CreateGCRoofExtrusion(Element elem) {
            return new GCRoofExtrusion(elem as ExtrusionRoof);
        }

        public static bool IsRoofExtrusion(Element elem) {
            return
                AGCRoofBase.IsRoof(elem) &&
                elem is ExtrusionRoof;
        }
        #endregion

        #region properties
        public ExtrusionRoof RevitExtrusionRoof {
            get { return this.elem as ExtrusionRoof; }
        }
        #endregion
    }
}
