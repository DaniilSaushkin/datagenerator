using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datagenerator.core.models
{
    [Table("USERS")]
    [Index(nameof(Nickname), IsUnique = true)]
    public sealed class User
    {
        [Comment("Идентификатор")]
        public Guid ID { get; set; }

        [MaxLength(32)]
        [Comment("Псевдоним пользователя")]
        public required string Nickname { get; set; }

        [Comment("Признак удаленного пользователя")]
        public bool IsDeleted { get; set; }

        public UserData UserData { get; set; } = new ();
    }
}
