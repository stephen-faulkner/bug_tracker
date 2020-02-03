using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace bug_tracker.code
{
    public class LogsDB
    {
        static BugTrackerDbmlDataContext bug_tracker = new BugTrackerDbmlDataContext();

        public static log AddEditLog(log log)
        {
            log _log = null;

            try
            {
                bug_tracker.logs.InsertOnSubmit(log);
                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict objConflict in bug_tracker.ChangeConflicts)
                    foreach (MemberChangeConflict memberChange in objConflict.MemberConflicts)
                        memberChange.Resolve(RefreshMode.KeepCurrentValues);

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (Exception ex)
            {
                
            }

            return _log;
        }

        public static bool AddLog(string description, int log_type)
        {
            bool log_added = false;
            try
            {
                log _log = new log();
                _log.log_description = description;
                _log.log_type = log_type;
                _log.userid = HttpContext.Current.Session["userid"].ToString();
                _log.created_date = DateTime.Now;

                bug_tracker.logs.InsertOnSubmit(_log);
                bug_tracker.SubmitChanges();

                log_added = true;
            }
            catch(Exception ex)
            {

            }

            return log_added;
        }

        public static bool AddLog(string description, int log_type, Exception ex)
        {
            bool log_added = false;
            try
            {
                log _log = new log();
                _log.log_description = description;
                _log.log_type = log_type;
                _log.exception_messaage = ex.Message;
                _log.exception_stacktrace = ex.StackTrace;
                _log.userid = HttpContext.Current.Session["userid"].ToString();
                _log.created_date = DateTime.Now;

                bug_tracker.logs.InsertOnSubmit(_log);
                bug_tracker.SubmitChanges();

                log_added = true;
            }
            catch (Exception e)
            {

            }

            return log_added;
        }

        public static bool AddLog(string description, int log_type, Int64 object_id)
        {
            bool log_added = false;
            try
            {
                log _log = new log();
                _log.log_description = description;
                _log.log_type = log_type;
                _log.obj_id = object_id;
                _log.userid = HttpContext.Current.Session["userid"].ToString();
                _log.created_date = DateTime.Now;

                bug_tracker.logs.InsertOnSubmit(_log);
                bug_tracker.SubmitChanges();

                log_added = true;
            }
            catch (Exception ex)
            {

            }

            return log_added;
        }

        public static bool AddLog(string description, int log_type, Int64 object_id, Exception ex)
        {
            bool log_added = false;
            try
            {
                log _log = new log();
                _log.log_description = description;
                _log.log_type = log_type;
                _log.obj_id = object_id;
                _log.exception_messaage = ex.Message;
                _log.exception_stacktrace = ex.StackTrace;
                _log.userid = HttpContext.Current.Session["userid"].ToString();
                _log.created_date = DateTime.Now;

                bug_tracker.logs.InsertOnSubmit(_log);
                bug_tracker.SubmitChanges();

                log_added = true;
            }
            catch (Exception e)
            {

            }

            return log_added;
        }

        public static log_type GetLogType(string this_log_type)
        {
            log_type type = (from log_types in bug_tracker.log_types
                             where log_types.log_type1.ToLower() == this_log_type.ToLower()
                             select log_types).FirstOrDefault();

            return type;
        }
    }
}