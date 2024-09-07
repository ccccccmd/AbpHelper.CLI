import { useI18n } from '/@/hooks/web/useI18n';
import { useTable } from '/@/components/Table';
import { formatPagedRequest } from '/@/utils/http/abp/helper';
import { getDataColumns } from '../datas/TableData';
import { getSearchFormSchemas } from '../datas/ModalData';
import { Modal } from 'ant-design-vue';
import {{ "{ " }} {{EntityInfo.Name}}Service {{ "}" }} from '/@/api/proxy/{{abp.kebaberize EntityInfo.Name}}-app';

const { t } = useI18n();
export function useDataTable() {
  const { getList, deleteById } = new {{EntityInfo.Name}}Service();
  const [registerTable, { reload: reloadTable }] = useTable({
    rowKey: 'id',
    title: t('列表'),
    columns: getDataColumns(),
    api: getList,
    beforeFetch: formatPagedRequest,
    pagination: true,
    striped: false,
    useSearchForm: true,
    showTableSetting: true,
    bordered: true,
    showIndexColumn: false,
    canResize: false,
    immediate: true,

    formConfig: getSearchFormSchemas(),
    actionColumn: {
      width: 220,
      title: t('table.action'),
      dataIndex: 'action',
      slots: { customRender: 'action' },
    },
  });

  function handleDelete(o) {
    Modal.warning({
      title: t('AbpUi.AreYouSure'),
      content: t('AbpUi.ItemWillBeDeletedMessageWithFormat', [o.store_name] as Recordable),
      okCancel: true,
      onOk: () => {
        deleteById(o.id).then(() => {
          reloadTable();
        });
      },
    });
  }

  return {
    registerTable,
    reloadTable,
    handleDelete,
  };
}
