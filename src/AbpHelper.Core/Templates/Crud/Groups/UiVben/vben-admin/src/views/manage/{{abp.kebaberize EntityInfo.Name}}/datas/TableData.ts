import { BasicColumn } from '/@/components/Table/src/types/table';
import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();

export function getDataColumns(): BasicColumn[] {
    return [
        {
            title: 'id',
            dataIndex: 'id',
            width: 1,
            ifShow: false,
        },
    {{~ for prop in EntityInfo.Properties ~}}
        {
        title:  '{{if prop.Document!=""&&prop.Document!=null; prop.Document; else; prop.Name;end}}',
        dataIndex: '{{ prop.Name | abp.camel_case_ui }}',
        },
    {{~ end ~}}
  ];
}


