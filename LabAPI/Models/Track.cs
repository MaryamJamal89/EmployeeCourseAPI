using System.Text.Json.Serialization;

namespace LabAPI.Models
{
    public class Track
    {
        public Track()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }

        //One to Many Relation
        //[JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
