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

    public class GCViewDrafting : AGCViewportView {

        #region constructors
        protected GCViewDrafting(ViewDrafting elem)
            : base(elem) { }

        public static GCViewDrafting CreateGCViewDrafting(Element elem) {
            return new GCViewDrafting(elem as ViewDrafting);
        }

        public static bool IsViewDrafting(Element elem) {
            return 
                AGCViewBase.IsViewBase(elem) &&
                elem is ViewDrafting;
        }
        #endregion

        #region properties
        public ViewDrafting RevitViewDrafting {
            get { return this.elem as ViewDrafting; }
        }
        #endregion
    }
}
