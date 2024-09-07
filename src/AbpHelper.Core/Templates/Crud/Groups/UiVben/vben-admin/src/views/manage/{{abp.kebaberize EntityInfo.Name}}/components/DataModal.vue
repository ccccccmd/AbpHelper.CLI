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
    <BasicForm @register="registerForm" @submit="handleSubmit" :showResetButton="false" :show-submit-button="false">
      <template #formFooter>
        <div style="width: 100%; text-align: right">
          <FormItem>
            <Button type="default" class="mr-2" @click="cancelModal">取消</Button>
            <Button type="primary" class="mr-2" @click="submit" :loading="loading">保存
            </Button>
          </FormItem>
        </div>
      </template>
    </BasicForm>
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useI18n } from '/@/hooks/web/useI18n';
import { BasicForm, useForm } from '/@/components/Form';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { getModalFormSchemas } from '../datas/ModalData';
import { useDataModal } from '../hooks/useDataModal';
import {Button} from "ant-design-vue";
export default defineComponent({
  name: '',
  components: { BasicForm, BasicModal,Button },
  emits: ['change'],
  setup(_props, { emit }) {
    const { t } = useI18n();
    const formTitle = ref<string>('');

    const modal = useDataModal();
    const loading = ref(false);
    const [registerModal, { closeModal }] = useModalInner((val: any) => {
      resetFields();
      loading.value = false;
      if (val?.id) {
        setFieldsValue(val);
        formTitle.value = '编辑';
      } else {
        formTitle.value = '创建';
      }

      modal.init({ value: val, updateSchema });
    });

    const [registerForm, { setFieldsValue, resetFields, updateSchema,submit }] = useForm({
      labelWidth: 120,
      submitButtonOptions: { text: '保存' },
      schemas: [...getModalFormSchemas()],
      actionColOptions: {
        span: 24,
        pull: 10,
      },
    });

    const handleSubmit = (val: any) => {
      loading.value = true;
      modal.save(val).then(() => {
        closeModal();
        emit('change');
        loading.value = false;
      });
    };

    const cancelModal = () => {
      closeModal();
    };
    return {
      t,
      formTitle,
      registerModal,
      registerForm,
      cancelModal,
      handleSubmit,submit,loading
    };
  },
});
</script>
