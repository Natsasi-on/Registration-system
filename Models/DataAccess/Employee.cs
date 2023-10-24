using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models.DataAccess
{
    public partial class Employee
    {
        public Employee()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }

        [NotMapped]
        public virtual ICollection<RoleSelection> roleSelections { get; set; }
    }
}
