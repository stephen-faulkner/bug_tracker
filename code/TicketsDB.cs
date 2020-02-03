using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.SqlClient;

namespace bug_tracker.code
{
    public class TicketsDB
    {
        static BugTrackerDbmlDataContext bug_tracker = new BugTrackerDbmlDataContext();

        public static ticket GetTicketHeader(Int64 ticketID)
        {
            ticket Ticket = null;

            try
            {
                Ticket = (from _ticket in bug_tracker.tickets
                          where _ticket.id == ticketID
                          select _ticket).FirstOrDefault();
            }
            catch(Exception ex)
            {

            }

            return Ticket;
        }

        public static List<ticket_status> GetTicketStatuses()
        {
            List<ticket_status> ticket_Statuses = null;

            ticket_Statuses = (from status in bug_tracker.ticket_status
                               select status).Distinct().ToList();

            return ticket_Statuses;
        }

        public static ticket_status GetStatus(Int16 statusID)
        {
            ticket_status status = (from stat in bug_tracker.ticket_status
                                    where stat.id == statusID
                                    select stat).FirstOrDefault();

            return status;
        }

        public static ticket_status GetStatus(string status_description)
        {
            ticket_status status = (from stat in bug_tracker.ticket_status
                                    where stat.description.ToLower() == status_description.ToLower()
                                    select stat).FirstOrDefault();

            return status;
        }

        public static List<ticket_priority> GetTicket_Priorities()
        {
            List<ticket_priority> priorities = (from priority in bug_tracker.ticket_priorities
                                                select priority).Distinct().ToList();


            return priorities;
        }

        public static ticket AddEditTicket(ticket ticket)
        {
            ticket _ticket = null;

            try
            {
                if(ticket.id != 0)
                {
                    _ticket = (from tickets in bug_tracker.tickets
                               where tickets.id == ticket.id
                               select tickets).FirstOrDefault();

                    _ticket.description = ticket.description;
                    _ticket.status = ticket.status;
                    _ticket.priority = ticket.priority;
                }
                else
                {
                    _ticket = new ticket();
                    _ticket.created_date = DateTime.Now;
                    _ticket.created_by = HttpContext.Current.Session["userid"].ToString();
                    _ticket.title = ticket.title;
                    _ticket.description = ticket.description;
                    _ticket.status = ticket.status;
                    _ticket.priority = ticket.priority;

                    bug_tracker.tickets.InsertOnSubmit(_ticket);
                }

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch(ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict objConflict in bug_tracker.ChangeConflicts)
                    foreach (MemberChangeConflict memberChange in objConflict.MemberConflicts)
                        memberChange.Resolve(RefreshMode.KeepCurrentValues);

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (Exception ex)
            {
                LogsDB.AddLog("Error in add/edit ticket", LogsDB.GetLogType("error").id, ticket.id, ex);
            }

            return _ticket;
        }

        public void AddTicketNote(string this_note, ticket this_ticket)
        {
            try
            {
                note _note = new note();

                _note.ticket_id = this_ticket.id;
                _note.message = this_note;
                _note.created_date = DateTime.Now;
                _note.creator_id = HttpContext.Current.Session["userid"].ToString();

                bug_tracker.notes.InsertOnSubmit(_note);
                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);

                string s = "";
            }
            catch (ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict objConflict in bug_tracker.ChangeConflicts)
                    foreach (MemberChangeConflict memberChange in objConflict.MemberConflicts)
                        memberChange.Resolve(RefreshMode.KeepCurrentValues);

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ArgumentNullException ex)
            {

            }
            catch(Exception ex)
            {
                LogsDB.AddLog("Error adding note to ticket. Note = {0}", LogsDB.GetLogType("error").id, this_ticket.id, ex);
            }
            
        }
    }
}