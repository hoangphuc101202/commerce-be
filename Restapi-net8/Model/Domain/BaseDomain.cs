namespace Restapi_net8.Model.Domain
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
