using Restapi_net8.Exceptions.Http;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository supplierRepository;
    public SupplierService(ISupplierRepository supplierRepository)
    {
        this.supplierRepository = supplierRepository;
    }
    public async Task<ApiResponse> CreateSupplier(CreateSupplierRequestDTO request)
    {
        var supplierExist = await supplierRepository.GetByNameAsync(request.name);
        if(supplierExist != null)
        {
           throw new BadRequestHttpException("Supplier already exist");
        }
        var supplierEmailExist = await supplierRepository.GetByEmailAsync(request.email);
        if(supplierEmailExist != null)
        {
           throw new BadRequestHttpException("Supplier email already exist");
        }
        var supplier = new Supplier
        {
            Name = request.name,
            Logo = request.logo,
            SupplierContact = request.supplierContact,
            Address = request.address,
            Email = request.email,
            Phone = request.phone,
        };
        await supplierRepository.CreateAsync(supplier);
        return new ApiResponse(200, "Supplier created successfully", supplier, null);
    }
    public async Task<ApiResponse> GetSupplierById(string id)
    {
        var supplier = await supplierRepository.GetById(Guid.Parse(id));
        if(supplier == null)
        {
            throw new NotFoundHttpException("Supplier not found");
        }
        var data = new {
            Id = supplier.Id,
            Name = supplier.Name,
            Logo = supplier.Logo,
            SupplierContact = supplier.SupplierContact,
            Address = supplier.Address,
            Email = supplier.Email,
            Phone = supplier.Phone,
        };
        return new ApiResponse(200, "Supplier found", data, null);
    }
    public async Task<ApiResponse> UpdateSupplier(string id, UpdateSupplierRequestDTO request)
    {
        var supplier = await supplierRepository.GetById(Guid.Parse(id));
        if(supplier == null)
        {
            throw new NotFoundHttpException("Supplier not found");
        }
        var supplierUpdate = new Supplier
        {
            Id = supplier.Id,
            Name = request.name,
            Logo = request.logo,
            SupplierContact = request.supplierContact,
            Address = request.address,
            Email = request.email,
            Phone = request.phone,
        };
        await supplierRepository.UpdateAsync(supplier, supplierUpdate);
        return new ApiResponse(200, "Supplier updated successfully", null, null);
    }
    public async Task<ApiResponse> GetAllSuppliers(){
        var suppliers = await supplierRepository.GetAll();
        var data = suppliers.Select(s => new {
            id = s.Id,
            name = s.Name,
            logo = s.Logo,
            supplierContact = s.SupplierContact,
            address = s.Address,
            email = s.Email,
            phone = s.Phone,
        }).ToList();
        return new ApiResponse(200, "Suppliers retrieved successfully", data, null);
    }
    public async Task<ApiResponse> DeleteSupplier(string id)
    {
        var supplier = await supplierRepository.GetById(Guid.Parse(id));
        if(supplier == null)
        {
            throw new NotFoundHttpException("Supplier not found");
        }
        await supplierRepository.SoftDelete(Guid.Parse(id));
        return new ApiResponse(200, "Supplier deleted successfully", null, null);
    }
}