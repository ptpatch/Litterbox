using LitterBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class DashboardService
    {
        #region Define as Singleton
        private static DashboardService _Instance;

        public static DashboardService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DashboardService();
                }

                return (_Instance);
            }
        }

        private DashboardService()
        {
        }
        #endregion

        public int GetUserCount()
        {
            LitterboxContext context = new LitterboxContext();

            return context.Users.Count();
        }
        public int GetRolesCount()
        {
            LitterboxContext context = new LitterboxContext();

            return context.Roles.Count();
        }
    }
}