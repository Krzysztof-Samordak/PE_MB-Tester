using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using MindWorks.Nimbus;

namespace Keysight.KtEL30000
{
    [InstrumentFamilies]
    internal static class Families
    {
        internal const string EL30000 = "EL30000";

        internal static readonly string[] EL30000Members = new[] { Models.EL34243A, Models.EL34143A, Models.EL33133A };
    }

    [InstrumentModels(Models.EL34243A)]
    internal static class Models
    {
        internal const string Any = "";
        internal const string EL34243A = "EL34243A";
        internal const string EL34143A = "EL34143A";
        internal const string EL33133A = "EL33133A";
    }
}
