<template>
  <DataTable />
</template>

<script lang="ts">
import { defineComponent } from "vue";

import DataTable from "./components/DataTable.vue";
export default defineComponent({
  name: "{{abp.kebaberize  EntityInfo.Name }}",
  components: {
    DataTable,
  },
  setup() {},
});
</script>
