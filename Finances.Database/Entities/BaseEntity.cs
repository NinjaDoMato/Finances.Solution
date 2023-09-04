using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.Database.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }

    [DisplayName("Data de criação")]
    public DateTime DateCreated { get; set; }
   
    [DisplayName("Última atualização")]
    public DateTime? LastUpdate { get; set; }
}
