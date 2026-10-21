using AutoMapper;
using BusinessLogic.Models.Product;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Generic;
using Repository.Entity;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

public class ProductService(IMapper mapper, IProductRepository productRepository)
    : BaseService<ulong, Product, ProductReadModel, ProductCreateModel, ProductUpdateModel>(mapper, productRepository), IProductService
{
    private IProductRepository ProductRepository => (IProductRepository) _repository;
}
