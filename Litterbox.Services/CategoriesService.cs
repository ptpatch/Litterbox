using Litterbox.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LitterBox.Data;

namespace Litterbox.Services
{
    public class CategoriesService
    {
        #region Define as Singleton
        private static CategoriesService _Instance;

        public static CategoriesService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CategoriesService();
                }

                return (_Instance);
            }
        }

        private CategoriesService()
        {
        }
        #endregion

        public List<Category> GetAllCategories()
        {
            LitterboxContext context = new LitterboxContext();

            return context.Categories.OrderBy(x => x.DisplaySeqNo).ToList();
        }

        public List<Category> GetFeaturedCategories(int recordSize = 5)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Categories.Where(x => x.isFeatured).OrderBy(x => x.DisplaySeqNo).Take(recordSize).ToList();
        }

        public List<Category> GetAllParentCategories()
        {
            LitterboxContext context = new LitterboxContext();

            return context.Categories.Where(x => !x.ParentCategoryID.HasValue).OrderBy(x => x.DisplaySeqNo).ToList();
        }

        public Category GetCategoryByID(int ID)
        {
            using (var context = new LitterboxContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public Category GetCategoryByName(string sanitizedCategoryName)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Categories.FirstOrDefault(x => x.SanitizedName.Equals(sanitizedCategoryName));
        }

        public void SaveCategory(Category category)
        {
            LitterboxContext context = new LitterboxContext();

            context.Categories.Add(category);

            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            LitterboxContext context = new LitterboxContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public bool DeleteCategory(int ID)
        {
            using (var context = new LitterboxContext())
            {
                var category = context.Categories.Find(ID);

                context.Categories.Remove(category);

                return context.SaveChanges() > 0;
            }
        }

        public List<Category> SearchCategories(int? parentCategoryID, string searchTerm, int? pageNo, int pageSize)
        {
            LitterboxContext context = new LitterboxContext();

            var categories = context.Categories.AsQueryable();

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * pageSize;

            return categories.OrderBy(x => x.DisplaySeqNo).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetCategoriesCount(int? parentCategoryID, string searchTerm)
        {
            LitterboxContext context = new LitterboxContext();

            var categories = context.Categories.AsQueryable();

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return categories.Count();
        }
    }
}