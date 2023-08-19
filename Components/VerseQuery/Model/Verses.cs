using System.Collections;

namespace StudyCentralV2.Components.VerseQuery.Model
{
    public class Verses
    {
        public class Root : IDisposable
        {
            public Data data { get; set; }
            public Meta meta { get; set; }
            public IEnumerator<Vers> GetEnumerator()
            {
                return ((IEnumerable<Vers>)data.verses).GetEnumerator();
            }

            public void Dispose() => GC.Collect();
        }

        public class Data
        {
            public string query { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public int total { get; set; }
            public int verseCount { get; set; }
            public Vers[] verses { get; set; }
        }

        public class Vers
        {
            public string id { get; set; }
            public string orgId { get; set; }
            public string bookId { get; set; }
            public string bibleId { get; set; }
            public string chapterId { get; set; }
            public string reference { get; set; }
            public string text { get; set; }
        }

        public class Meta
        {
            public string fums { get; set; }
            public string fumsId { get; set; }
            public string fumsJsInclude { get; set; }
            public string fumsJs { get; set; }
            public string fumsNoScript { get; set; }
        }

        public class Displayed
        {
            public string reference { get; set; }
            public string text { get; set; }
        }
    }
}
