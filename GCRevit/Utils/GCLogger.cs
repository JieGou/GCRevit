using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCRevit.Utils {

    public static class GCLogger {

        #region static members
        static StringBuilder log;
        static int succCt;
        static int failCt;
        #endregion

        #region static constructor
        static GCLogger() {
            log = new StringBuilder();
            succCt = 0;
            failCt = 0;
        }
        #endregion

        #region properties
        public static int SuccessCount {
            get { return succCt; }
            set { succCt = value; }
        }

        public static int FailCount {
            get { return failCt; }
            set { failCt = value; }
        }
        #endregion

        #region log methods
        public static void AppendLine() {
            log.AppendLine();
        }

        public static void AppendLine(string message) {
            log.AppendLine(message);
        }

        public static void AppendLine(string format, params object[] args) {
            try {
                log.AppendLine(String.Format(format, args));
            } catch {
                AppendLine("Failed To log formatted data: {0}", format);
            }
        }

        public static void DisplayLog(bool clearLog) {
            using (GCLoggerForm form = new GCLoggerForm(log.ToString())) {
                form.ShowDialog();
            }
            if (clearLog) {
                ClearLog();
            }
        }

        public static void ClearLog() {
            log = new StringBuilder();
            succCt = 0;
            failCt = 0;
        }
        #endregion
    }
}
