//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using GCRevit.Exceptions;
using GCRevit.Utils;
using System;

namespace GCRevit.Elements {

    public class GCFrameBrace : AGCFrameCurveDriven {

        #region constructors
        protected GCFrameBrace(FamilyInstance famInst)
            : base(famInst) { }

        public static GCFrameBrace CreateGCFrameBrace(Element elem) {
            return new GCFrameBrace(elem as FamilyInstance);
        }

        public static bool IsBrace(Element elem) {
            return
                AGCFrameCurveDriven.IsFrameCurveDriven(elem) &&
                StructuralType.Brace == (elem as FamilyInstance).StructuralType &&
                AGCFrameCurveDriven.TypicalFramingParametersExist(elem);
        }
        #endregion
    }
}
