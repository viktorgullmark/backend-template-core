using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseBackend.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseBackend.Entities
{
    // Test-entity as example on how to reference identity cols
    public class Test : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        public string UserId { get; set; }

        // Reference to identity's usertable
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}
