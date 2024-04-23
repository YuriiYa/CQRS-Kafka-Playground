using Confluent.Kafka;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<ActionResult> GePostsAsync(Func<Task<List<PostEntity>>> getFunctionAsync, string funcName)
        {
            try
            {
                var posts = await getFunctionAsync();
                if (posts is null || !posts.Any())
                {
                    return NoContent();
                }

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"successcully returned {posts.Count} posts"
                });
            }
            catch (Exception ex)
            {
                string SAFE_ERROR_MESSAGE = $"Error while processing {funcName}!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPostAsync()
        {
            return await GePostsAsync(() => _queryDispatcher.SendAsync(new FindAllPostsQuery()), nameof(GetAllPostAsync));
        }

        [HttpGet("byid/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync(Guid postId)
        {
            return await GePostsAsync(() => _queryDispatcher.SendAsync(new FindPostByIdQuery() { Id = postId }), nameof(GetByPostIdAsync));
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetByPostByAuthorAsync(string author)
        {
            return await GePostsAsync(() => _queryDispatcher.SendAsync(new FindPostsByAuthorQuery() { Author = author }), nameof(GetByPostByAuthorAsync));
        }

        [HttpGet("withComments")]
        public async Task<ActionResult> GetPostWithCommentsAsync()
        {
            return await GePostsAsync(() => _queryDispatcher.SendAsync(new FindPostsWithCommentsQuery()), nameof(GetPostWithCommentsAsync));
        }

        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostWithLikesAsync(int numberOfLikes)
        {
            return await GePostsAsync(() => _queryDispatcher.SendAsync(new FindPostsWithLikesQuery(){ NumberOfLikes= numberOfLikes}), nameof(GetPostWithLikesAsync));
        }
    }
}