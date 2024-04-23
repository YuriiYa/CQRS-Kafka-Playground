using CQRS.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Api.Queries
{
    public class FindPostByIdQuery:BaseQuery
    {
        public Guid Id { get; set; }
        
    }
}