import { useI18n } from '/@/hooks/web/useI18n';
import { FormProps, FormSchema } from '/@/components/Form';
const { t } = useI18n();

export function getSearchFormSchemas(): Partial<FormProps> {
  return {
    labelWidth: 100,
    showAdvancedButton: true,
    autoAdvancedLine: 10,
    schemas: [
	{{~ for prop in EntityInfo.Properties ~}}
		{
		field: '{{ prop.Name | abp.camel_case_ui }}',
		component: 'Input',
		label: '{{if prop.Document!=""&&prop.Document!=null; prop.Document; else; prop.Name;end}}',
		defaultValue: '',
		colProps: {
          lg: 12,
          xs: 24,
          },
		},
	{{~ end ~}}
    ],
  };
}

export function getModalFormSchemas(): FormSchema[] {
  return [
    {
      field: 'id',
      component: 'Input',
      label: 'id',
      show: false,
      colProps: { lg: 12, xs: 24 },
    },
	{{~ for prop in EntityInfo.Properties ~}}
		{
		field: '{{ prop.Name | abp.camel_case_ui }}',
		component: 'Input',
		label: '{{if prop.Document!=""&&prop.Document!=null; prop.Document; else; prop.Name;end}}',
		colProps: {
          lg: 12,
          xs: 24,
          }
		},
	{{~ end ~}}
  ];
}
