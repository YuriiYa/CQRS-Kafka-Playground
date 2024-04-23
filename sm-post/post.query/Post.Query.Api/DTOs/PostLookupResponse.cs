using Post.Common.DTOs;
using Post.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Api.DTOs
{
    public class PostLookupResponse:BaseResponse
    {
        public List<PostEntity> Posts{get;set;}
        
    }
}