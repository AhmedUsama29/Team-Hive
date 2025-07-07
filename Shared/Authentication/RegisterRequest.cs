using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{

    public enum UserType
    {
        Student = 1,
        Teacher = 2,
        TeacherAssistant = 3,
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public UserType Role { get; set; }

    }
}
