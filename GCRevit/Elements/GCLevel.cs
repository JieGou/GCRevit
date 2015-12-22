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

    public class GCLevel : AGCElement {

        #region constructor
        public GCLevel(Level inst)
            : base(inst) { }

        public static GCLevel CreateGCLevel(Element elem) {
            return new GCLevel(elem as Level);
        }

        public static bool IsLevel(Element elem) {
            return 
                elem is Level && 
                elem.Category.Name.Equals(RevitCategoryUtil.LevelCategoryName);
        }
        #endregion

        #region properties
        public Level RevitLevel {
            get { return this.elem as Level; }
        }
        #endregion
    }
}
