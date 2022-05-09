using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCW.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        public string Title { get; set; }
        public DateTime DateReleased { get; set; }
        public int StandardCharge { get; set; }
        public int PenaltyCharge { get; set; }

        [ForeignKey("DVDCategory")]
        public int CategoryNumber { get; set; }
        public virtual DVDCategory? DVDCategory { get; set; }

        [ForeignKey("Studio")]
        public int StudioNumber { get; set; }
        public virtual Studio? Studio { get; set; }

        [ForeignKey("Producer")]
        public int ProducerNumber { get; set; }
        public virtual Producer? Producer { get; set; }

        [NotMapped]
        public virtual ICollection<DVDCopy> DVDCopy { get; set; }

        [NotMapped]
        public virtual ICollection<CastMember> CastMember { get; set; }

        [NotMapped]
        public virtual string actors { get; set; }

        [NotMapped]
        public virtual List<Actor> ActorList { get; set; }
    }
}
