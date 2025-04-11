using AMS.Doman.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Doman.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn is null && !IsExpired;
    }
}
