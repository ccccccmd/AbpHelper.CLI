<template>
  <div class="content">
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button
          v-if="hasPermission('AbpIdentity.Roles.Create')"
          type="primary"
          @click="handleAddNew"
          >{{ "{{ " }} t("添加{{EntityInfo.Name}}") {{ "}} " }}</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :stop-button-propagation="true"
          :actions="[
            {
              auth: '{{ProjectInfo.Name}}.{{EntityInfo.Name }}.Update',
              label: t('AbpUi.Edit'),
              icon: 'ant-design:edit-outlined',
              onClick: handleEdit.bind(null, record),
            },
            {
              auth: '{{ProjectInfo.Name}}.{{EntityInfo.Name }}.Delete',
              color: 'error',
              label: t('AbpUi.Delete'),
              icon: 'ant-design:delete-outlined',
              ifShow: !record.isStatic,
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <RoleModal @register="registerModal" @change="reloadTable" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useI18n } from "/@/hooks/web/useI18n";
import { usePermission } from "/@/hooks/web/usePermission";
import { useModal } from "/@/components/Modal";
import RoleModal from "./DataModal.vue";
import { PermissionModal } from "/@/components/Permission";
import { BasicTable, TableAction } from "/@/components/Table";
import { useDataTable } from "../hooks/useDataTable";

export default defineComponent({
  name: "DataTable",
  components: {
    BasicTable,

    RoleModal,
    TableAction,
    PermissionModal,
  },
  setup() {
    const { t } = useI18n();
    const { hasPermission } = usePermission();
    const [registerModal, { openModal }] = useModal();
    const { registerTable, reloadTable, handleDelete } = useDataTable();

    return {
      t,
      hasPermission,
      registerTable,
      reloadTable,
      registerModal,
      openModal,

      handleDelete,
    };
  },
  methods: {
    handleAddNew() {
      this.openModal(true, {}, true);
    },
    handleEdit(record) {
      this.openModal(true, record, true);
    },
  },
});
</script>
