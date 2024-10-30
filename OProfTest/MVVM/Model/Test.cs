using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OProfTest.MVVM.Model
{
    [Serializable]
    [Table("Tests")]
    public class Test
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Title {  get; set; }

        public string Description { get; set; }

        public IEnumerable<TestImage> TestImages { get; set; }
        [NotMapped]
        public byte[] ImageBytes { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        public IEnumerable<Result> Results { get; set; }
    }
}