<script lang="ts">
    import { onMount } from 'svelte';
    import { visible$ } from '../stores/loader.store';
    let visible = false;
    onMount(() => visible$.subscribe(x => visible = x))
</script>

{#if visible}
    <div class="wrapper">
        <div class="body">
            <i class="las la-cog"></i>
        </div>
    </div>
{/if}

<style>
.wrapper {
    position: absolute;
    z-index: 2000;

    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    
    background: var(--black-t1);
    
    display: flex;
    justify-content: center;
    align-items: center;
    
    padding: 2rem;
}

.body {
    font-size: 3rem;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    width: 25%;
    text-align: center;
    color: var(--white);
}

.la-cog {
    line-height: 100%;
    color: var(--color4);
    text-shadow: 1px 1px 5px var(--color7);
    width: fit-content;
    height: 100%;
    text-align: center;
    background-color: transparent;

    animation: cog-rotate .5s, cog-color 3s;
    animation-iteration-count: infinite;
}

/*
 * Animation
 */
@keyframes cog-rotate {
    0% {
        -webkit-transform: rotate(0deg);
           -moz-transform: rotate(0deg);
             -o-transform: rotate(0deg);
                transform: rotate(0deg);
    }
    100% {
        -webkit-transform: rotate(360deg);
           -moz-transform: rotate(360deg);
             -o-transform: rotate(360deg);
                transform: rotate(360deg);
    }
}
</style>