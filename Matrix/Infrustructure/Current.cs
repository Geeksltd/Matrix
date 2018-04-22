using Matrix.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Infrustructure
{
    static class Current
    {
        public static ISymbol Symbol { get; set; }
        public static IEnumerable<Example> DesignedExamples { get; set; }
    }
}
