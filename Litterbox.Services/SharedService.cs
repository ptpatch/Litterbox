using Litterbox.Entities;
using LitterBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class SharedService
    {
        #region Define as Singleton
        private static SharedService _Instance;

        public static SharedService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SharedService();
                }

                return (_Instance);
            }
        }

        private SharedService()
        {
        }
        #endregion

        public int SavePicture(Picture picture)
        {
            LitterboxContext context = new LitterboxContext();

            context.Pictures.Add(picture);

            context.SaveChanges();

            return picture.ID;
        }
    }
}