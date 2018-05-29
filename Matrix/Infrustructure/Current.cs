using System;
using System.Collections.Generic;
using System.IO;
using Matrix.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace Matrix.Infrustructure
{
    static class Current
    {
        public static ISymbol Symbol { get; set; }
        public static IEnumerable<Example> DesignedExamples { get => JsonConvert.DeserializeObject<IEnumerable<Example>>(File.ReadAllText(Path + "/DesignedExamples.json")); }
        public static string Path { get => new FileInfo(new Uri(typeof(TestQuickInfoSource).Assembly.CodeBase, UriKind.Absolute).LocalPath).DirectoryName; }
    }
}
