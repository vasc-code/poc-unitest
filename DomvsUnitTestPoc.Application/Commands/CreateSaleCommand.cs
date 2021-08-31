using DomvsUnitTestPoc.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace DomvsUnitTestPoc.Application.Commands
{
    public class CreateSaleCommand : IRequest<bool>
    {
        public IList<Sale> Sales { get; }

        public CreateSaleCommand(IList<Sale> sales)
        {
            Sales = sales;
        }
    }
}
