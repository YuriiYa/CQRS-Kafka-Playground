using Post.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid postId);
        Task<PostEntity> GetByIdAsync(Guid postid);
        Task<List<PostEntity>> ListAllAsync();

        Task<List<PostEntity>> ListByAuthorAsync(string author);
        Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes);
        Task<List<PostEntity>> ListWithCommentsAsync();
    }
}