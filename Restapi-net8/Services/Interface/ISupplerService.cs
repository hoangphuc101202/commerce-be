using Restapi_net8.Middlewares;

public interface ISupplierService
{
    public Task<ApiResponse> CreateSupplier(CreateSupplierRequestDTO request);
    public Task<ApiResponse> GetSupplierById(string id);
    public Task<ApiResponse> UpdateSupplier(string id, UpdateSupplierRequestDTO request);
    public Task<ApiResponse> GetAllSuppliers();
    public Task<ApiResponse> DeleteSupplier(string id);
}