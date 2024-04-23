using Post.Common.Events;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public EventHandler(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity()
            {
                PostId = @event.Id,
                Author = @event.Author,
                DatePosted = @event.DatePosted,
                Message = @event.Message
            };

            await _postRepository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
                throw new ArgumentException($"Post with ID {@event.Id} does not exist.");


            post.Message = @event.Message;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(PostLikedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
                throw new ArgumentException($"Post with ID {@event.Id} does not exist.");


            post.Likes++; ;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity()
            {
                PostId = @event.Id,
                Comment = @event.Comment,
                CommentDate = @event.CommentDate,
                CommentId = @event.CommentId,
                Username = @event.Username,
                Edited = false
            };
            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetByIdAsync(@event.CommentId);

            if (comment == null)
                throw new ArgumentException($"Comment with ID {@event.CommentId} does not exist.");

            comment.Comment = @event.Comment;
            comment.CommentDate = @event.EditDate;
            comment.Edited = true;
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            await _commentRepository.DeleteAsync(@event.CommentId);
        }

        public async Task On(PostRemovedEvent @event)
        {
            await _postRepository.DeleteAsync(@event.Id);
        }
    }
}