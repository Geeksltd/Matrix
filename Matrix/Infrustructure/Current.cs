using Matrix.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Matrix.Infrustructure
{
    static class Current
    {
        public static ISymbol Symbol { get; set; }
        public static IEnumerable<Example> DesignedExamples { get => JsonConvert.DeserializeObject<IEnumerable<Example>>(File.ReadAllText("DesignedExamples.json")); }
    }
}
