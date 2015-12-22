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

    public class GCRoofFootPrint : AGCRoofBase {

        #region constructors
        public GCRoofFootPrint(FootPrintRoof elem)
            : base(elem) { }

        public static GCRoofFootPrint CreateGCRoofFootPrint(Element elem) {
            return new GCRoofFootPrint(elem as FootPrintRoof);
        }
        
        public static bool IsRoofFootPrint(Element elem) {
            return
                AGCRoofBase.IsRoof(elem) &&
                elem is FootPrintRoof;
        }
        #endregion

        #region properties
        public FootPrintRoof RevitFootPrintRoof {
            get { return this.elem as FootPrintRoof; }
        }
        #endregion
    }
}
