using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContextFactory _databaseContextFactory;

        public CommentRepository(DatabaseContextFactory databaseContextFactory)
        {
            _databaseContextFactory = databaseContextFactory;
        }

        public async Task CreateAsync(CommentEntity comment)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateContext();
            _ = databaseContext.Comments.Add(comment);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid commentid)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateContext();

            var comment = await GetByIdAsync(commentid);
            if (comment == null) return;
            _ = databaseContext.Comments.Remove(comment);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task<CommentEntity> GetByIdAsync(Guid commentid)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateContext();
            return await databaseContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentid);
        }

        public async Task UpdateAsync(CommentEntity comment)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateContext();
            _ = databaseContext.Comments.Update(comment);
            _ = await databaseContext.SaveChangesAsync();
        }
    }
}