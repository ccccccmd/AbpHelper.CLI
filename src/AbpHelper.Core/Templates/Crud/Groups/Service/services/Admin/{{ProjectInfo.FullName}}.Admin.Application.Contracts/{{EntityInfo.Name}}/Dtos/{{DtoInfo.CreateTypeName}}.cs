using System;
{{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
using System.ComponentModel;
{{~ end ~}}

namespace {{ ProjectInfo.FullName }}.Admin.{{EntityInfo.Name}}_App.Dtos;

[Serializable]
public class {{ DtoInfo.CreateTypeName }}
{
    {{~ for prop in EntityInfo.Properties ~}}
    {{~ if prop | abp.is_ignore_property; continue; end ~}}
    {{~ if prop.Document| !string.whitespace ~}}
    /// <summary>
    /// {{ prop.Document }}
    /// </summary>
    {{~ end ~}} 
    {{~ if !Option.SkipLocalization && Option.SkipViewModel ~}}
    [DisplayName("{{ EntityInfo.Name + prop.Name}}")]
    {{~ end ~}}
    public {{ prop.Type}} {{ prop.Name }} { get; set; }
    {{~ if !for.last ~}}

    {{~ end ~}}
    {{~ end ~}}
}