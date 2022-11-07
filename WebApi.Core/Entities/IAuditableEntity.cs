namespace WebApi.Core.Entities
{
    public interface IAuditableEntity
    {
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
        DateTime? CreatedDate { get; set; }
        Guid? CreatedById { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedById { get; set; }
    }
}
