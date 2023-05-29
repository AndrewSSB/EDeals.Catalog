using System.ComponentModel.DataAnnotations.Schema;

namespace EDeals.Catalog.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; private init; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
