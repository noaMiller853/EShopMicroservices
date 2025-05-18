
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler (IDocumentSession session,ILogger<UpdateProductCommandHandler>logger)
        : ICommanderHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           logger.LogInformation("UpdateProductCommandHandler called with {@Command}", request);
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
