using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Application.Host.Queries.GetHost
{
    public class GetHostQuery : IRequest<string>
    {
        public string Host { get; set; }
    }
}
