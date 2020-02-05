using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.SqlClient;

namespace bug_tracker.code
{
    public static class MenusDB
    {
        static BugTrackerDbmlDataContext bug_tracker = new BugTrackerDbmlDataContext();

        public static List<bug_tracker_menu_header> GetMenu()
        {
            List<bug_tracker_menu_header> menu = new List<bug_tracker_menu_header>();

            menu = (from _menus in bug_tracker.bug_tracker_menu_headers
                    join _menu_roles in bug_tracker.bug_tracker_menu_roles on _menus.id equals _menu_roles.menu_id
                    where _menu_roles.role_id == Convert.ToInt64(HttpContext.Current.Session["login_type"])
                    select _menus).Distinct().ToList();

            return menu;
        }

        public static List<bug_tracker_menu_header> GetSubMenus(int menu_id)
        {
            List<bug_tracker_menu_header> submenus = new List<bug_tracker_menu_header>();

            submenus = (from _submenus in bug_tracker.bug_tracker_menu_headers
                        join _menus in bug_tracker.bug_tracker_menu_submenus on _submenus.id equals _menus.submenu_id
                        join _menu_role in bug_tracker.bug_tracker_menu_roles on _submenus.id  equals _menu_role.menu_id
                        where _menus.menu_header_id == menu_id && _menu_role.role_id == Convert.ToInt64(HttpContext.Current.Session["login_type"])
                        select _submenus).Distinct().ToList();

            return submenus;
        }

        public static List<bug_tracker_page> GetMenuPages(int menu_id)
        {
            List<bug_tracker_page> pages = new List<bug_tracker_page>();

            pages = (from _pages in bug_tracker.bug_tracker_pages
                     join _menu in bug_tracker.bug_tracker_menu_pages on _pages.id equals _menu.page_id
                     join _page_roles in bug_tracker.bug_tracker_pages_roles on _pages.id equals _page_roles.page_id
                     where _menu.menu_id == menu_id && _page_roles.role_id == Convert.ToInt64(HttpContext.Current.Session["login_type"])
                     select _pages).Distinct().ToList();

            return pages;
        }

        public static bug_tracker_page GetMenuHeaderPage(int menu_id)
        {
            bug_tracker_page page = (from _page in bug_tracker.bug_tracker_pages
                                     join _menu in bug_tracker.bug_tracker_menu_pages on _page.id equals _menu.page_id
                                     where _menu.menu_id == menu_id
                                     select _page).FirstOrDefault();

            return page;
        }
    }
}