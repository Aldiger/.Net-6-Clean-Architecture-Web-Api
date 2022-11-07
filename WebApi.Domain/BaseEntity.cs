using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Core.Entities;

namespace WebApi.Domain;

public abstract class BaseEntity : IEntity, IAuditableEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public bool IsActive { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public Guid? CreatedById { get; set; }
    public DateTime? UpdatedDate { get; set; }
    [JsonIgnore]
    public Guid? UpdatedById { get; set; }
}