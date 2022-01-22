namespace Musement.Model
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }

    }
}