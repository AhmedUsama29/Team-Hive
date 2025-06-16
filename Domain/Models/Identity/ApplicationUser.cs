using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public enum UserType
    {
        Student = 1,
        Teacher = 2,
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
    }

    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public UserType UserType { get; set; } = default!;

    }
}
