using System.Collections;

namespace StudyCentralV2.Components.VerseQuery.Model
{
    public class Bibles
    {
        public List<Datum> data { get; set; }

        public IEnumerator GetEnumerator()
        {
            if (data != null)
            {
                return data.GetEnumerator();
            }
            else
            {
                return null;
            }
        }
    }

    public class AudioBible
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nameLocal { get; set; }
        public string description { get; set; }
        public string descriptionLocal { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nameLocal { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string dblId { get; set; }
        public string abbreviation { get; set; }
        public string abbreviationLocal { get; set; }
        public Language language { get; set; }
        public List<Country> countries { get; set; }
        public string name { get; set; }
        public string nameLocal { get; set; }
        public string description { get; set; }
        public string descriptionLocal { get; set; }
        public string relatedDbl { get; set; }
        public string type { get; set; }
        public DateTime updatedAt { get; set; }
        public List<AudioBible> audioBibles { get; set; }
    }

    public class Language
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nameLocal { get; set; }
        public string script { get; set; }
        public string scriptDirection { get; set; }
    }
}
