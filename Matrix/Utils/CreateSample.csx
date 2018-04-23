//This is a helper script used to produce and test samples
public static void Main()
{
    var examples = new List<Example>();
    var example = new Example() { Type = typeof(DateTime), MethodName = "AddDays" };
    var samples = new List<SampleObject>();
    var parameters = new List<KeyValuePair<Type, object>>();

    parameters.Add(new KeyValuePair<Type, object>(typeof(int), -1));
    samples.Add(new SampleObject() { Instance = new DateTime(2017, 1, 23), parameters = parameters.ToArray() });
    example.SampleObjects = samples;
    examples.Add(example);


    var Json = JsonConvert.SerializeObject(examples);

    //read
    var b = JsonConvert.DeserializeObject<IEnumerable<Example>>(File.ReadAllText("DesignedExamples.json"));
    foreach (var eg in b)
    {
        foreach (var item in eg.SampleObjects)
        {
            var thisMethod = eg.Type.GetMethod(example.MethodName);
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