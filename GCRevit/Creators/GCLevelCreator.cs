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

namespace GCRevit.Creators {

    public static class GCLevelCreator {

        public static GCLevel CreateLevel(GCRevitDocument doc, double elev) {
#if (REVIT2015)
            Level lev = doc.Document.Create.NewLevel(elev);
#else
            var lev = Level.Create(doc.Document, elev);
#endif
            return GCLevel.CreateGCLevel(lev);
        }

        public static List<GCLevel> CreateLevels(GCRevitDocument doc, List<double> elevs) {
            var levs = new List<GCLevel>();
            foreach (var elev in elevs) {
#if (REVIT2015)
                Level lev = doc.Document.Create.NewLevel(elev);
#else
                var lev = Level.Create(doc.Document, elev);
#endif
                levs.Add(GCLevel.CreateGCLevel(lev));
            }
            return levs;
        }
    }
}
