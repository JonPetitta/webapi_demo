namespace webapi.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("webapi.usertasks")]
    public partial class UserTask
    {
        [DataMember]
        [Column(@"id", TypeName = "uint")]
        public long Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(150)]
        [Column(@"title")]
        public string Title { get; set; }

        [DataMember]
        [Column(@"completedate")]
        public DateTime CompleteDate { get; set; }

        [Column(@"userid", TypeName = "uint")]
        public long UserId { get; set; }

        [DataMember]
        [Column(@"taskcomplete", TypeName = "bit")]
        public bool TaskComplete { get; set; }

        [DataMember]
        [Column(@"insertdate", TypeName = "timestamp")]
        public DateTime? InsertDate { get; set; }
    }
}
