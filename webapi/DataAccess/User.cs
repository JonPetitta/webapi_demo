namespace webapi.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("webapi.user")]
    public partial class User
    {
        [Column(@"id", TypeName = "uint")]
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column(@"firstname")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        [Column(@"lastname")]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        [Column(@"username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        [Column(@"password")]
        public string Password { get; set; }

        [Column(@"insertdate", TypeName = "timestamp")]
        public DateTime? InsertDate { get; set; }
    }
}
