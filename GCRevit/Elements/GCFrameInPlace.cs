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
    
    public class GCFrameInPlace : AGCFrameBase {

        #region constructor
        protected GCFrameInPlace(FamilyInstance elem)
            : base(elem) { }

        public static GCFrameInPlace CreateGCFrameInPlace(Element elem) {
            return new GCFrameInPlace(elem as FamilyInstance);
        }

        public static bool IsFrameInPlace(Element elem) {
            return
                AGCFrameBase.IsFrame(elem) &&
                elem.Location is LocationPoint && 
                !AGCFrameCurveDriven.TypicalFramingParametersExist(elem);
        }
        #endregion
    }
}
