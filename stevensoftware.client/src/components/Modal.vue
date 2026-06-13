<template>
  <TransitionRoot appear :show="modelValue" as="template">
    <Dialog as="div" class="relative z-50" @close="emit('update:modelValue', false)">
      <TransitionChild as="template"
                       enter="ease-out duration-300"
                       enter-from="opacity-0"
                       enter-to="opacity-100"
                       leave="ease-in duration-200"
                       leave-from="opacity-100"
                       leave-to="opacity-0">
        <div class="fixed inset-0 bg-black/30 backdrop-blur-sm" />
      </TransitionChild>

      <div class="fixed inset-0 overflow-y-auto">
        <div class="flex min-h-full items-center justify-center p-4">

          <TransitionChild as="template"
                           enter="ease-out duration-300"
                           enter-from="opacity-0 scale-95"
                           enter-to="opacity-100 scale-100"
                           leave="ease-in duration-200"
                           leave-from="opacity-100 scale-100"
                           leave-to="opacity-0 scale-95">
            <DialogPanel class="w-full max-w-3xl transform overflow-hidden rounded-2xl px-4 py-8 md:p-8 text-left shadow-md transition-all bg-white border border-slate-200 text-slate-900">
              <DialogTitle v-if="title"
                           class="text-3xl font-bold mb-6 border-b border-slate-200 pb-4 text-slate-900">
                {{ title }}
              </DialogTitle>

              <div class="mb-6 text-slate-700">
                <slot name="body" />
              </div>

              <div class="pt-4 border-t border-slate-200">
                <slot name="footer" />
              </div>

            </DialogPanel>
          </TransitionChild>

        </div>
      </div>
    </Dialog>
  </TransitionRoot>
</template>

<script setup>
  import {
    Dialog,
    DialogPanel,
    DialogTitle,
    TransitionChild,
    TransitionRoot,
  } from '@headlessui/vue';
  import { defineProps, defineEmits } from 'vue';

  const props = defineProps({
    modelValue: Boolean,
    title: String,
  });

  const emit = defineEmits(['update:modelValue']);
</script>
