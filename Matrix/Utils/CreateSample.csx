public static void Main()
{
    var Ls = new List<Example>();
    var FirstSample = new Example();
    FirstSample.Type = typeof(DateTime);
    FirstSample.MethodName = "AddDays";
    var lst = new List<SampleObject>();
    var a = new List<KeyValuePair<Type, object>>();
    a.Add(new KeyValuePair<Type, object>(typeof(int), -1));
    lst.Add(new SampleObject() { Instance = JsonConvert.SerializeObject(new DateTime(2017, 1, 23)), parameters = a.ToArray() });
    FirstSample.SampleObjects = lst;
    Ls.Add(FirstSample);
    var aba = JsonConvert.SerializeObject(Ls);
}
class SampleObject
{
    public string Instance { get; set; }
    public KeyValuePair<Type, object>[] parameters { get; set; }
}
class Sample
{
    public IEnumerable<SampleObject> SampleObjects { get; set; }
    public Type Type { get; set; }
    public string MethodName { get; set; }
}