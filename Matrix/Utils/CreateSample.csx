public static void Main()
{
    var Ls = new List<Example>();
    var FirstSample = new Example();
    FirstSample.Type = typeof(DateTime);
    FirstSample.MethodName = "AddDays";
    var lst = new List<SampleObject>();
    var a = new List<KeyValuePair<Type, object>>();
    a.Add(new KeyValuePair<Type, object>(typeof(int), -1));
    lst.Add(new SampleObject() { Instance = new DateTime(2017, 1, 23), parameters = a.ToArray() });
    FirstSample.SampleObjects = lst;
    Ls.Add(FirstSample);
    var aba = JsonConvert.SerializeObject(Ls);
    //read
    var b = JsonConvert.DeserializeObject<IEnumerable<Example>>(File.ReadAllText("DesignedExamples.json"));
    foreach (var example in b)
    {
        foreach (var item in example.SampleObjects)
        {
            var thisMethod = example.Type.GetMethod(example.MethodName);
            System.Diagnostics.Debug.WriteLine(thisMethod.Name);
            System.Diagnostics.Debug.WriteLine(thisMethod.ReturnType);
            foreach (var sd in item.parameters.Select(x => x.Value).ToArray())
            {
                System.Diagnostics.Debug.WriteLine(sd);
            }
            var invokeResult = thisMethod.Invoke(item.Instance, item.parameters.Select(x => x.Value).ToArray());
        }

    }
}
class SampleObject
{
    public object Instance { get; set; }
    public KeyValuePair<Type, object>[] parameters { get; set; }
}
class Sample
{
    public IEnumerable<SampleObject> SampleObjects { get; set; }
    public Type Type { get; set; }
    public string MethodName { get; set; }
}