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

    public class GCFloorInPlace : AGCInstance {

        #region constructors
        public GCFloorInPlace(FamilyInstance elem)
            : base(elem) { }
                
        public static GCFloorInPlace CreateGCFloorInPlace(Element elem) {
            return new GCFloorInPlace(elem as FamilyInstance);
        }

        public static bool IsFloorInPlace(Element elem) {
            return 
                AGCInstance.IsInstance(elem) && 
                elem.Category.Name.Equals(RevitCategoryUtil.FloorCategoryName);
        }
        #endregion
    }
}
