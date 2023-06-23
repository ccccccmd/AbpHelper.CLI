import {{ "{ " }} {{EntityInfo.Name}}Service {{ "}" }} from '/@/api/proxy/{{abp.kebaberize EntityInfo.Name}}-app';
import {{ "{ " }}  {{EntityInfo.Name}}Dto {{ "}" }} from '/@/api/proxy/{{abp.kebaberize EntityInfo.Name}}-app/dtos';
import { FormSchema } from '/@/components/Form';

const service = new {{EntityInfo.Name}}Service();
interface modalParmType {
  updateSchema?: (data: Partial<FormSchema> | Partial<FormSchema>[]) => Promise<void>;
  value: {{EntityInfo.Name}}Dto;
}

export function useDataModal() {
  //保存
  const save = (val: {{EntityInfo.Name}}Dto): Promise<{{EntityInfo.Name}}Dto> => {
    return val.id ? service.update(val.id, val) : service.create(val);
  };

  //弹框取消
  const cancel = () => {
    console.log('cancel');
  };

  //初始化数据
  const init = (modal: modalParmType) => {
    if (modal.value?.id) {
      //修改
    } else {
      //新增
    }
  };

  return {
    save,
    cancel,
    init,
  };
}
