using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OProfTest.MVVM.Model
{
    [Serializable]
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        public int Role { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IEnumerable<Result> Results { get; set; }
    }
}