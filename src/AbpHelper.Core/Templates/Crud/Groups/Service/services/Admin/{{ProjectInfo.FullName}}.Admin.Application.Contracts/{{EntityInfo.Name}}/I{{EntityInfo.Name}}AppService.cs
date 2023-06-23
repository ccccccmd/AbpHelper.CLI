using System;
using {{ ProjectInfo.FullName }}.Admin.{{EntityInfo.Name}}_App.Dtos;
{{~ if Option.SkipGetListInputDto ~}}
using Volo.Abp.Application.Dtos;
{{~ end ~}}
using Volo.Abp.Application.Services;

namespace {{ProjectInfo.FullName}}.Admin.{{EntityInfo.Name}}_App;

{{
    if !Option.SkipGetListInputDto
        TGetListInput = EntityInfo.Name + "GetListInput"
    else
        TGetListInput = "PagedAndSortedResultRequestDto"
end ~}}

{{~ if EntityInfo.Document | !string.whitespace ~}}
/// <summary>
/// {{ EntityInfo.Document }}
/// </summary>
{{~ end ~}}
public interface I{{ EntityInfo.Name }}AppService :
    ICrudAppService< 
        {{ DtoInfo.ReadTypeName }}, 
        {{ EntityInfo.PrimaryKey ?? EntityInfo.CompositeKeyName }}, 
        {{TGetListInput}},
        {{ DtoInfo.CreateTypeName }},
        {{ DtoInfo.UpdateTypeName }}>
{

}