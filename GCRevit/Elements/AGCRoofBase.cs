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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Elements {

    public abstract class AGCRoofBase : AGCElement {

        #region constructors
        protected AGCRoofBase(FootPrintRoof elem)
            : base(elem) { }

        protected AGCRoofBase(ExtrusionRoof elem)
            : base(elem) { }

        protected AGCRoofBase(FamilyInstance elem)
            : base(elem) { }

        public static bool IsRoof(Element elem) {
            return 
                (elem is FootPrintRoof || elem is ExtrusionRoof) && 
                elem.Category.Name.Equals(RevitCategoryUtil.RoofCategoryName);
        }
        #endregion
    }
}
