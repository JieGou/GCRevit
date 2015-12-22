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

    public class GCFloor : AGCFloorBase {

        #region constructors
        public GCFloor(Floor floor)
            : base(floor) { }
                
        public static GCFloor CreateGCFloor(Element elem) {
            return new GCFloor(elem as Floor);
        }
        
        public static bool IsFloor(Element elem) {
            return 
                AGCFloorBase.IsFloorBase(elem) &&
                AGCFloorBase.TypicalFloorParametersExist(elem);
        }
        #endregion

        #region properties
        public Floor RevitFloor {
            get { return this.elem as Floor; }
        }
        #endregion
    }
}
