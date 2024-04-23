using CQRS.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Api.Queries
{
    public class FindPostsByAuthorQuery:BaseQuery
    {
        public string Author { get; set; }
    }
}