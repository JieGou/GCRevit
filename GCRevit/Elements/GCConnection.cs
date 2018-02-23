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
using System.Collections.Generic;

namespace GCRevit.Elements {
    
    public class GCConnection : AGCInstance {

        #region constructors
        private GCConnection(FamilyInstance elem)
            : base(elem) { }
                
        public static GCConnection CreateGCConnection(Element elem) {
            return new GCConnection(elem as FamilyInstance);
        }
        
        public static bool IsConnection(Element elem) {
            return 
                AGCInstance.IsInstance(elem) && 
                elem.Category.Name.Equals(RevitCategoryUtil.ConnectionCategoryName);
        }
        #endregion
    }
}
