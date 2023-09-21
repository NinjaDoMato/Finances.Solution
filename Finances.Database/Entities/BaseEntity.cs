using System.ComponentModel;

namespace Finances.Database.Entities;

/// <summary>
/// Base entity of the database with all the common properties.
/// </summary>
public class BaseEntity
{
    public Guid Id { get; set; }

    [DisplayName("Data de criação")]
    public DateTime DateCreated { get; set; }

    [DisplayName("Última atualização")]
    public DateTime? LastUpdate { get; set; }
}
