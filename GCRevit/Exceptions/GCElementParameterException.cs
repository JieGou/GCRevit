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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Exceptions {

    public class GCElementParameterException : Exception {
        public GCElementParameterException()
            : base() { }

        public GCElementParameterException(string message)
            : base(message) { }

        public GCElementParameterException(StorageType storType)
            : base(String.Format("Failed to set parameter of type \"{0}\": Failed to cast object", storType.ToString())) { }

        public GCElementParameterException(StorageType storType, string message)
            : base(String.Format("Failed to set parameter of type \"{0}\": {1}", storType.ToString(), message)) { }

        public GCElementParameterException(string format, params object[] args)
            : base(String.Format(format, args)) { }

        public GCElementParameterException(string message, Exception innerException)
            : base(message, innerException) { }

        public GCElementParameterException(string format, Exception innerException, params object[] args)
            : base(String.Format(format, args), innerException) { }

        protected GCElementParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
