using DomvsUnitTestPoc.Application.Constants;
using MediatR;
using System;
using System.Globalization;

namespace DomvsUnitTestPoc.Application.Commands
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public CreateProductCommand(string name, string price, int quantity)
        {
            decimal priceTest = 0;
            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(price)
                || !decimal.TryParse(price, NumberStyles.Float, ApplicationConstants.CultureInfoPtBr, out priceTest)
                || quantity <= 0)
            {
                throw new Exception(ApplicationConstants.InvalidData);
            }
            Name = name;
            Price = priceTest;
            Quantity = quantity;
        }
    }
}
