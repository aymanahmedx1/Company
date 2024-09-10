namespace WpfApp1
{
    public class Class1
    {
        public string messaging_product { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public Template template { get; set; }
    }

    public partial class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public Component[] components { get; set; }
    }

    public partial class Component
    {
        public string type { get; set; }

        public Parameter[] parameters { get; set; }
    }

    public partial class Parameter
    {
        public string type { get; set; }

        public Document document { get; set; }

        public string text { get; set; }
    }

    public partial class Document
    {
        public string link { get; set; }
        public string filename { get; set; }
    }

    public partial class Language
    {
        public string code { get; set; }
    }



}
