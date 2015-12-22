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

namespace GCRevit.Elements {

    public abstract class AGCViewBase : AGCElement {

        #region constructors
        protected AGCViewBase(View elem)
            : base(elem) { }

        public static bool IsViewBase(Element elem) {
            return 
                elem is View && 
                elem.Category.Name.Equals(RevitCategoryUtil.ViewCategoryName);
        }
        #endregion

        #region properties
        public View RevitView {
            get { return this.elem as View; }
        }
        #endregion
    }
}
