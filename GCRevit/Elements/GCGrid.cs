//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Exceptions;
using GCRevit.Utils;
using System;

namespace GCRevit.Elements {

    public class GCGrid : AGCGridBase {

        #region constructors
        public GCGrid(Grid grid)
            : base(grid) { }

        public static GCGrid CreateGCGrid(Element elem) {
            return new GCGrid(elem as Grid);
        }

        public static bool IsGrid(Element elem) {
            return 
                elem is Grid && 
                elem.Category.Name.Equals(RevitCategoryUtil.GridCategoryName);
        }
        #endregion

        #region properties
        public Grid RevitGrid {
            get { return this.elem as Grid; }
        }
        #endregion
    }
}
