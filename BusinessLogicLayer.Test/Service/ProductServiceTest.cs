using Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Test.Service;

public class ProductServiceTest
{
    private readonly Mock<IProductRepository> _mockProductRepository;

    public ProductServiceTest()
    {
        _mockProductRepository = new Mock<IProductRepository>();
    }


}
