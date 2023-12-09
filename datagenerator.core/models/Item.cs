using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datagenerator.core.models
{
    [Table("ITEMS")]
    [Index(nameof(Name), IsUnique = true)]
    public sealed class Item
    {
        [Comment("Идентификатор")]
        public Guid ID { get; set; }

        [MaxLength(32)]
        [Comment("Имя предмета")]
        public required string Name { get; set; }

        [Comment("Признак удаленного предмета")]
        public required bool IsDeleted { get; set; }

        [Comment("Ссылка на шаблон")]
        public Guid TemplateID { get; set; }
        public required Template Template { get; set; }
    }
}
