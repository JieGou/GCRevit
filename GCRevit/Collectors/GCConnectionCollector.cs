//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
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

namespace GCRevit.Collectors {

    public static class GCConnectionCollector {

        public static IEnumerable<GCConnection> CollectAllConnections(GCRevitDocument doc) {
            return GCElementCollector.CollectAllElementsOfCategory<GCConnection>(doc, BuiltInCategory.OST_StructConnections, GCConnection.IsConnection, GCConnection.CreateGCConnection);
        }

        public static IEnumerable<GCConnection> CollectSelectedConnections(GCRevitDocument doc) {
            return GCElementCollector.CollectSelectedElements<GCConnection>(doc, GCConnection.IsConnection, GCConnection.CreateGCConnection);
        }
    }
}
