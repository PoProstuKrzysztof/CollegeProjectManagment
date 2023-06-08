using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ProgrammingLanguages
{
    CSharp,
    Java,
    Python,
    JavaScript,
    Ruby,
    TypeScript,
    CPlusPlus,
    Go,
    Swift,
    Kotlin
}