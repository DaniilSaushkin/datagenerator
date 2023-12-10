using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace datagenerator.core.models
{
    [Table("USERDATA")]
    public sealed class UserData
    {
        [Comment("Идентификатор")]
        public Guid ID { get; set; }

        [Comment("Хешированный пароль")]
        public string? Password { get; set; }

        [Comment("Ссылка на пользователя")]
        public Guid UserID { get; set; }
        public User? User { get; set; }
    }
}