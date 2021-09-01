using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using MediatR;

namespace DomvsUnitTestPoc.Application.Commands
{
    public class ListProductCommand : IRequest<PagedModel<Product>>
    {
        public string Search { get; }

        public ListProductCommand(string search)
        {
            Search = search;
        }
    }
}
