using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; }

        [Column("is_deleted", TypeName= "bit")]
        public bool IsDeleted { get; set; } = false;
    }
}
