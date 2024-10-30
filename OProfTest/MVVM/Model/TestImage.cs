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
    [Table("TestsImages")]
    public class TestImage
    {
        [Key]
        public int ID { get; set; }

        public byte[] Image { get; set; }

        public string FileExtension { get; set; }

        public decimal Size { get; set; }

        public string FilePath { get; set; }

        [ForeignKey("Test")]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }
    }
}
