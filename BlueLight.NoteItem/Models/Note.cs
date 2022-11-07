using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.NoteItem.Models
{
    public class Note
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)] // 主键自增id
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Create { get; set; }
    }
}
