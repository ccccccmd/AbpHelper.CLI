<template>
  <BasicModal
    v-bind="$attrs"
    :useWrapper="true"
    :title="formTitle"
    @register="registerModal"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
    :showCancelBtn="false"
    :showOkBtn="false"
    @cancel="cancelModal"
  >
    <BasicForm @register="registerForm" @submit="handleSubmit" :showResetButton="false" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useI18n } from '/@/hooks/web/useI18n';
import { BasicForm, useForm } from '/@/components/Form';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { getModalFormSchemas } from '../datas/ModalData';
import { useDataModal } from '../hooks/useDataModal';

export default defineComponent({
  name: '',
  components: { BasicForm, BasicModal },
  emits: ['change'],
  setup(_props, { emit }) {
    const { t } = useI18n();
    const formTitle = ref<string>('');

    const modal = useDataModal();

    const [registerModal, { closeModal }] = useModalInner((val: any) => {
      resetFields();
      if (val?.id) {
        setFieldsValue(val);
        formTitle.value = '编辑';
      } else {
        formTitle.value = '创建';
      }

      modal.init({ value: val, updateSchema });
    });

    const [registerForm, { setFieldsValue, resetFields, updateSchema }] = useForm({
      labelWidth: 120,
      submitButtonOptions: { text: '保存' },
      schemas: [...getModalFormSchemas()],
      actionColOptions: {
        span: 24,
        pull: 10,
      },
    });

    const handleSubmit = (val: any) => {
      modal.save(val).then(() => {
        closeModal();
        emit('change');
      });
    };

    const cancelModal = () => {
      modal.cancel();
    };
    return {
      t,
      formTitle,
      registerModal,
      registerForm,
      cancelModal,
      handleSubmit,
    };
  },
});
</script>
