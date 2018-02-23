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

    public class GCFoundationFloor : AGCFoundationBase {

        #region constructors
        private GCFoundationFloor(Floor elem)
            : base(elem) { }
                
        public static GCFoundationFloor CreateGCFoundationFloor(Element elem) {
            return new GCFoundationFloor(elem as Floor);
        }

        public static bool IsFoundationFloor(Element elem) {
            return
                AGCFoundationBase.IsFoundationBase(elem) &&
                AGCFoundationBase.TypicalFoundationParametersExist(elem);
        }
        #endregion

        #region properties
        public Floor RevitFloor {
            get { return this.elem as Floor; }
        }
        #endregion
    }
}
