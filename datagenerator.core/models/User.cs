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
        public string? Nickname { get; set; }

        [Comment("Признак удаленного пользователя")]
        public bool IsDeleted { get; set; }

        [Comment("Ссылка на пользовательские данные")]
        public Guid UserDataID { get; set; }
        public UserData? UserData { get; set; }
    }
}
