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

    public class GCWall : AGCWallBase {

        #region constructors
        public GCWall(Wall elem)
            : base(elem) { }

        public static GCWall CreateGCWall(Element elem) {
            return new GCWall(elem as Wall);
        }

        public static bool IsWall(Element elem) {
            return
                AGCWallBase.IsWallBase(elem) &&
                elem is Wall;
        }
        #endregion
        
        #region properties
        public Wall RevitWall {
            get { return this.elem as Wall; }
        }
        #endregion
    }
}
