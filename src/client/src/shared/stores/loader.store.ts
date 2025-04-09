import { writable, derived } from 'svelte/store';

const callCount$ = writable(0);

export const visible$ = derived(callCount$, (callCount) => callCount > 0);
export const show = () => callCount$.update(x => x + 1);
export const hide = () => callCount$.update(x => x - 1); 