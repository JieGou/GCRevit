//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Elements {

    public class GCLink : AGCElement {

        #region
        public GCLink(RevitLinkInstance link) 
            : base(link) { }

        public static GCLink CreateGCRevitLink(Element elem) {
            return new GCLink(elem as RevitLinkInstance);
        }

        public static bool IsLink (Element e) {
            return e is RevitLinkInstance;
        }
        #endregion

        #region
        public RevitLinkInstance RevitLink {
            get { return this.elem as RevitLinkInstance; }
        }
        #endregion
    }
}
