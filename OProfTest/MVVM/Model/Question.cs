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
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Test")]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }

        public int QuestionType { get; set; }

        public string Title { get; set; }
    }
}
