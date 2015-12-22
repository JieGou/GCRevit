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

    public abstract class AGCWallBase : AGCElement {

        #region constructor
        protected AGCWallBase(Wall elem)
            : base(elem) { }

        protected AGCWallBase(CurtainSystem elem)
            : base(elem) { }

        protected AGCWallBase(FamilyInstance elem)
            : base(elem) { }

        public static bool IsWallBase(Element elem) {
            return 
                (elem is Wall || elem is CurtainSystem) && 
                elem.Category.Name.Equals(RevitCategoryUtil.WallCategoryName);
        }
        #endregion
    }
}
