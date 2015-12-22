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

    public class GCWallCurtainPanel : AGCInstance {

        #region constructors
        public GCWallCurtainPanel(Panel elem)
            : base(elem) { }

        public static GCWallCurtainPanel CreateGCWallCurtainPanel(Element elem) {
            return new GCWallCurtainPanel(elem as Panel);
        }

        public static bool IsWallCurtainPanel(Element elem) {
            return elem is Panel;
        }
        #endregion
    }
}
