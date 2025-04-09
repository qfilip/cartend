<script lang="ts">
  import Loader from './shared/components/loader.svelte';
  import { show, hide } from './shared/stores/loader.store';
  
  function showLoader() {
    show()
    setTimeout(() => hide(), 3000);
  }
</script>

<Loader />

<header>
  <h1>
    Cartend
  </h1>
</header>

<main>
  <button on:click={showLoader}>Load</button>
</main>
