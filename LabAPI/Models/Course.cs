using System.ComponentModel.DataAnnotations.Schema;

namespace LabAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }
        public double Duration { get; set; }

        //One to Many Relation (Navigation Prop)
        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public virtual Track? Track { get; set; }
    }
}
