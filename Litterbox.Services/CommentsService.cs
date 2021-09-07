using Litterbox.Entities;
using LitterBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class CommentsService
    {
        #region Define as Singleton
        private static CommentsService _Instance;

        public static CommentsService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CommentsService();
                }

                return (_Instance);
            }
        }

        private CommentsService()
        {
        }
        #endregion

        public bool AddComment(Comment comment)
        {
            LitterboxContext context = new LitterboxContext();

            context.Comments.Add(comment);

            return context.SaveChanges() > 0;
        }

        public List<Comment> GetComments(int entityID, int recordID, int recordsSize = 20)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Comments.Where(x => x.EntityID == entityID && x.RecordID == recordID).OrderByDescending(x => x.TimeStamp).Take(recordsSize).ToList();
        }

        public List<Comment> GetComments(string userID, string searchTerm, int entityID, int? pageNo, int recordsSize)
        {
            LitterboxContext context = new LitterboxContext();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordsSize;

            var comments = context.Comments.Where(x => x.EntityID == entityID)
                                   .AsQueryable();

            if (!string.IsNullOrEmpty(userID))
            {
                comments = comments.Where(x => x.UserID == userID);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                comments = comments.Where(x => x.Text.ToLower().Contains(searchTerm.ToLower()));
            }

            return comments.OrderByDescending(x => x.TimeStamp)
                           .Skip(skipCount)
                           .Take(recordsSize)
                           .ToList();
        }

        public int GetCommentsTotalCount(string userID, string searchTerm, int entityID)
        {
            LitterboxContext context = new LitterboxContext();

            var comments = context.Comments.Where(x => x.EntityID == entityID)
                                   .AsQueryable();

            if (!string.IsNullOrEmpty(userID))
            {
                comments = comments.Where(x => x.UserID == userID);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                comments = comments.Where(x => x.Text.ToLower().Contains(searchTerm.ToLower()));
            }

            return comments.Count();
        }

        public Comment GetComment(int ID)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Comments.Find(ID);
        }

        public bool DeleteComment(int ID)
        {
            LitterboxContext context = new LitterboxContext();

            var comment = context.Comments.Find(ID);

            if (comment != null)
            {
                context.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
            }
            return context.SaveChanges() > 0;
        }
    }
}