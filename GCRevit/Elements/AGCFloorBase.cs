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
    
    public abstract class AGCFloorBase : AGCElement {

        #region constructor
        protected AGCFloorBase(Floor elem)
            : base(elem) { }
        
        protected AGCFloorBase(FamilyInstance elem)
            : base(elem) { }

        public static bool IsFloorBase(Element elem) {
            return
                elem is Floor &&
                elem.Category.Name.Equals(RevitCategoryUtil.FloorCategoryName);
        }

        public static bool TypicalFloorParametersExist(Element elem) {
            foreach (var paramName in RevitFloorParameterUtil.StandardFloorParameterNames) {
                if (null == elem.LookupParameter(paramName)) {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
