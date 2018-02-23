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

    public class GCGridMulti : AGCGridBase {

        #region constructors
        protected GCGridMulti(MultiSegmentGrid grid)
            : base(grid) { }

        public static GCGridMulti CreateGCGridMulti(Element elem) {
            return new GCGridMulti(elem as MultiSegmentGrid);
        }

        public static bool IsGridMulti(Element elem) {
            return 
                elem is MultiSegmentGrid;
        }
        #endregion

        #region properties
        public MultiSegmentGrid RevitMultiGrid {
            get { return this.elem as MultiSegmentGrid; }
        }
        #endregion
    }
}
