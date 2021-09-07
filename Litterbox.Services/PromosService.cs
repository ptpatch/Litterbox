﻿using Litterbox.Entities;
using LitterBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class PromosService
    {
        #region Define as Singleton
        private static PromosService _Instance;

        public static PromosService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PromosService();
                }

                return (_Instance);
            }
        }

        private PromosService()
        {
        }
        #endregion

        public List<Promo> GetAllPromos()
        {
            LitterboxContext context = new LitterboxContext();

            return context.Promos.ToList();
        }

        public List<Promo> SearchPromos(string searchTerm, int? pageNo, int pageSize)
        {
            LitterboxContext context = new LitterboxContext();

            var Promos = context.Promos.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Promos = Promos.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return Promos.OrderByDescending(x => x.Name).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetPromosCount(string searchTerm)
        {
            LitterboxContext context = new LitterboxContext();

            var Promos = context.Promos.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Promos = Promos.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return Promos.Count();
        }

        public Promo GetPromoByID(int ID)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Promos.Find(ID);
        }
        public Promo GetPromoByCode(string code)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Promos.FirstOrDefault(x => x.Code == code);
        }

        public List<Promo> GetPromosByIDs(List<int> IDs)
        {
            LitterboxContext context = new LitterboxContext();

            return IDs.Select(id => context.Promos.Find(id)).OrderBy(x => x.ID).ToList();
        }

        public void SavePromo(Promo Promo)
        {
            LitterboxContext context = new LitterboxContext();

            context.Promos.Add(Promo);

            context.SaveChanges();
        }


        public void UpdatePromo(Promo Promo)
        {
            LitterboxContext context = new LitterboxContext();

            var exitingPromo = context.Promos.Find(Promo.ID);

            context.Entry(exitingPromo).CurrentValues.SetValues(Promo);

            context.SaveChanges();
        }

        public bool DeletePromo(int ID)
        {
            using (var context = new LitterboxContext())
            {
                var promo = context.Promos.Find(ID);

                context.Promos.Remove(promo);

                return context.SaveChanges() > 0;
            }
        }
    }
}