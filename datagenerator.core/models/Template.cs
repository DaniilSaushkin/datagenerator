using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datagenerator.core.models
{
    [Table("TEMPLATES")]
    [Index(nameof(Name), IsUnique = true)]
    public sealed class Template
    {
        [Comment("Идентификатор")]
        public Guid ID { get; set; }

        [MaxLength(16)]
        [Comment("Имя шаблона")]
        public required string Name { get; set; }

        [Comment("Признак удаленного шаблона")]
        public required bool IsDeleted { get; set; }

        public List<Item> Items { get; set; } = new ();
    }
}
