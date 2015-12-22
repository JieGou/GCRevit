﻿//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.DB;
using GCRevit.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Collectors {

    public class GCLinkCollector {

        public static IEnumerable<GCLink> CollectAllLinks(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfClass<GCLink>(doc, typeof(RevitLinkInstance), GCLink.IsLink, GCLink.CreateGCRevitLink);
        }

        public static IEnumerable<GCLink> CollectSelectedLinks(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCLink>(doc, GCLink.IsLink, GCLink.CreateGCRevitLink);
        }
    }
}
