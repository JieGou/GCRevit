//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GCRevit;
using GCRevit.Elements;
using GCRevit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevitTest {

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class GCRevitTestCommand : IExternalCommand {

        GCRevitDocument doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) {
            try {
                this.doc = GCRevitDocument.DocumentInstance(commandData);
                GCLogger.AppendLine("Document Path: {0}", this.doc.Document.PathName);
                TestCollectors();
                TestCreators();
                GCLogger.DisplayLog(true);
                return Result.Succeeded;
            } catch (Exception x) {
                GCLogger.AppendLine("Failed: {0}\n{1}", x.Message, x.StackTrace);
                GCLogger.DisplayLog(true);
                return Result.Failed;
            }
        }

        void TestCollectors() {
            //Columns
            //List<GCColumnVertical> vertCols = 
            //Connections

            //Floors

            //Foundations

            //Framing

            //Grids

            //Instances

            //Levels

            //Mass

            //Roofs

            //Views

            //Walls
        }

        void TestCreators() {
            GCLogger.AppendLine("Creators");
        }
    }
}