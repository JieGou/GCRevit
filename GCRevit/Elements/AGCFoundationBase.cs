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

    public abstract class AGCFoundationBase : AGCElement {

        #region constructors
        protected AGCFoundationBase(FamilyInstance elem)
            : base(elem) { }

        protected AGCFoundationBase(Floor elem)
            : base(elem) { }

        public static bool IsFoundationBase(Element elem) {
            return
                (elem is Floor || elem is FamilyInstance) &&
                elem.Category.Name.Equals(RevitCategoryUtil.FoundationCategoryName);
        }

        public static bool TypicalFoundationParametersExist(Element elem) {
            foreach (var paramName in RevitFoundationParameterUtil.StandardFoundationParameterNames) {
                if (null == elem.LookupParameter(paramName)) {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
